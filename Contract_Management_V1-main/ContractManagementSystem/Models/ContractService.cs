using ContractManagementSystem.Data;
using ContractManagementSystem.Models;

public class ContractService
{
    private readonly ApplicationDbContext _context;

    public ContractService(ApplicationDbContext db)
    {
        _context = db;
    }

    public void UpdateContractStatus(int contractId, ContractStatus newStatus)
    {
        var contract = _context.Contracts.Find(contractId);
        if (contract != null)
        {
            contract.Status = newStatus;
            _context.SaveChanges();
        }
    }

    public (int ActiveCount, int PendingCount, int RejectedCount, int ExpiredCount, int DraftCount) GetContractCount()
    {
        var activeCount = _context.Contracts.Count(c => c.Status == ContractStatus.Active);
        var pendingCount = _context.Contracts.Count(c => c.Status == ContractStatus.Pending);
        var rejectedCount = _context.Contracts.Count(c => c.Status == ContractStatus.Rejected);
        var expiredCount = _context.Contracts.Count(c => c.Status == ContractStatus.Expired);
        var draftCount = _context.Contracts.Count(c => c.Status == ContractStatus.Draft);
        return (activeCount, pendingCount, rejectedCount, expiredCount, draftCount);
    }
}
