using backend.Data;
using backend.Entities;
using backend.IRepositories;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SsdController : ControllerBase
    {
        private readonly ILogger<SsdController> logger;
        private readonly DataContext dataContext;
        private readonly ISsdRepository ssdRepository;

        public SsdController(ILogger<SsdController> logger, DataContext dataContext,
            ISsdRepository ssdRepository)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.ssdRepository = ssdRepository;
        }

        [HttpPost("createssd")]
        public async Task<IActionResult> CreateSsd(SSD ssd)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await ssdRepository.AddSsd(ssd);

                logger.LogInformation("Ssd created with ID {SsdId}", ssd.Id);

                return Ok(new
                {
                    Component = "Ssd",
                    id = ssd.Id,
                    Data = new
                    {
                        brand = ssd.Brand,
                        model = ssd.Model,
                        country = ssd.Country,
                        capacity = ssd.Capacity,
                        price = ssd.Price,
                        description = ssd.Description,
                        image = ssd.Image
                    }
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating Ssd");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSsds()
        {
            try
            {
                var ssds = await ssdRepository.GetAllSsds();
                return Ok(ssds);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all SSDs");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSsdById(long id)
        {
            try
            {
                var ssd = await ssdRepository.GetSsdById(id);

                if (ssd == null)
                {
                    return NotFound();
                }

                return Ok(ssd);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting SSD");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRam(long id, SSD updateSsd)
        {
           
            try
            {
                var ssd = await ssdRepository.GetSsdById(id);

                if (ssd == null)
                {
                    return NotFound();
                }

                ssd.Brand = updateSsd.Brand;
                ssd.Model = updateSsd.Model;
                ssd.Country = updateSsd.Country;
                ssd.Capacity = updateSsd.Capacity;
                ssd.Price = updateSsd.Price;
                ssd.Description = updateSsd.Description;
                ssd.Image = updateSsd.Image;

                await ssdRepository.UpdateSsd(ssd);

                return Ok(new
                {
                    Component = "Ssd",
                    id = ssd.Id,
                    Data = new
                    {
                        brand = ssd.Brand,
                        model = ssd.Model,
                        country = ssd.Country,
                        capacity = ssd.Capacity,
                        price = ssd.Price,
                        description = ssd.Description,
                        image = ssd.Image
                    }
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating SSD");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSsd(long id)
        {
            try
            {
                await ssdRepository.DeleteSsd(id);
                return Ok($"SSD data with Index {id} deleted");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting SSD");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
