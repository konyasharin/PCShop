using backend.Data;
using backend.Entities;
using backend.IRepositories;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace backend.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost("createSsd")]
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
                    Data = ssd
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating Ssd");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
