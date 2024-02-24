using backend.Data;
using backend.Entities;
using backend.IRepositories;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamController : ControllerBase
    {
        private readonly ILogger<RamController> logger;
        private readonly DataContext dataContext;
        private readonly IRamRepository ramRepository;

        public RamController(ILogger<RamController> logger, DataContext dataContext,
            IRamRepository ramRepository)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.ramRepository = ramRepository;
        }

        [HttpPost("createRam")]
        public async Task<IActionResult> CreateRam(RAM ram)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await ramRepository.AddRam(ram);

                logger.LogInformation("Ram created with ID {RamId}", ram.Id);

                return Ok(new
                {
                    Component = "Ram",
                    id = ram.Id,
                    Data = ram
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating Ram");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
