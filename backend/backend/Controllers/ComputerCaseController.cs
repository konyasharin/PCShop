using backend.Data;
using backend.Entities;
using backend.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerCaseController : ControllerBase
    {
        private readonly ILogger<ComputerCaseController> logger;
        private readonly DataContext dataContext;
        private readonly IComputerCaseRepository computerCaseRepository;

        public ComputerCaseController(ILogger<ComputerCaseController> logger, DataContext dataContext,
            IComputerCaseRepository computerCaseRepository)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.computerCaseRepository = computerCaseRepository;
        }

        [HttpPost("createComputerCase")]
        public async Task<IActionResult> CreateComputerCase(ComputerCase computerCase)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await computerCaseRepository.AddComputerCase(computerCase);

                logger.LogInformation("ComputerCase created with ID {ComputerCaseId}", computerCase.Id);

                return Ok(new
                {
                    Component = "ComputerCase",
                    id = computerCase.Id,
                    Data = computerCase
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating ComputerCase");
                return StatusCode(500, "Internal server error");
            }
        }
    }


}
