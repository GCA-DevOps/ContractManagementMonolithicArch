using CMS.Application.DTOs.Request.Approval;
using CMS.Application.Interfaces.ApprovalInterfaces;
using CMS.Domain.Entities.Approval;
using CMS.Domain.Entities.Authentication;
using CMS.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CMS.Infrastructure.Repositories
{
    public class ApproverRepository : IApproverRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<ApproverRepository> _logger;

        public ApproverRepository(ApplicationDbContext context, UserManager<AppUser> userManager, ILogger<ApproverRepository> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Approver> AddAsync(Approver approver)
        {
            var user = await _userManager.FindByEmailAsync(approver.Email!);

            if (user == null)
            {
                _logger.LogWarning($"Attempted to add approver for non-existent user: {approver.Email}");
                throw new InvalidOperationException("User does not exist.");
            }

            await _context.Approvers.AddAsync(approver);
            await _context.SaveChangesAsync();
            return approver;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var approver = await _context.Approvers.FindAsync(id);

            if (approver != null)
            {
                _context.Approvers.Remove(approver);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ApproverDTO>> GetAllApproversAsync()
        {
            return await _context.Approvers.Select(a => new ApproverDTO
            {
                Id = a.ApproverId,
                Email = a.Email
            }).ToListAsync();
        }

        public async Task<Approver> GetByIdAsync(int id)
        {
            return await _context.Approvers
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.ApproverId == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Approver approver)
        {
            _context.Entry(approver).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
