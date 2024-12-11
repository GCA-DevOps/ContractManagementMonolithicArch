using CMS.Application.Interfaces.CollaborationInterfaces;
using CMS.Domain.Entities;
using CMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CMS.Infrastructure.Repositories
{
    public class CollaborationRepository : ICollaborationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CollaborationRepository> _logger;

        public CollaborationRepository(ApplicationDbContext context, ILogger<CollaborationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Collaboration> GetByIdAsync(int id)
        {
            return await _context.Collaboration.FindAsync(id);
        }

        public async Task<List<Collaboration>> GetAllAsync()
        {
            return await _context.Collaboration.ToListAsync();
        }

        public async Task<bool> AddAsync(Collaboration collaboration)
        {
            try
            {
                _context.Collaboration.Add(collaboration);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding collaboration to the database");
                return false;
            }
        }

        public async Task DeleteAsync(Collaboration collaboration)
        {
            _context.Collaboration.Remove(collaboration);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Collaboration collaboration)
        {
            _context.Collaboration.Update(collaboration);
            await _context.SaveChangesAsync();
        }

        public bool ExistsEmail(string email)
        {
            return _context.Collaboration.Any(c => c.Email == email);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
