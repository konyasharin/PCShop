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

        [HttpGet]
        public async Task<IActionResult> GetAllComputerCases()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var computerCases = await computerCaseRepository.GetAllComputerCases();
                return Ok(computerCases);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all ComputerCases");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComputerCaseById(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var computerCase = await computerCaseRepository.GetComputerCaseById(id);

                if (computerCase == null)
                {
                    return NotFound();
                }

                return Ok(computerCase);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all ComputerCases");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComputerCase(long id, ComputerCase computerCase)
        {
            if (id != computerCase.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await computerCaseRepository.UpdateComputerCase(computerCase);
                return Ok($"ComputerCase data with Index {id} updated");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating ComputerCase");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputerCase(long id)
        {

            try
            {
                await computerCaseRepository.DeleteComputerCase(id);
                return Ok($"ComputerCase data with Index {id} deleted");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting ComputerCase");
                return StatusCode(500, "Internal server error");
            }



        }
    }

}
