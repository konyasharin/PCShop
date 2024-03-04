
using backend.Entities;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Npgsql;


namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerCaseController : ComponentController
    {

        public ComputerCaseController(ILogger<ComputerCaseController> logger):base(logger)
        {
        }

        [HttpPost("createComputerCase")]
        public async Task<IActionResult> CreateCreateCase([FromForm] ComputerCase<IFormFile> computerCase)
        {

            try
            {
                string imagePath = BackupWriter.Write(computerCase.Image);


                if (computerCase.Price < 0)
                {
                    return BadRequest(new {error = "Price must not be less than 0" });
                }

                if (computerCase.Width < 10 || computerCase.Width > 100)
                {
                    return BadRequest(new { error = "Width must be between 10 and 100" });
                }

                if (computerCase.Height < 30 || computerCase.Height > 150)
                {
                    return BadRequest(new { error = "Height must be between 30 and 150" });
                }

                if (computerCase.Depth < 20 || computerCase.Depth > 100)
                {
                    return BadRequest(new { error = "Depth must be between 20 and 100" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var data = new
                    {
                        brand = computerCase.Brand,
                        model = computerCase.Model,
                        country = computerCase.Country,
                        material = computerCase.Material,
                        width = computerCase.Width,
                        height = computerCase.Height,
                        depth = computerCase.Depth,
                        price = computerCase.Price,
                        description = computerCase.Description,
                        image = imagePath,
                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.computer_case (brand, model, country, material, width, height, depth," +
                        "price, description, image)" +
                        "VALUES (@brand, @model, @country, @material, @width, @height, @depth, @price, @description, @image) RETURNING id", data);

                    logger.LogInformation($"ComputerCase data saved to database with id {id}");

                    return Ok(new { id = id, data });
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"ComputerCase data failed to save in database. Exception: {ex}");
                return BadRequest(new { error = ex .Message});
            }
        }

        [HttpGet("getAllComputerCases")]
        public async Task<IActionResult> GetAllComputerCases(int limit, int offset)
        {
            logger.LogInformation("Get method has started");
            try
            {

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var computerCases = connection.Query<ComputerCase<string>>("SELECT * FROM public.computer_case LIMIT @Limit OFFSET @Offset", 
                        new {Limit = limit, Offset = offset});

                    logger.LogInformation("Retrieved all ComputerCase data from the database");

                    return Ok(new { computerCases });
                }

               
            }
            catch(Exception ex)
            {
                logger.LogError($"ComputerCase data did not get from database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpGet("getComputerCase/{id}")]
        public async Task<IActionResult> GetComputerCaseById (int id)
        {
            try
            {
                

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    
                    var computerCase = connection.QueryFirstOrDefault<ComputerCase<string>>("SELECT * FROM public.computer_case WHERE Id = @Id",
                        new { Id = id });

                    if (computerCase != null)
                    {
                        logger.LogInformation($"Retrieved ComputerCase with Id {id} from the database");
                        return Ok(new { id = id, computerCase});

                    }
                    else
                    {
                        logger.LogInformation($"ComputerCase with Id {id} not found in the database");
                        return NotFound(new {error = $"Not found ComputerCase with {id}"});
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve ComputerCase data from the database. \nException {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("updateComputerCase/{id}")]
        public async Task<IActionResult> UpdateComputerCase(int id, [FromForm] UpdatedComputerCase updatedComputerCase)
        {
            try
            {

                if (updatedComputerCase.Width < 10 || updatedComputerCase.Width > 100)
                {
                    return BadRequest(new { error = "Width must be between 10 and 100" });
                }

                if (updatedComputerCase.Height < 30 || updatedComputerCase.Height > 150)
                {
                    return BadRequest(new { error = "Height must be between 30 and 150" });
                }

                if (updatedComputerCase.Depth < 20 || updatedComputerCase.Depth > 100)
                {
                    return BadRequest(new { error = "Depth must be between 20 and 100" });
                }

                if (updatedComputerCase.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }


                string imagePath = string.Empty;

                await using var connection = new NpgsqlConnection(connectionString);
                {

                    string filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.computer_case WHERE Id = @id");

                    if (updatedComputerCase.updated)
                    {
                        
                        BackupWriter.Delete(filePath);
                        imagePath = BackupWriter.Write(updatedComputerCase.Image);
                    }
                    else 
                    {
                        imagePath = filePath;
                    }

                    var data = new
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
                        image = imagePath,
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.computer_case SET Brand = @brand, Model = @model, Country = @country, Material = @material, Width = @width, Height = @height," +
                        " Depth = @depth, Price = @price, Description = @description, Image = @image WHERE Id = @id", data);

                    logger.LogInformation("ComputerCase data updated in the database");

                    return Ok(new { id = id, data });

                }

                
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update ComputerCase data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("deleteComputerCase/{id}")]
        public async Task<IActionResult> DeleteComputerCase(int id)
        {
            try
            {
                string filePath;

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.computer_case WHERE Id = @id", new { Id = id });
                    BackupWriter.Delete(filePath);

                    connection.Execute("DELETE FROM public.computer_case WHERE Id = @id", new { id });

                    logger.LogInformation("ComputerCase data deleted from the database");

                    return Ok(new {id = id});
                }

            }
            catch(Exception ex)
            {
                logger.LogError($"Failed to delete ComputerCase data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}

