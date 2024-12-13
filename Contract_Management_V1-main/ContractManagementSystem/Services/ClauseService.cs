using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContractManagementSystem.Services
{
    public class ClauseService
    {
        private readonly ApplicationDbContext _context;

        public ClauseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ClauseLibrary> GetClauses(int pageNumber, int pageSize)
        {
            return _context.ClauseLibrary
                        .OrderBy(c => c.Id) // Assuming Id is unique and primary key
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
        }

        public bool DeleteClause(int id)
        {
            var clause = _context.ClauseLibrary.Find(id);
            if (clause != null)
            {
                _context.ClauseLibrary.Remove(clause);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
