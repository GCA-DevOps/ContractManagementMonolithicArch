using CMS.Application.Interfaces.ContractInterfaces;
using CMS.Domain.DTO;
using CMS.Domain.Entities.Contract.ContractManagement;
using CMS.Domain.Entities.Contract;
using CMS.Domain.Entities.ContractManagement;
using CMS.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CMS.Domain.Entities.Authentication;

namespace CMS.Infrastructure.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContractRepository(ApplicationDbContext context, UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> ContractExistsAsync(int id)
        {
            return await _context.Contracts.AnyAsync(x => x.Id == id);
        }

        public async Task<Contract?> GetById(int id)
        {


            return await _context.Contracts.FindAsync(id);
        }


        public async Task<bool> AddContract(ContractDto obj)
        {

            try
            {
                var contracttype_string = Enum.GetName(typeof(Predefined_ContractTypes), obj.ContractType);
                //var contractstatus_string = Enum.GetName(typeof(ContractStatus), obj.Status);

                var contract = new Contract
                {
                    ContractType = contracttype_string,
                    RequesterName = obj.RequesterName,
                    EffectiveDate = obj.StartDate,

                    EndDate = obj.EndDate,
                    Status = obj.Status,
                    IsApprovalSent = false,
                    AppUserId = await GetCurrentUserId(),
                    DepartmentId = obj.SelectedDepartment,



                };
                _context.Contracts.Add(contract);
                await _context.SaveChangesAsync();

                int contractId = contract.Id; // take the contractid of that newly created contract
                obj.ContractId = contractId;//to use it in updating the contract in summary iaction
                var paymentstructure_string = Enum.GetName(typeof(Predefined_PaymentStructure), obj.PaymentStructure);
                var payment = new Payment
                {
                    ContractId = contractId,
                    PaymentStructure = paymentstructure_string,
                    Amount = obj.ContractValue,

                };
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                //create party record
                var party = new Party
                { //create party record
                    PartyName = obj.PartyName,
                    Email = obj.Email,
                };
                _context.Parties.Add(party);
                await _context.SaveChangesAsync();
                var partyId = party.Id;




                // Associate party with the contract
                var contractParty = new ContractParty
                {   //create contractparty records for this contract ,one contract having many parties
                    ContractId = contractId,
                    PartyId = partyId,


                };

                // Add party to the database
                _context.ContractParties.Add(contractParty);
                await _context.SaveChangesAsync();






                var terminationClause = new TerminationClause
                { //create terminationclause record for this contract
                    ContractId = contractId,
                    NoticePeriod = obj.TerminationNoticePeriod,

                };

                _context.TerminationClauses.Add(terminationClause);
                await _context.SaveChangesAsync();

                //create table to store contract documents 
                if (obj.ContractFile != null && obj.ContractFile.Length > 0)
                {
                    byte[] documentBytes = await ProcessFile(obj.ContractFile); //processing the file


                    // Now that you have the document bytes, you can create the ContractDocument object
                    var contractDocument = new ContractDocument
                    {
                        ContractId = contractId,
                        FileData = documentBytes
                    };

                    _context.ContractDocuments.Add(contractDocument);
                    await _context.SaveChangesAsync();

                    _context.Contracts.Add(contract);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private async Task<string?> GetCurrentUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.User == null)
            {
                return null; // or handle the case appropriately, e.g., throw an exception or return an empty string
            }

            var user = await _userManager.GetUserAsync(httpContext.User);
            return user?.Id;
        }

        public async Task UpdateContract(Contract contract)
        {
            var existingContract = await _context.Contracts.FindAsync(contract.Id);
            if (existingContract != null)
            {
                // Update properties using AutoMapper
                // AutoMapper.Mapper.Map(contract, existingContract);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteContract(int id)
        {
            var contract = await GetById(id);
            if (contract != null)
            {
                _context.Contracts.Remove(contract);
                await _context.SaveChangesAsync();
                return true;

            }
            return false;

        }
        private async Task<byte[]> ProcessFile(IFormFile file)
        {
            byte[] documentBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                documentBytes = memoryStream.ToArray();
            }
            return documentBytes;
        }

        public Task UpdateContract(ContractDto contract)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contract>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Contract> AddAsync(Contract contract)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Contract contract)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        //public Task UpdateContract(Contract contract)
        //{
        //    throw new NotImplementedException();
        //}

        Task<Contract?> IContractRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
