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
    public class PowerUnitController : ControllerBase
    {
        private readonly ILogger<PowerUnitController> logger;
        private readonly DataContext dataContext;
        private readonly IPowerUnitRepository powerUnitRepository;

        public PowerUnitController(ILogger<PowerUnitController> logger, DataContext dataContext,
            IPowerUnitRepository powerUnitRepository)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.powerUnitRepository = powerUnitRepository;
        }

        [HttpPost("createPowerUnit")]
        public async Task<IActionResult> CreatePowerUnit(PowerUnit powerUnit)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await powerUnitRepository.AddPowerUnit(powerUnit);

                logger.LogInformation("PowerUnit created with ID {PowerUnitId}", powerUnit.Id);

                return Ok(new
                {
                    Component = "PowerUnit",
                    id = powerUnit.Id,
                    Data = powerUnit
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating PowerUnit");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
