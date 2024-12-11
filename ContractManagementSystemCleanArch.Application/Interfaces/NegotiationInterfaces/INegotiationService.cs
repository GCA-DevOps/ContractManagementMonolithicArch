using CMS.Application.DTOs.Request;

namespace CMS.Application.Interfaces.NegotiationInterfaces
{
    public interface INegotiationService
    {
        Task<Negotiation> GetNegotiatorByIdAsync(int id); // Returns a single Negotiation by ID

        Task<List<Negotiation>> GetAllNegotiatorsAsync(); // Returns all Negotiations
        Task<bool> AddNegotiatorAsync(NegotiationsDto negotiatordto); // Adds a new Negotiation and returns the created Negotiation
        Task UpdateNegotiatorAsync(Negotiation negotiator); // Updates an existing Negotiation
        Task RemoveNegotiatorAsync(int id); // Removes a Negotiation by ID
        Task RemoveNegotiatorAsync(Negotiation negotiator);
        bool EmailExist(string email);
    }
}