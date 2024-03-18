
using backend.CommentEntities;
using backend.Entities;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
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

                if (computerCase.Amount < 0)
                {
                    return BadRequest(new {error = "Amount must not be less than 0"});
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
                        amount = computerCase.Amount,
                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.computer_case (brand, model, country, material, width, height, depth," +
                        "price, description, image, amount)" +
                        "VALUES (@brand, @model, @country, @material, @width, @height, @depth, @price," +
                        " @description, @image, @amount) RETURNING id", data);

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

                if (updatedComputerCase.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must not be less than 0" });
                }


                string imagePath = string.Empty;

                await using var connection = new NpgsqlConnection(connectionString);
                {

                    string filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.computer_case" +
                        " WHERE Id = @id");

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
                        amount = updatedComputerCase.Amount,
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.computer_case SET Brand = @brand, Model = @model, Country = @country," +
                        " Material = @material, Width = @width, Height = @height," +
                        " Depth = @depth, Price = @price, Description = @description," +
                        " Image = @image, Amount = @amount WHERE Id = @id", data);

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

                    filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.computer_case WHERE Id = @id",
                        new { Id = id });
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

        [HttpGet("search")]
        public async Task<IActionResult> SearchComputerCase(string keyword, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var computerCases = connection.Query<ComputerCase<string>>(@"SELECT * FROM public.computer_case " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT @Limit OFFSET @Offset", new { Keyword = "%" + keyword + "%", Limit = limit, Offset = offset });

                    return Ok(new { computerCases });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with search");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("FilterByCountry")]
        public async Task<IActionResult> FilterByCountry(string country, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var computerCases = connection.Query<ComputerCase<string>>(@"SELECT * FROM public.computer_case " +
                    "WHERE country = @Country " +
                    "LIMIT @Limit OFFSET @Offset", new { Country = country, Limit = limit, Offset = offset });

                    return Ok(new { computerCases });

                }
            }
            catch(Exception ex)
            {
                logger.LogError("Error with country filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByBrand")]
        public async Task<IActionResult> FilterByBrand(string brand, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var computerCases = connection.Query<ComputerCase<string>>(@"SELECT * FROM public.computer_case " +
                    "WHERE brand = @Brand " +
                    "LIMIT @Limit OFFSET @Offset", new { Brand = brand, Limit = limit, Offset = offset });

                    return Ok(new { computerCases });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with brand filter");
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("FilterByModel")]
        public async Task<IActionResult> FilterByModel(string model, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var computerCases = connection.Query<ComputerCase<string>>(@"SELECT * FROM public.computer_case " +
                    "WHERE model = @Model " +
                    "LIMIT @Limit OFFSET @Offset", new { Model = model, Limit = limit, Offset = offset });

                    return Ok(new { computerCases });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with country filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByPrice")]
        public async Task<IActionResult> FilterByPrice(int minPrice, int maxPrice, int limit, int offset)
        {
            try
            {
                if (minPrice < 0 || maxPrice < 0)
                {
                    return BadRequest(new { error = "price must not be 0" });
                }

                if (maxPrice < minPrice)
                {
                    return BadRequest(new { error = "maxPrice could not be less than minPrice" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var computerCases = connection.Query<ComputerCase<string>>(@"SELECT * FROM public.computer_case " +
                    "WHERE price >=  @MinPrice AND price <= @MaxPrice " +
                    "LIMIT @Limit OFFSET @Offset", new { MinPrice = minPrice, MaxPrice = maxPrice,
                        Limit = limit, Offset = offset });

                    return Ok(new { computerCases });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with price filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("addComment/{id}")]
        public async Task<IActionResult> AddComment(int id, ComputerCaseComment computerCaseComment)
        {
            try
            {

                if (string.IsNullOrEmpty(computerCaseComment.content))
                {
                    return BadRequest(new { error = "Content cannot be empty" });
                }

                computerCaseComment.comment_date = DateTime.Now;

                int insertedId;

                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    var data = new
                    {
                        content = computerCaseComment.content,
                        comment_date = computerCaseComment.comment_date,
                        computerCaseId = id,
                    };

                    await connection.OpenAsync();

                    logger.LogInformation("Connection started");

                    insertedId = await connection.QueryFirstOrDefaultAsync<int>(
                        "INSERT INTO public.computer_case_comment (content, comment_date, computercaseid) " +
                        "VALUES (@content, @comment_date, @computerCaseId) RETURNING id", data);

                    return Ok(new { id = insertedId, data });
                }


            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while adding comment");
                return BadRequest(new { error = "An error occurred while adding comment" });
            }
        }

        [HttpPut("{id}/updateComment/{comment_id}")]
        public async Task<IActionResult> UpdateComment(int id, int comment_id, ComputerCaseComment computerCaseComment)
        {
            try
            {

                if (string.IsNullOrEmpty(computerCaseComment.content))
                {
                    return BadRequest(new { error = "Content cannot be empty" });
                }

                computerCaseComment.comment_date = DateTime.Now;

                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    var data = new
                    {
                        id = comment_id,
                        content = computerCaseComment.content,
                        comment_date = computerCaseComment.comment_date,
                        computercaseid = id,
                    };

                    await connection.OpenAsync();

                    logger.LogInformation("Connection started");

                    int updatedComment = await connection.ExecuteAsync(
                        "UPDATE public.computer_case_comment " +
                        "SET content = @content, comment_date = @comment_date " +
                        "WHERE id = @id AND computercaseid = @computercaseid", data);

                    if (updatedComment == 0)
                    {
                        return NotFound(new { error = "Comment not found" });
                    }

                    return Ok(new { id = comment_id, data });
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while updating comment");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}/deleteComment/{comment_id}")]
        public async Task<IActionResult> DeleteComment(int id, int comment_id)
        {
            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    logger.LogInformation("Connection started");

                    int deletedComment = await connection.ExecuteAsync(
                        "DELETE FROM public.computer_case_comment " +
                        "WHERE id = @comment_id AND computercaseid = @id", new { id, comment_id });

                    if (deletedComment == 0)
                    {
                        return NotFound(new { error = "Comment not found" });
                    }
                }

                return Ok(new { id = id, comment_id = comment_id });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while deleting comment");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetAllComments(int limit = 1, int offset = 0)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var comments = connection.Query<ComputerCaseComment>("SELECT * FROM public.computer_case_comment " +
                        "LIMIT @Limit OFFSET @Offset",
                        new { Limit = limit, Offset = offset });

                    logger.LogInformation("Retrieved all ComputerCase data from the database");

                    return Ok(new { comments });
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with get comments");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}/getComment/{comment_id}")]
        public async Task<IActionResult> GetComment(int id, int comment_id)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var comment = await connection.QueryFirstOrDefaultAsync<ComputerCaseComment>(
                        "SELECT * FROM public.computer_case_comment WHERE id = @id AND computercaseid = @computercaseid",
                        new { id = comment_id, computercaseid = id });

                    if (comment == null)
                    {
                        return NotFound(new { error = "Comment not found" });
                    }


                    return Ok(new { id = comment_id, comment });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error with retrieving comment by ID");
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

