using backend.Data;
using backend.Entities;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api")]
    [ApiController]
    public class ComputerCaseController : ControllerBase
    {
        private readonly ILogger<ComputerCaseController> logger;
        private readonly DataContext dataContext;

        public ComputerCaseController(ILogger<ComputerCaseController> logger, DataContext dataContext)
        {
            this.logger = logger;
            this.dataContext = dataContext;
        }

        [HttpPost("createComponent/ComputerCase")]
        public async Task<IActionResult> CreateComputerCase(ComputerCase computerCase)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                dataContext.ComputerCases.Add(computerCase);
                await dataContext.SaveChangesAsync();

                logger.LogInformation("ComputerCase created with ID {ComputerCaseId}", computerCase.Id);

                return Ok(new
                {
                    EntityName = "ComputerCase",
                    Data = computerCase
                });

            }

            catch(Exception ex)
            {
                logger.LogError(ex, "Error creating ComputerCase");
                return StatusCode(500, "Internal server error");
            }
        }


        
    }


}
