using CMS.Application.DTOs.Request;
using CMS.Application.Interfaces.CollaborationInterfaces;
using CMS.Application.Interfaces.ContractInterfaces;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CMS.Application.Services
{
    public class CollaborationService : ICollaborationService
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<CollaborationService> _logger;
        private readonly ICollaborationRepository _collaborationRepository;
        private readonly IContractRepository _contractRepository;

        public CollaborationService(IEmailService emailService, ILogger<CollaborationService> logger, ICollaborationRepository collaborationRepository, IContractRepository contractRepository)
        {
            _emailService = emailService;
            _logger = logger;
            _collaborationRepository = collaborationRepository;
            _contractRepository = contractRepository;
        }

        public async Task<bool> AddCollaboratorAsync(CollaborationDto collaborationDto)
        {
            var collaboration = new Collaboration 
            {
                Name = collaborationDto.FirstName,
                DocumentData = await ProcessFile(collaborationDto.DocumentData),

                DocumentName = collaborationDto.DocumentName,
                Email = collaborationDto.Email,

            };
            
            var result = await _collaborationRepository.AddAsync(collaboration);
            if (result)
            {
                return true;
            }
            return false;
        }
        private async Task<byte[]> ProcessFile(IFormFile file)
        {
            byte[] Documentbytes;
            using (var memorystream = new MemoryStream())
            {
                await file.CopyToAsync(memorystream);
                Documentbytes = memorystream.ToArray();
            };
            return Documentbytes;
        }

        public bool EmailExists(string email)
        {
            var result = _collaborationRepository.ExistsEmail(email);
            if (result)
            {
                return true;
            }
            return false;
        }

        public Task<List<Collaboration>> GetAllCollaboratorsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Collaboration> GetCollaboratorByIdAsync(int id)
        {
            return await _collaborationRepository.GetByIdAsync(id);
        }

        public async Task RemoveCollaboratorAsync(Collaboration collaboration)
        {
            await _collaborationRepository.DeleteAsync(collaboration.Id);
        }

        public Task ShareContractAsync(int contractId, string documentPath)
        {
            throw new NotImplementedException();
        }

        // Other methods as defined in your original code...

        //public async Task ShareContractAsync(int contractId, string documentPath)
        //{
        //    if (contractId <= 0) throw new ArgumentOutOfRangeException(nameof(contractId));
        //    if (string.IsNullOrWhiteSpace(documentPath)) throw new ArgumentException("Invalid document path", nameof(documentPath));

        //    _logger.LogInformation("Sharing contract document for contract with ID {ContractId}", contractId);

        //    //var contract = await _contractRepository.GetByIdAsync(contractId);
        //    if (contract == null)
        //    {
        //        _logger.LogWarning("Contract with ID {ContractId} not found", contractId);
        //        throw new KeyNotFoundException("Contract not found");
        //    }

        //    var subject = $"Contract Document: {contract.Title}";
        //    var body = $"Please find the attached document for the contract: {contract.Title}";

        //    foreach (var collaborator in contract.Collaborators)
        //    {
        //        await _emailService.SendEmailAsync(collaborator.Email, subject, body);
        //        _logger.LogInformation("Email sent to collaborator {Email} for contract ID {ContractId}", collaborator.Email, contractId);
        //    }

        //    _logger.LogInformation("Contract document shared for contract with ID {ContractId}", contractId);
        //}


        public Task UpdateCollaborationAsync(CollaborationDto collaborationDto)
        {
            throw new NotImplementedException();
        }
    }
}
