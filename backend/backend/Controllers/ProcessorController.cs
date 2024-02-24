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
    public class ProcessorController : ControllerBase
    {
        private readonly ILogger<ProcessorController> logger;
        private readonly DataContext dataContext;
        private readonly IProcessorRepository processorRepository;

        public ProcessorController(ILogger<ProcessorController> logger, DataContext dataContext,
            IProcessorRepository processorRepository)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.processorRepository = processorRepository;
        }

        [HttpPost("createProcessor")]
        public async Task<IActionResult> CreateProcessor(Processor processor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await processorRepository.AddProcessor(processor);

                logger.LogInformation("Processor created with ID {ProcessorId}", processor.Id);

                return Ok(new
                {
                    Component = "PowerUnit",
                    id = processor.Id,
                    Data = processor
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating Processor");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
