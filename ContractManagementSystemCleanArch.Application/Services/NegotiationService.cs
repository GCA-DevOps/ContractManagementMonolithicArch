using CMS.Application.DTOs.Request;
using CMS.Application.Interfaces.NegotiationInterfaces;
using CMS.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CMS.Application.Services
{
    // Service class implementing the business logic for negotiations
    public class NegotiationsService : INegotiationService
    {
        private readonly INegotiationsRepository _negotiationsRepository;
        private readonly IEmailService _emailService;

        // Constructor for dependency injection
        public NegotiationsService(INegotiationsRepository negotiationsRepository, IEmailService emailService)
        {
            _negotiationsRepository = negotiationsRepository;
            _emailService = emailService;
        }


        // Get a negotiator by name
        public async Task<Negotiation> GetNegotiatorByNameAsync(string name)
        {
            var negotiators = await _negotiationsRepository.GetAllAsync();
            return negotiators.FirstOrDefault(n => n.Name == name);
        }

        // Add a new negotiator
        public async Task<bool> AddNegotiatorAsync(NegotiationsDto negotiationDto)
        {
            var negotiation = new Negotiation
            {
                Name = negotiationDto.FirstName,
                DocumentData = await ProcessFile(negotiationDto.DocumentData),

                DocumentName = negotiationDto.DocumentName,
                Email = negotiationDto.Email,

            };



            var result = await _negotiationsRepository.AddAsync(negotiation);

            //// Assuming the document is part of the Negotiation entity and named as DocumentData and DocumentName
            //if (negotiator.DocumentData != null && !string.IsNullOrEmpty(negotiator.DocumentName))
            //{
            //    await _emailService.SendEmailAsync(negotiator.Email, "Negotiation Document", "Please find the attached document.", negotiator.DocumentData, negotiator.DocumentName);
            //}
            if (result)
            {
                return true;
            }
            return false;
        }

        private async Task<byte[]> ProcessFile(IFormFile file)
        {
            byte[] Documentbytes;
            using (var memorystream = new MemoryStream())
            {
                await file.CopyToAsync(memorystream);
                Documentbytes = memorystream.ToArray();
            };
            return Documentbytes;
        }

        // Remove an existing negotiator
        public async Task RemoveNegotiatorAsync(Negotiation negotiator)
        {
            await _negotiationsRepository.DeleteAsync(negotiator.Id);
        }

        // Get a negotiator by ID
        public async Task<Negotiation> GetNegotiatorByIdAsync(int id)
        {
            return await _negotiationsRepository.GetByIdAsync(id);
        }

        // Get all negotiators
        public async Task<List<Negotiation>> GetAllNegotiatorsAsync()
        {
            return await _negotiationsRepository.GetAllAsync();
        }

        // Update an existing negotiator
        public async Task UpdateNegotiatorAsync(Negotiation negotiator)
        {
            var existingNegotiator = await _negotiationsRepository.GetByIdAsync(negotiator.Id);
            if (existingNegotiator == null)
            {
                throw new Exception("Negotiator not found");
            }

            // Update logic here
            await _negotiationsRepository.UpdateAsync(negotiator);
        }

        public Task RemoveNegotiatorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool EmailExist(string email)
        {
            var result = _negotiationsRepository.ExistsEmail(email);
            if (result)
            {
                return true;
            }
            return false;
        }
    }
}
