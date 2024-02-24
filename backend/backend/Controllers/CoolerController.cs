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
    }
}
