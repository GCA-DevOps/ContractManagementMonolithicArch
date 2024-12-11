namespace CMS.Domain.Interfaces
{
    public interface INegotiationsRepository
    {
        bool ExistsEmail(string email);
        Task<bool> AddAsync(Negotiation negotiation);
        Task<List<Negotiation>> GetAllAsync();
        Task<Negotiation> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task UpdateAsync(Negotiation negotiator); // New method for updating
    }
}
