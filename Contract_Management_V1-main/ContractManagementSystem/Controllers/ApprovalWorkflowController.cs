using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagementSystem.Controllers
{
    public class ApprovalWorkflowController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ApplicationDbContext _context;
        private string signature;
        private string comments;

        public ApprovalWorkflowController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public ActionResult SubmitAprovalRequest(int ApproverId)
        {
            var ApprovalProcess = new ApprovalProcess
            {
                ApproverId = ApproverId,
                ContractStatus = "Approved",
                ApproverSignature = signature,
                ApproverComments = comments

            };
            _context.ApprovalProcesses.Add(ApprovalProcess);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public ActionResult ApprovalStatus(int ApproverId, bool isApproved)
        {

            var clearanceProcess = _context.ApprovalProcesses.Find(ApproverId);

            if (isApproved)
            {
                ApprovalProcess.Status = "Approved by x"; // Update status
                                                          // Notify next approver ('y') via email or other means
            }
            else
            {
                ApprovalProcess.Status = "Rejected by x"; // Update status
                                                          // Notify employee and allow resubmission
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }
    }
}