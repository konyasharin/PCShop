using backend.Data;
using backend.Entities;
using backend.IRepositories;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AssemblyController : ControllerBase
    {
        private readonly ILogger<AssemblyController> logger;
        private readonly DataContext dataContext;
        private readonly IAssemblyRepository assemblyRepository;

        public AssemblyController(ILogger<AssemblyController> logger, DataContext dataContext,
            IAssemblyRepository assemblyRepository)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.assemblyRepository = assemblyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssemblies()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var assamblies = await assemblyRepository.GetAllAssemblies();
                return Ok(assamblies);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all ComputerCases");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssemblyById(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var assembly = await assemblyRepository.GetAssemblyById(id);

                if (assembly == null)
                {
                    return NotFound();
                }

                return Ok(assembly);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all ComputerCases");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("sortedByTime")]
        public async Task<IActionResult> GetAllAssembliesSortedByTimeAdded()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var assemblies = await assemblyRepository.GetAllAssembliesSortedByTimeAdded();
                return Ok(assemblies);
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all AssemblyControllers");
                return StatusCode(500, "Internal server error");
            }


        }

        [HttpPost("createassembly")]
        public async Task<IActionResult> CreateAssembly(Assembly assembly)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var processor = await dataContext.Processors.FindAsync(assembly.ProcessorId);
                var computerCase = await dataContext.ComputerCases.FindAsync(assembly.ComputerCaseId);
                var cooler = await dataContext.Coolers.FindAsync(assembly.CoolerId);
                var motherboard = await dataContext.MotherBoards.FindAsync(assembly.MotherBoardId);
                var powerUnit = await dataContext.PowerUnits.FindAsync(assembly.PowerUnitId);
                var ram = await dataContext.RAMs.FindAsync(assembly.RamId);
                var ssd = await dataContext.SSDs.FindAsync(assembly.SsdId);
                var videocard = await dataContext.VideoCards.FindAsync(assembly.VideoCardId);

                if (processor == null || computerCase == null || cooler == null
                    || motherboard == null || powerUnit == null || ram == null
                    || ssd == null || videocard == null)
                {
                    logger.LogError("Absence of one or all components");
                    return BadRequest("All components must be specified.");
                }

                await assemblyRepository.AddAssembly(assembly);

                logger.LogInformation("Assembly created with ID {AssemblyId}", assembly.Id);

                return Ok(new
                {
                    Component = "Assembly",
                    id = assembly.Id,
                    Data = new
                    {
                        name = assembly.Name,
                        price = assembly.Price,
                        computerCaseId = assembly.ComputerCaseId,
                        coolerId = assembly.CoolerId,
                        motherBoardId = assembly.MotherBoardId,
                        processorId = assembly.ProcessorId,
                        ramId = assembly.RamId,
                        ssdId = assembly.SsdId,
                        videocardId = assembly.VideoCardId,
                        powerUnitId = assembly.PowerUnitId,
                        likes = assembly.Likes,
                        creation_time = assembly.Creation_time,

                    }
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating Assembly");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssembly(long id, Assembly updatedAssembly)
        {
            try
            {
                var assembly = await assemblyRepository.GetAssemblyById(id);

                if (assembly == null)
                {
                    return NotFound();
                }

                assembly.Name = updatedAssembly.Name;
                assembly.Price = updatedAssembly.Price;
                assembly.ComputerCaseId = updatedAssembly.ComputerCaseId;
                assembly.CoolerId = updatedAssembly.CoolerId;
                assembly.MotherBoardId = updatedAssembly.MotherBoardId;
                assembly.ProcessorId = updatedAssembly.ProcessorId;
                assembly.RamId = updatedAssembly.RamId;
                assembly.SsdId = updatedAssembly.SsdId;
                assembly.VideoCardId = updatedAssembly.VideoCardId;
                assembly.PowerUnitId = updatedAssembly.PowerUnitId;
                assembly.Likes = updatedAssembly.Likes;
                assembly.Creation_time = updatedAssembly.Creation_time;

                await assemblyRepository.UpdateAssembly(assembly);

                return Ok(new
                {
                    Component = "Assembly",
                    id = assembly.Id,
                    Data = new
                    {
                        name = assembly.Name,
                        price = assembly.Price,
                        computerCaseId = assembly.ComputerCaseId,
                        coolerId = assembly.CoolerId,
                        motherBoardId = assembly.MotherBoardId,
                        processorId = assembly.ProcessorId,
                        ramId = assembly.RamId,
                        ssdId = assembly.SsdId,
                        videocardId = assembly.VideoCardId,
                        powerUnitId = assembly.PowerUnitId,
                        likes = assembly.Likes,
                        creation_time = assembly.Creation_time,

                    }
                });

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating ComputerCase");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssembly(long id)
        {

            try
            {
                await assemblyRepository.DeleteAssembly(id);
                return Ok($"Assembly data with Index {id} deleted");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting Assembly");
                return StatusCode(500, "Internal server error");
            }


        }
    }
}
