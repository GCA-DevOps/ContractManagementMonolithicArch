using Microsoft.AspNetCore.Mvc;
using CMS.Domain.Entities;
using System.Collections.Generic;
using CMS.Application.UseCase.ContractReportsUseCase;

namespace CMSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ReportsController : ControllerBase
    {
        private readonly GetContractsByStatusUseCase _getContractsByStatusUseCase;
        private readonly GetContractsByTypeUseCase _getContractsByTypeUseCase;
        private readonly GetContractsByDepartmentUseCase _getContractsByDepartmentUseCase;
        private readonly GetContractsByValueUseCase _getContractsByValueUseCase;
        private readonly GetContractsByPartiesUseCase _getContractsByPartiesUseCase;
        private readonly GetCustomContractsReportUseCase _getCustomContractsReportUseCase;

        public ReportsController(
            GetContractsByStatusUseCase getContractsByStatusUseCase,
            GetContractsByTypeUseCase getContractsByTypeUseCase,
            GetContractsByDepartmentUseCase getContractsByDepartmentUseCase,
            GetContractsByValueUseCase getContractsByValueUseCase,
            GetContractsByPartiesUseCase getContractsByPartiesUseCase,
            GetCustomContractsReportUseCase getCustomContractsReportUseCase)
        {
            _getContractsByStatusUseCase = getContractsByStatusUseCase;
            _getContractsByTypeUseCase = getContractsByTypeUseCase;
            _getContractsByDepartmentUseCase = getContractsByDepartmentUseCase;
            _getContractsByValueUseCase = getContractsByValueUseCase;
            _getContractsByPartiesUseCase = getContractsByPartiesUseCase;
            _getCustomContractsReportUseCase = getCustomContractsReportUseCase;
        }

        [HttpGet("status")]
        public IActionResult GetContractsByStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return BadRequest("Status is required.");
            }

            var contracts = _getContractsByStatusUseCase.Execute(status);
            return Ok(contracts);
        }

        [HttpGet("type")]
        public IActionResult GetContractsByType(string type)
        {
            var contracts = _getContractsByTypeUseCase.Execute(type);
            return Ok(contracts);
        }

        [HttpGet("department")]
        public IActionResult GetContractsByDepartment(string department)
        {
            var contracts = _getContractsByDepartmentUseCase.Execute(department);
            return Ok(contracts);
        }

        [HttpGet("value")]
        public IActionResult GetContractsByValue(decimal minValue, decimal maxValue)
        {
            var contracts = _getContractsByValueUseCase.Execute(minValue, maxValue);
            return Ok(contracts);
        }

        [HttpGet("parties")]
        public IActionResult GetContractsByParties(string party)
        {
            var contracts = _getContractsByPartiesUseCase.Execute(party);
            return Ok(contracts);
        }

        [HttpGet("custom")]
        public IActionResult GetCustomContractsReport([FromQuery] string status, [FromQuery] string type, [FromQuery] string department, [FromQuery] decimal? minValue, [FromQuery] decimal? maxValue, [FromQuery] string party)
        {
            Func<ContractsReport, bool> predicate = c =>
            {
                bool match = true;
                if (!string.IsNullOrEmpty(status))
                    match &= c.Status == status;
                if (!string.IsNullOrEmpty(type))
                    match &= c.Type == type;
                if (!string.IsNullOrEmpty(department))
                    match &= c.Department == department;
                if (minValue.HasValue)
                    match &= c.Value >= minValue.Value;
                if (maxValue.HasValue)
                    match &= c.Value <= maxValue.Value;
                if (!string.IsNullOrEmpty(party))
                    match &= c.Parties.Contains(party);
                return match;
            };

            var contracts = _getCustomContractsReportUseCase.Execute(predicate);
            return Ok(contracts);
        }
    }
}
