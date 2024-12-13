/*using ContractManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace ContractManagementSystem.Models
{
    public interface IContractApprovalService
    {

        Task<bool> ApproveContract(int contractId, string approverId); //returns a boolean if contract approved
        //or not
    }
    public class ContractApprovalProcess : IContractApprovalService
    {

        private readonly ApplicationDbContext _context;
        public ContractApprovalProcess(ApplicationDbContext context)//dependency injection
            
        {
            _context = context;
        }

        public async Task<bool>ApproveContract(int contractId,string approverId)
            //returns contract id and also related navigation information from approvals table where the contractid matches
            //in contract table matches in the approvals table
        {
            
           var contract= await _context.Contracts.Include(c=>c.Approval).FirstOrDefaultAsync(c=>c.ContractId==contractId);  

            if(contract == null)
            {
                return false;

            }
            var PendingApproval=contract.Approval.FirstOrDefault(a=>!a.IsApproved); //finds first obj
            //that is marked as not approved
            if(PendingApproval == null)
            {
                return false; // no pending approvals
            }
            if (PendingApproval.ApproverId != approverId)
            {
                return false; // Current user is not the assigned approver
            }
            //approve the contract
            PendingApproval.IsApproved = true;
            PendingApproval.DateApproved = DateTime.UtcNow;

            // Check if all approvals are completed
            if (contract.Approval.All(a => a.IsApproved))
            {
                // Contract is fully approved, you can update its status or take other actions here
                var EntityUpdate = _context.Contracts.FirstOrDefault(i => i.ContractId == contractId);
                if (EntityUpdate != null)
                {
                    EntityUpdate.Status = ContractStatus.Active; // set the contract status to active
                    await _context.SaveChangesAsync();
                    
                }

            }
            return true;// if all approvers have approved the contract
            


        }

    }
}
*/