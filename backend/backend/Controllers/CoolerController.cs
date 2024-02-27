using backend.Data;
using backend.Entities;
using backend.IRepositories;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoolerController : ControllerBase
    {
        private readonly ILogger<CoolerController> logger;
        private readonly DataContext dataContext;
        private readonly ICoolerRepository coolerRepository;
    

        public CoolerController(ILogger<CoolerController> logger, DataContext dataContext,
            ICoolerRepository сoolerRepository)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.coolerRepository = сoolerRepository;
        }

        [HttpPost("createcooler")]
        public async Task<IActionResult> CreateCooler(Cooler cooler)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await coolerRepository.AddCooler(cooler);

                logger.LogInformation("Cooler created with ID {CoolerId}", cooler.Id);

                return Ok(new
                {
                    Component = "Cooler",
                    id = cooler.Id,
                    Data = new
                    {
                        brand = cooler.Brand,
                        model = cooler.Model,
                        country = cooler.Country,
                        speed = cooler.Speed,
                        power = cooler.Power,
                        price = cooler.Price,
                        description = cooler.Description,
                        image = cooler.Image
                    }
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
                var coolers = await coolerRepository.GetAllCoolers();
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
                var cooler = await coolerRepository.GetCoolerById(id);

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
        public async Task<IActionResult> UpdateCooler(long id, Cooler updatedCooler)
        {
            try
            {
       
                var cooler = await coolerRepository.GetCoolerById(id);

                if (cooler == null)
                {
                    return NotFound(); 
                }

               
                cooler.Brand = updatedCooler.Brand;
                cooler.Model = updatedCooler.Model;
                cooler.Country = updatedCooler.Country;
                cooler.Speed = updatedCooler.Speed;
                cooler.Power = updatedCooler.Power;
                cooler.Price = updatedCooler.Price;
                cooler.Description = updatedCooler.Description;
                cooler.Image = cooler.Image;
                    
                await coolerRepository.UpdateCooler(cooler);


                return Ok(new
                {
                    Component = "Cooler",
                    id = cooler.Id,
                    Data = new
                    {
                        brand = cooler.Brand,
                        model = cooler.Model,
                        country = cooler.Country,
                        speed = cooler.Speed,
                        power = cooler.Power,
                        price = cooler.Price,
                        description = cooler.Description,
                        image = cooler.Image
                    }
                });
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
                await coolerRepository.DeleteCooler(id);
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
