using backend.Entities;
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
    public class AssemblyController : ControllerBase
    {
        private readonly ILogger<AssemblyController> logger;


        public AssemblyController(ILogger<AssemblyController> logger)
        {
            this.logger = logger;

        }

        [HttpPost("createAssembly")]
        public async Task<IActionResult> CreateAssembly(Entities.Assembly assembly)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");


                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();

                    logger.LogInformation("Database connected");

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

                    assembly.Creation_time = DateTime.Now;

                    var parameters = new
                    {
                        id = assembly.Id,
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
                        creation_time = assembly.Creation_time,
                    };

                    
                    logger.LogInformation("Database connected");

                    await connection.ExecuteAsync("INSERT INTO assembly (id, name, price, computercaseid, coolerid," +
                        " motherboardid, processorid, ramid, ssdid, videocardid, powerunitid, likes, creation_time) " +
                                      "VALUES (@id, @name, @price, @computercaseid, @coolerid, @motherboardid," +
                                      " @processorid, @ramid, @ssdid, @videocardid, @powerunitid, @likes, @creation_time)", assembly);

                    logger.LogInformation("Assembly data saved to database");

                    return Ok("Assembly data saved successfully");
                }
            }

            catch (Exception ex)
            {
                logger.LogError($"Assembly data failed to save in database. Exception: {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllAssemblies")]
        public async Task<IActionResult> GetAllAssemblieses()
        {
           
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var assemblies = connection.Query<Entities.Assembly>("SELECT * FROM public.assembly");

                    logger.LogInformation("Retrieved all Assembly data from the database");

                    return Ok(assemblies);
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"Assembly data did not get gtom database. Exception: {ex}");
                return NotFound();
            }
        }

        [HttpGet("getAssembly/{id}")]
        public async Task<IActionResult> GetAssemblyById(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var assembly = connection.QueryFirstOrDefault<Entities.Assembly>("SELECT * FROM public.assembly WHERE Id = @Id",
                        new { Id = id });

                    if (assembly != null)
                    {
                        logger.LogInformation($"Retrieved Assembly with Id {id} from the database");
                        return Ok(assembly);

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
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("updateAssembly/{id}")]
        public async Task<IActionResult> UpdateAssembly(int id, Entities.Assembly updatedAssembly)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (updatedAssembly.Price < 0)
                {
                    return BadRequest("Price must not be less than 0");
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
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
                        creation_time = updatedAssembly.Creation_time,

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
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("deleteAssembly/{id}")]
        public async Task<IActionResult> DeleteAssembly(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("DELETE FROM public.assembly WHERE Id = @id", new { id });

                    logger.LogInformation("Assembly data deleted from the database");

                    return Ok("Assembly data deleted successfully");
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete Assembly data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getAllAssemblies/desc")]
        public async Task<IActionResult> GetAllByTimesDesc()
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var assemblies = await connection.QueryAsync<Entities.Assembly>("SELECT * FROM public.assembly ORDER BY creation_time DESC");

                    logger.LogInformation("Retrieved all Assembly data from the database");

                    return Ok(assemblies);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Assembly data did not get from database. Exception: {ex}");
                return NotFound();
            }
        }

        [HttpGet("getAllAssemblies/asc")]
        public async Task<IActionResult> GetAllByTimeAsc()
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var assemblies = await connection.QueryAsync<Entities.Assembly>("SELECT * FROM public.assembly ORDER BY creation_time ASC");

                    logger.LogInformation("Retrieved all Assembly data from the database");

                    return Ok(assemblies);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Assembly data did not get from database. Exception: {ex}");
                return NotFound();
            }
        }

        [HttpPut("likeAssembly/{id}")]
        public async Task<IActionResult> LikeAssembly(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

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

                    return Ok($"Likes for Assembly with Id {id} was plused");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to like Assembly with Id {id}. Exception: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getPopularAssemblies")]
        public async Task<IActionResult> GetPopularAssemblies()
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var assemblies = await connection.QueryAsync<Entities.Assembly>("SELECT * FROM public.assembly ORDER BY likes DESC");

                    logger.LogInformation("Retrieved all Assembly data from the database");

                    return Ok(assemblies);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Assembly data did not get from database. Exception: {ex}");
                return NotFound();
            }
        }

    }
}
