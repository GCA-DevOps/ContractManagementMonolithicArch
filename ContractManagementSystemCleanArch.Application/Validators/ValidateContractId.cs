using CMS.Application.Interfaces.ContractInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CMS.Application.Validators
{
    public class ValidateContractId : ActionFilterAttribute
    {
        private readonly IContractService _contractService;

        public ValidateContractId(IContractService contractService)
        {
            _contractService = contractService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //await base.OnActionExecutionAsync(context, next);

            var contractId = context.ActionArguments["id"] as int?;
            if (contractId.HasValue)
            {
                if (contractId.Value < 0)
                {
                    context.ModelState.AddModelError("contractId", "contractId is invalid");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else
                {
                    bool exists = await _contractService.ContractExistsAsync(contractId.Value); // Await the Task<bool>
                    if (!exists)
                    {
                        context.ModelState.AddModelError("contractId", "contract doesn't exist");
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status404NotFound
                        };
                        context.Result = new BadRequestObjectResult(problemDetails);
                    }
                    else
                    {
                        await next(); // Continue executing the action if the contract exists
                    }
                }
            }
            else
            {
                await next();
            }

        }
    }
}

