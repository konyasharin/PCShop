using backend.Data;
using backend.Entities;
using backend.IRepositories;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoolerController : ControllerBase
    {
        private readonly ILogger<CoolerController> logger;
        private readonly DataContext dataContext;
        private readonly ICoolerRepository сoolerRepository;

        public CoolerController(ILogger<CoolerController> logger, DataContext dataContext,
            ICoolerRepository сoolerRepository)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.сoolerRepository = сoolerRepository;
        }

        [HttpPost("createCooler")]
        public async Task<IActionResult> CreateCooler(Cooler сooler)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await сoolerRepository.AddCooler(сooler);

                logger.LogInformation("Cooler created with ID {CoolerId}", сooler.Id);

                return Ok(new
                {
                    Component = "Cooler",
                    id = сooler.Id,
                    Data = сooler
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating Cooler");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoolers()
        {
            try
            {
                var coolers = await сoolerRepository.GetAllCoolers();
                return Ok(coolers);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all Coolers");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoolerById(long id)
        {
            try
            {
                var cooler = await сoolerRepository.GetCoolerById(id);

                if (cooler == null)
                {
                    return NotFound();
                }

                return Ok(cooler);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting Cooler");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCooler(long id, Cooler cooler)
        {
            if (id != cooler.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await сoolerRepository.UpdateCooler(cooler);
                return Ok($"Cooler data with Index {id} updated");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating Cooler");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCooler(long id)
        {
            try
            {
                await сoolerRepository.DeleteCooler(id);
                return Ok($"Cooler data with Index {id} deleted");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting Cooler");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
