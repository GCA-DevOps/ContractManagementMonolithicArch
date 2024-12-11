using CMS.Domain.Interfaces;
using CMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Repositories
{
    public class NegotiationsRepository : INegotiationsRepository
    {
        private readonly ApplicationDbContext _context;

        public NegotiationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool ExistsEmail(string email)
        {
            return _context.Negotiations.Any(n => n.Email == email);
        }

        public async Task<bool> AddAsync(Negotiation negotiator)
        {
            try
            {
                _context.Negotiations.Add(negotiator);
                await _context.SaveChangesAsync();

            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<List<Negotiation>> GetAllAsync()
        {
            return await _context.Negotiations.ToListAsync();
        }

        public async Task<Negotiation> GetByIdAsync(int id)
        {
            return await _context.Negotiations.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var negotiator = await _context.Negotiations.FindAsync(id);
            if (negotiator == null)
            {
                return false;
            }

            _context.Negotiations.Remove(negotiator);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(Negotiation negotiator)
        {
            _context.Negotiations.Update(negotiator);
            await _context.SaveChangesAsync();
        }
    }
}
