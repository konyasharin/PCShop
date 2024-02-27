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

        [HttpPost("createprocessor")]
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
                    Data = new
                    {
                        brand = processor.Brand,
                        model = processor.Model,
                        country = processor.Country,
                        clock_frequency = processor.Clock_frequency,
                        turbo_frequency = processor.Turbo_frequency,
                        heat_dissipation = processor.Heat_dissipation,
                        price = processor.Price,
                        description = processor.Description,
                        image = processor.Image
                    }
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating Processor");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProcessors()
        {
            try
            {
                var processors = await processorRepository.GetAllProcessors();
                return Ok(processors);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all Processors");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProcessorById(long id)
        {
            try
            {
                var processor = await processorRepository.GetProcessorById(id);

                if (processor == null)
                {
                    return NotFound();
                }

                return Ok(processor);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting Processor");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProcessor(long id, Processor updatedProcessor)
        {
           
            try
            {
                var processor = await processorRepository.GetProcessorById(id);

                if(processor == null)
                {
                    return NotFound();
                }

                processor.Brand = updatedProcessor.Brand;
                processor.Model = updatedProcessor.Model;
                processor.Country = updatedProcessor.Country;
                processor.Clock_frequency = updatedProcessor.Clock_frequency;
                processor.Turbo_frequency = updatedProcessor.Turbo_frequency;
                processor.Heat_dissipation = updatedProcessor.Heat_dissipation;
                processor.Price = updatedProcessor.Price;
                processor.Description = updatedProcessor.Description;
                processor.Image = updatedProcessor.Image;

                await processorRepository.UpdateProcessor(processor);

                return Ok(new
                {
                    Component = "PowerUnit",
                    id = processor.Id,
                    Data = new
                    {
                        brand = processor.Brand,
                        model = processor.Model,
                        country = processor.Country,
                        clock_frequency = processor.Clock_frequency,
                        turbo_frequency = processor.Turbo_frequency,
                        heat_dissipation = processor.Heat_dissipation,
                        price = processor.Price,
                        description = processor.Description,
                        image = processor.Image
                    }
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating Processor");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcessor(long id)
        {
            try
            {
                await processorRepository.DeleteProcessor(id);
                return Ok($"Processor data with Index {id} deleted");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting Processor");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
