using backend.Data;
using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerCaseController : ControllerBase
    {
        private readonly ILogger<ComputerCaseController> logger;
      
        public ComputerCaseController(ILogger<ComputerCaseController> logger)
        {
            this.logger = logger;
    
        }

        [HttpPost("createComputerCase")]
        public async Task<IActionResult> CreateCreateCase(ComputerCase computerCase)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (computerCase.Price < 0)
                {
                    string error = "Price must not be less than 0";
                    return BadRequest(error);
                }

                if (computerCase.Width < 10 || computerCase.Width > 100)
                {
                    string error = "Width must be between 10 and 100.";
                    return BadRequest(error);
                }

                if (computerCase.Height < 30 || computerCase.Height > 150)
                {
                    string error = "Height must be between 30 and 150";
                    return BadRequest(error);
                }

                if (computerCase.Depth < 20 || computerCase.Depth > 100)
                {
                    return BadRequest("Depth must be between 20 and 100");
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = computerCase.Id,
                        brand = computerCase.Brand,
                        model = computerCase.Model,
                        country = computerCase.Country,
                        material = computerCase.Material,
                        width = computerCase.Width,
                        height = computerCase.Height,
                        depth = computerCase.Depth,
                        price = computerCase.Price,
                        description = computerCase.Description,
                        image = computerCase.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.computer_case (Id, Brand, Model, Country, Material, Width, Height, Depth," +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Material, @Width, @Height, @Depth, @Price, @Description, @Image)", computerCase);

                    logger.LogInformation("ComputerCase data saved to database");

                    String result = "ComputerCase data saved to database";

                    return Ok(result);
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"ComputerCase data failed to save in database. Exception: {ex}");
                return BadRequest(ex);
            }
        }

        [HttpGet("getAllComputerCases")]
        public async Task<IActionResult> GetAllComputerCases()
        {
            logger.LogInformation("Get method has started");
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var computerCases = connection.Query<ComputerCase>("SELECT * FROM public.computer_case");

                    logger.LogInformation("Retrieved all ComputerCase data from the database");

                    return Ok(computerCases);
                }

               
            }
            catch(Exception ex)
            {
                logger.LogError($"ComputerCase data did not get gtom database. Exception: {ex}");
                return NotFound();
            }
        }

        [HttpGet("getComputerCase/{id}")]
        public async Task<IActionResult> GetComputerCaseById (int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    
                    var computerCase = connection.QueryFirstOrDefault<ComputerCase>("SELECT * FROM public.computer_case WHERE Id = @Id",
                        new { Id = id });

                    if (computerCase != null)
                    {
                        logger.LogInformation($"Retrieved ComputerCase with Id {id} from the database");
                        return Ok(computerCase);

                    }
                    else
                    {
                        logger.LogInformation($"ComputerCase with Id {id} not found in the database");
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve ComputerCase data from the database. \nException {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("updateComputerCase/{id}")]
        public async Task<IActionResult> UpdateComputerCase(int id, ComputerCase updatedComputerCase)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (updatedComputerCase.Width < 10 || updatedComputerCase.Width > 100)
                {
                    return BadRequest("Width must be between 10 and 100.");
                }

                if (updatedComputerCase.Height < 30 || updatedComputerCase.Height > 150)
                {
                    return BadRequest("Height must be between 30 and 150");
                }

                if (updatedComputerCase.Depth < 20 || updatedComputerCase.Depth > 100)
                {
                    return BadRequest("Depth must be between 20 and 100");
                }

                if (updatedComputerCase.Price < 0)
                {
                    return BadRequest("Price must not be less than 0");
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = id,
                        brand = updatedComputerCase.Brand,
                        model = updatedComputerCase.Model,
                        country = updatedComputerCase.Country,
                        material = updatedComputerCase.Material,
                        width = updatedComputerCase.Width,
                        height = updatedComputerCase.Height,
                        depth = updatedComputerCase.Depth,
                        price = updatedComputerCase.Price,
                        description = updatedComputerCase.Description,
                        image = updatedComputerCase.Image
                    };

                }

                connection.Open();
                logger.LogInformation("Connection started");

                connection.Execute("UPDATE public.computer_case SET Brand = @brand, Model = @model, Country = @country, Material = @material, Width = @width, Height = @height," +
                    " Depth = @depth, Price = @price, Description = @description, Image = @image WHERE Id = @id", updatedComputerCase);

                logger.LogInformation("ComputerCase data updated in the database");

                return Ok("ComputerCase data updated successfully");
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update ComputerCase data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("deleteComputerCase/{id}")]
        public async Task<IActionResult> DeleteComputerCase(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("DELETE FROM public.computer_case WHERE Id = @id", new { id });

                    logger.LogInformation("ComputerCase data deleted from the database");

                    return Ok("ComputerCase data deleted successfully");
                }

            }
            catch(Exception ex)
            {
                logger.LogError($"Failed to delete ComputerCase data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

