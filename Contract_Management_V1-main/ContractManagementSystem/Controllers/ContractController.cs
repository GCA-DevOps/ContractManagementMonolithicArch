using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using ContractManagementSystem.Models.ContractManagement;
using ContractManagementSystem.Services;
using ContractManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore;

//using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace ContractManagementSystem.Controllers
{

    public class ContractController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ContractService _contractService;
        private readonly UserManager<AppUser> _userManager;
        private readonly GetApproversService _getApproversService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public ContractController(ContractService contractService, UserManager<AppUser> userManager,
               ApplicationDbContext db, GetApproversService getApproversService,
              IEmailService emailService, IConfiguration configuration)//dependency injection
        {

            this._contractService = contractService;
            _userManager = userManager;
            _context = db;
            _getApproversService = getApproversService;
            _emailService = emailService;
            _configuration = configuration;

        }


        public IActionResult ContractReports(Contract obj)
        {

            //get all information from contracts table
            IEnumerable<Contract> ContractList = _context.Contracts;


            return View(ContractList);

        }

        public IActionResult ContractTable()
        {
            return View();
        }

        public IActionResult ContractCount()
        {
            var counts = _contractService.GetContractCount();
            return View(counts);
        }
        private async Task<string> GetCurrentUserId()
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            return user!.Id; //returns user id otherwise null. ? it wont throw an error if it is null

        }


        public IActionResult ContractPopup()
        {
           
        
            

            return PartialView("_ContractPopupPartial");
        }




        public IActionResult ApprovalWorkflow()
        {


            return View();

        }
       


        [HttpGet]
        public IActionResult CreateContract(string selectedContracts)
        {
            // Fetch the departments from the database
            var departments = _context.Departments.ToList();
            ViewBag.Departments = new SelectList(departments, "Id", "DepartmentName");

            // Initialize the view model
            var viewModel = new CreateContractVm();

            // Optionally handle the selected contracts here
            ViewBag.SelectedContracts = selectedContracts;

            return View(viewModel);
        }


        [HttpPost]
        
        public async Task<IActionResult> CreateContract(CreateContractVm obj)
        {
            if (ModelState.IsValid)
            {
                //nb obj.selecteddepartment returns the ID of department 
                var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == obj.SelectedDepartment);
                if (department == null)
                {
                    ModelState.AddModelError("SelectedDepartment", "Department not found.");
                    return View(obj);
                }



                //convert contract type to string.
                var contracttype_string = Enum.GetName(typeof(Predefined_ContractTypes), obj.ContractType);

                var contract = new Contract
                {
                    ContractType = contracttype_string,
                    RequesterName = obj.RequesterName,
                    EffectiveDate = obj.StartDate,
                    EndDate = obj.EndDate,
                    Status = ContractStatus.Draft,
                    IsApprovalSent = false,
                    AppUserId = await GetCurrentUserId(),
                    DepartmentId = department.Id,



                };
                _context.Contracts.Add(contract);
                await _context.SaveChangesAsync();

                int contractId = contract.Id; // take the contractid of that newly created contract
                obj.ContractId = contractId;//to use it in updating the contract in summary iaction
                var paymentstructure_string=Enum.GetName(typeof(Predefined_PaymentStructure), obj.PaymentStructure);
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

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred.contractfile is empty");
                    return View(obj); // if there are errors return the model 
                }

                TempData["success"] = "Contract Creation Ok";
                return RedirectToAction("ContractSummary", new { id = obj.ContractId }); // if successful also pass the contracid 
            }
            // If ModelState is invalid, loop through model state errors and log them
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    // Log or inspect the error message
                   // Console.WriteLine($"Error: {error.ErrorMessage}");
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }
            
           
            return View(obj); // if there are errors return the model 
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


        [HttpGet]
        public async Task<IActionResult> ContractSummary(int id)
        {
            var departments = _context.Departments.ToList();
            ViewBag.Departments = new SelectList(departments, "Id", "DepartmentName");
            var contract = await _context.Contracts //get that specific contract and include all related entries
                       .Include(c => c.Payments!)
              .Include(c => c.ContractParties!).ThenInclude(cp => cp.Party!) //include also the parties related to the contract
              .Include(c => c.TerminationClause!)
              .FirstOrDefaultAsync(c => c.Id == id);

            if (contract == null)
            {
                return NotFound();
            }

            //create a view model and get the details of that specific contract
            var viewModel = new CreateContractVm
            {
                ContractId = contract.Id,

                ContractType = (Predefined_ContractTypes)Enum.Parse(typeof(Predefined_ContractTypes),contract.ContractType),
                RequesterName = contract.RequesterName,
                StartDate = contract.EffectiveDate,
                EndDate = contract.EndDate,
                SelectedDepartment = contract.DepartmentId,
                ContractValue = (decimal)(contract.Payments?.FirstOrDefault()?.Amount ?? 0),
               // PaymentStructure = contract.Payments?.FirstOrDefault()?.PaymentStructure,
                PartyName = contract.ContractParties?.FirstOrDefault()?.Party?.PartyName,
                Email = contract.ContractParties?.FirstOrDefault()?.Party?.Email,
                TerminationNoticePeriod = (int)(contract.TerminationClause?.NoticePeriod ?? 0)
            };
            ViewBag.thiscontract = viewModel.ContractId;
            TempData["CurrentContract"] = viewModel.ContractId;
            TempData["PartyEmail"] = viewModel.Email;
            // Check if TempData["IsReadOnly"] is true and set ViewBag.IsReadOnly accordingly
            ViewBag.IsReadOnly = TempData["IsReadOnly"] as bool? ?? false;
            return View(viewModel);


        }

        [HttpPost]
        public async Task<IActionResult> ContractSummary(CreateContractVm obj)
        {
            if (!ModelState.IsValid)
            {
                // If ModelState is invalid, loop through model state errors and log them
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        // Log or inspect the error message
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }
                return View("ContractSummary", obj);
            }


            // Update the contract details in the database
            var contract = await _context.Contracts.FirstOrDefaultAsync(c => c.Id == obj.ContractId);

            if (contract == null)
            {
                return NotFound();
            }

            // Update the contract entity with the data from the view model

            contract.RequesterName = obj.RequesterName;
            contract.EffectiveDate = obj.StartDate;
            contract.EndDate = obj.EndDate;
            contract.DepartmentId = obj.SelectedDepartment;

            // Update other contract properties as needed...
            // Update the contract entity with the data from the view model
            if (obj.ContractType != null)
            {
                contract.ContractType =Enum.GetName(typeof(Predefined_ContractTypes), obj.ContractType);
            }
            contract.RequesterName = obj.RequesterName;
            contract.EffectiveDate = obj.StartDate;
            contract.EndDate = obj.EndDate;
            contract.DepartmentId = obj.SelectedDepartment;

            // Update other related entities
            var payment = contract.Payments?.FirstOrDefault(); // call payments if property not null
            if (payment != null)
            {
                payment.PaymentStructure = Enum.GetName(typeof(Predefined_PaymentStructure), obj.PaymentStructure);
                payment.Amount = obj.ContractValue;
            }

            var party = contract.ContractParties?.FirstOrDefault()?.Party;
            if (party != null)
            {
                party.PartyName = obj.PartyName;
                party.Email = obj.Email;
            }

            if (contract.TerminationClause != null)
            {
                contract.TerminationClause.NoticePeriod = obj.TerminationNoticePeriod;
            }

            // Save the changes to the database
            await _context.SaveChangesAsync();
            // to disable inputs after successful updates,used to send data across multiple redirects
            TempData["IsReadOnly"] = true;

            // Set a success message
            TempData["SuccessMessage"] = "Contract details updated successfully.";
            return RedirectToAction("ContractSummary", new { id = obj.ContractId });



        }

        [HttpPost]
        public IActionResult EmailToNegotiator(string email, int contractid)
        {
            var message = "Please review the document and sign";
            var subject = "Contract Review";

            if (!string.IsNullOrEmpty(email))
            {

                var doc = _context.ContractDocuments.FirstOrDefault(e => e.ContractId == contractid);
                if (doc == null)
                {

                    return NotFound();
                }
                byte[] attachment = doc.FileData;
                var filename = "ContractDoc.docx"; // client will receive the file as word document

                SendEmail(email, subject, message, attachment, filename);


                return RedirectToAction("EmailSentSuccessfully");
            }

            // Handle the case where email is null or empty
            return BadRequest("Email is required");



        }
        public IActionResult EmailSentSuccessfully()
        {
            return View();

        }

        private async Task<IActionResult> SendEmail(string email, string subject, string message,
             byte[] Document, string documentname)
        {


            try
            {
                await _emailService.SendEmailAsync(email, subject, message, Document, documentname);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

        }




        public IActionResult Getapprovers()
        {

            var approvers = _getApproversService.GetApprovers();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ActiveContracts()
        {
             List<Contract> activecontracts= await _context.Contracts.Where(c=>c.Status==ContractStatus.Active).ToListAsync();
            return View(activecontracts);
        }
        [HttpGet]
        public async Task<IActionResult> DraftContracts()
        {
            List<Contract> draftcontracts = await _context.Contracts.Where(c => c.Status == ContractStatus.Draft).ToListAsync();
            return View(draftcontracts);
        }
        [HttpGet]
        public async Task<IActionResult> PendingContracts()
        {
            List<Contract> pendingcontracts = await _context.Contracts.Where(c => c.Status == ContractStatus.Pending).ToListAsync();
            return View(pendingcontracts);
        }
        [HttpGet]
        public async Task<IActionResult> ExpiredContracts()
        {
            List<Contract> expiredcontracts = await _context.Contracts.Where(c => c.Status == ContractStatus.Expired).ToListAsync();
            return View(expiredcontracts);
        }
        [HttpGet]
        public async Task<IActionResult> RejectedContracts()
        {
            List<Contract> rejectedcontracts = await _context.Contracts.Where(c => c.Status == ContractStatus.Rejected).ToListAsync();
            return View(rejectedcontracts);
        }


        [HttpPost]
        public async Task<IActionResult> Collaboration([FromBody] EmailRequest model)
        {
            var emailService = new EmailService(_configuration); // Or inject via constructor
            try
            {
                await emailService.SendEmailAsync(model.To, model.Subject, model.Body);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class EmailRequest
        {
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var contract = await _context.Contracts
                .Include(c => c.ContractParties) // Include related ContractParties
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract == null)
            {
                return NotFound();
            }

            // Remove related ContractParties
            _context.ContractParties.RemoveRange(contract.ContractParties);

            // Remove the contract itself
            _context.Contracts.Remove(contract);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Contract deleted successfully.";
            return RedirectToAction("DraftContracts");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateContractStatus(int contractId, ContractStatus newStatus)
        {
            // Call service method to update status
            _contractService.UpdateContractStatus(contractId, newStatus);

            // Optionally, redirect to a summary page or back to the list
            return RedirectToAction("ContractSummary", new { id = contractId });
        }
    }


}

   