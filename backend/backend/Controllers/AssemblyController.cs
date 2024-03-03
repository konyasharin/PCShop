using backend.Entities;
using backend.UpdatedEntities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssemblyController : ComponentController
    {
        private readonly ILogger<AssemblyController> logger;

        public AssemblyController(ILogger<AssemblyController> logger):base(logger)
        {
        }

        [HttpPost("createAssembly")]
        public async Task<IActionResult> CreateAssembly(Entities.Assembly assembly)
        {
            try
            {

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();

                    var computerCasePrice = await connection
                        .ExecuteScalarAsync<int>("SELECT price FROM computer_case WHERE Id = @Id", new { Id = assembly.ComputerCaseId });

                    var coolerPrice = await connection
                        .ExecuteScalarAsync<int>("SELECT price FROM cooler WHERE Id = @Id", new { Id = assembly.CoolerId });

                    var motherBoardPrice = await connection
                        .ExecuteScalarAsync<int>("SELECT price FROM mother_board WHERE Id = @Id", new { Id = assembly.MotherBoardId });

                    var processorPrice = await connection
                        .ExecuteScalarAsync<int>("SELECT price FROM processor WHERE Id = @Id", new { Id = assembly.ProcessorId });

                    var ramPrice = await connection
                        .ExecuteScalarAsync<int>("SELECT price FROM ram WHERE Id = @Id", new { Id = assembly.RamId });

                    var ssdPrice = await connection
                        .ExecuteScalarAsync<int>("SELECT price FROM ssd WHERE Id = @Id", new { Id = assembly.SsdId });

                    var videoCardPrice = await connection
                        .ExecuteScalarAsync<int>("SELECT price FROM mother_board WHERE Id = @Id", new { Id = assembly.VideoCardId });

                    var powerUnitPrice = await connection
                        .ExecuteScalarAsync<int>("SELECT price FROM power_unit WHERE Id = @Id", new { Id = assembly.PowerUnitId });

                    int assembly_price = computerCasePrice + coolerPrice + motherBoardPrice + processorPrice + ramPrice 
                        + ssdPrice + videoCardPrice + powerUnitPrice + 3000;

                    assembly.Price = assembly_price;
                    assembly.Likes = 0;

                    assembly.CreationTime = DateTime.Now;

                    var data = new
                    {
                        name = assembly.Name,
                        price = assembly.Price,
                        computerCaseId = assembly.ComputerCaseId,
                        coolerId = assembly.CoolerId,
                        motherBoardId = assembly.MotherBoardId,
                        processorId = assembly.ProcessorId,
                        ramId = assembly.RamId,
                        ssdId = assembly.SsdId,
                        videoCardId = assembly.VideoCardId,
                        powerUnitId = assembly.PowerUnitId,
                        likes = assembly.Likes,
                        creationTime = assembly.CreationTime,
                    };

                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO assembly (name, price, computercase_id, cooler_id," +
                        " motherboard_id, processor_id, ram_id, ssd_id, videocard_id, powerunit_id, likes, creation_time) " +
                                      "VALUES (@name, @price, @computerCaseId, @coolerId, @motherBoardId," +
                                      " @processorId, @ramId, @ssdId, @videocardId, @powerunitId, @likes, @creationTime) RETURN id", data);
                    logger.LogInformation("Assembly data saved to database");
                    return Ok(new { id = id, data });
                }
            }

            catch (Exception ex)
            {
                logger.LogError($"Assembly data failed to save in database. Exception: {ex}");
                return BadRequest(new {error = ex.Message });
            }
        }

        [HttpGet("getAllAssemblies")]
        public async Task<IActionResult> GetAllAssemblieses()
        {
           
            try
            {
               
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var assemblies = connection.Query<Entities.Assembly>("SELECT * FROM public.assembly");

                    logger.LogInformation("Retrieved all Assembly data from the database");

                    return Ok(new { data = assemblies });
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"Assembly data did not get gtom database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpGet("getAssembly/{id}")]
        public async Task<IActionResult> GetAssemblyById(int id)
        {
            try
            {
                
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var assembly = connection.QueryFirstOrDefault<Entities.Assembly>("SELECT * FROM public.assembly WHERE Id = @Id",
                        new { Id = id });

                    if (assembly != null)
                    {
                        logger.LogInformation($"Retrieved Assembly with Id {id} from the database");
                        return Ok(new { id = id, assembly });

                    }
                    else
                    {
                        logger.LogInformation($"Assembly with Id {id} not found in the database");
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve Assembly data from the database. \nException {ex}");
                return StatusCode(500, new { error = ex.Message});
            }
        }

        [HttpPut("updateAssembly/{id}")]
        public async Task<IActionResult> UpdateAssembly(int id, Entities.Assembly updatedAssembly)
        {
            try
            {
                
                if (updatedAssembly.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var data = new
                    {
                        id = id,
                        name = updatedAssembly.Name,
                        price = updatedAssembly.Price,
                        computerCaseId = updatedAssembly.ComputerCaseId,
                        coolerId = updatedAssembly.CoolerId,
                        motherBoardId = updatedAssembly.MotherBoardId,
                        processorId = updatedAssembly.ProcessorId,
                        ramId = updatedAssembly.RamId,
                        ssdId = updatedAssembly.SsdId,
                        videoCardId = updatedAssembly.VideoCardId,
                        powerUnitId = updatedAssembly.PowerUnitId,
                        creation_time = updatedAssembly.CreationTime,

                    };

                }

                connection.Open();
                logger.LogInformation("Connection started");

                connection.Execute("UPDATE public.assembly SET Name = @name, Price = @price, ComputerCaseId = @computerCaseId, " +
                    "CoolerId = @coolerId, MotherBoardId = @motherBoardId, ProcessorId = @processorId, RamId = @ramId," +
                    " SsdId = @ssdId, VideoCardId = @videoCardId, PowerUnitId = @powerUnitId," +
                    " Creation_time = @creation_time WHERE Id = @id", updatedAssembly);

                logger.LogInformation("Assembly data updated in the database");

                return Ok("Assembly data updated successfully");
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update Assembly data in database. \nException: {ex}");
                return StatusCode(500, new {error = ex.Message});
            }
        }

        [HttpDelete("deleteAssembly/{id}")]
        public async Task<IActionResult> DeleteAssembly(int id)
        {
            try
            {
                
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("DELETE FROM public.assembly WHERE Id = @id", new { id });

                    logger.LogInformation("Assembly data deleted from the database");

                    return Ok(new {id = id});
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete Assembly data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message});
            }
        }

        [HttpGet("getAllAssemblies/desc")]
        public async Task<IActionResult> GetAllByTimesDesc()
        {
            try
            {
                
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var assemblies = await connection.QueryAsync<Entities.Assembly>("SELECT * FROM public.assembly ORDER BY creation_time DESC");

                    logger.LogInformation("Retrieved all Assembly data from the database");

                    return Ok(new { data = assemblies });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Assembly data did not get from database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpGet("getAllAssemblies/asc")]
        public async Task<IActionResult> GetAllByTimeAsc()
        {
            try
            {
                
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var assemblies = await connection.QueryAsync<Entities.Assembly>("SELECT * FROM public.assembly ORDER BY creation_time ASC");

                    logger.LogInformation("Retrieved all Assembly data from the database");

                    return Ok(new { data = assemblies });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Assembly data did not get from database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpPut("likeAssembly/{id}")]
        public async Task<IActionResult> LikeAssembly(int id)
        {
            try
            {
                
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    
                    var currentLikes = await connection.ExecuteScalarAsync<int>("SELECT likes FROM public.assembly WHERE Id = @Id",
                        new { Id = id });

                    var updatedLikes = currentLikes + 1;

                    await connection.ExecuteAsync("UPDATE public.assembly SET likes = @Likes WHERE Id = @Id",
                        new { Likes = updatedLikes, Id = id });

                    logger.LogInformation($"Likes for Assembly with Id {id} was plused");

                    // ИНдекс сборки, которой поставили лайк
                    return Ok(new {id = id});
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to like Assembly with Id {id}. Exception: {ex}");
                return StatusCode(500, new {error = ex.Message});
            }
        }

        [HttpGet("getPopularAssemblies")]
        public async Task<IActionResult> GetPopularAssemblies()
        {
            try
            {
                
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var assemblies = await connection.QueryAsync<Entities.Assembly>("SELECT * FROM public.assembly ORDER BY likes DESC");

                    logger.LogInformation("Retrieved all Assembly data from the database");

                    return Ok(new { data = assemblies });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Assembly data did not get from database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }

    }
}
