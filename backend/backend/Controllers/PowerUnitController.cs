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
                    Data = new
                    {
                        brand = powerUnit.Brand,
                        model = powerUnit.Model,
                        country = powerUnit.Country,
                        battery = powerUnit.Battery,
                        voltage = powerUnit.Voltage,
                        price = powerUnit.Price,
                        description = powerUnit.Description,
                        image = powerUnit.Image
                    }
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating PowerUnit");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPowerUnits()
        {
            try
            {
                var poweunits = await powerUnitRepository.GetAllPowerUnits();
                return Ok(poweunits);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all PowerUnits");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPowerUnitById(long id)
        {
            try
            {
                var powerUnit = await powerUnitRepository.GetPowerUnitById(id);

                if (powerUnit == null)
                {
                    return NotFound();
                }

                return Ok(powerUnit);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting PowerUnit");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePowerUnit(long id, PowerUnit updatePowerUnit)
        {

            try
            {
                var powerUnit = await powerUnitRepository.GetPowerUnitById(id);

                if (powerUnit == null)
                {
                    return NotFound();
                }

                powerUnit.Brand = updatePowerUnit.Brand;
                powerUnit.Model = updatePowerUnit.Model;
                powerUnit.Country = updatePowerUnit.Country;
                powerUnit.Battery = updatePowerUnit.Battery;
                powerUnit.Voltage = updatePowerUnit.Voltage;
                powerUnit.Price = updatePowerUnit.Price;
                powerUnit.Description = updatePowerUnit.Description;
                powerUnit.Image = updatePowerUnit.Image;

                await powerUnitRepository.UpdatePowerUnit(powerUnit);

                return Ok(new
                {
                    Component = "PowerUnit",
                    id = powerUnit.Id,
                    Data = new
                    {
                        brand = powerUnit.Brand,
                        model = powerUnit.Model,
                        country = powerUnit.Country,
                        battery = powerUnit.Battery,
                        voltage = powerUnit.Voltage,
                        price = powerUnit.Price,
                        description = powerUnit.Description,
                        image = powerUnit.Image
                    }
                });

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating PowerUnit");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePowerUnit(long id)
        {
            try
            {
                await powerUnitRepository.DeletePowerUnit(id);
                return Ok($"PowerUnit data with Index {id} deleted");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting PowerUnit");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
