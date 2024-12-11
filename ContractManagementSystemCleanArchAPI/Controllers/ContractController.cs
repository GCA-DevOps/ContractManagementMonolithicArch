using AutoMapper;
using CMS.Application.DTOs.Request;
using CMS.Application.Interfaces.ContractInterfaces;
using CMS.Application.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;
        
        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet("{id}")]
       
        //[ServiceFilter(typeof(ValidateContractId))]
        public IActionResult GetContractById(int id)
        {
            var contract = _contractService.GetById(id);
            return Ok(contract);
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateContract([FromForm] CreateContractDto contractdto)
        {
            if(!ModelState.IsValid)
            {
              
               return BadRequest(ModelState);
            }
            // Attempt to add the contract
            bool isSuccess = await _contractService.AddContract(contractdto);
            if(isSuccess)
            {
                return Ok("contract added successfully");
            }
           return BadRequest("failure to create contract");
        }

        
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateContractId))]
        public async Task<IActionResult> DeleteContract(int id )
        {

            var result = await _contractService.DeleteContract(id);
            if(result)
            {
                return Ok("Contract Deleted Successfully");
            }
            return BadRequest("Failed to delete the Contract");
        }
    }
}