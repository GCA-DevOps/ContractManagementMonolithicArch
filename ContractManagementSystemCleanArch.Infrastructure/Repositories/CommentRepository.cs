using CMS.Application.Interfaces.ContractInterfaces;
using CMS.Domain.Entities.Approval;
using CMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetCommentsByContractIdAsync(int contractId)
        {
            return await _context.Comments.Where(c => c.ContractId == contractId).ToListAsync();

        }
    }
}
