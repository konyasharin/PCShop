﻿using backend.Entities;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Runtime.Intrinsics.Arm;
using backend.Entities.CommentEntities;
using backend.Entities.User;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamController : ComponentController
    {
        
        public RamController(ILogger<RamController> logger):base(logger)
        {
           
        }

        [HttpPost("createRam")]
        public async Task<IActionResult> CreateRam([FromForm] RAM<IFormFile> ram)
        {

            try
            {
                string imagePath = BackupWriter.Write(ram.Image);
               
                if (ram.Frequency < 0 || ram.Frequency > 100000)
                {
                    return BadRequest(new { error = "Frequency must be between 0 and 100000" });
                }

                if (ram.Timings < 0 || ram.Timings > 10000)
                {
                    return BadRequest(new { error = "Timings must be between 0 and 10000" });
                }

                if (ram.Capacity_db < 0 || ram.Capacity_db > 10000)
                {
                    return BadRequest(new { error = "Capacity_db must be between 0 and 10000" });
                }

                if (ram.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if (ram.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must be less than 0" });
                }

                if(ram.Power < 0 || ram.Power > 10)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10" });
                }

                ram.Likes = 0;

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var data = new
                    {
                        brand = ram.Brand,
                        model = ram.Model,
                        country = ram.Country,
                        frequency = ram.Frequency,
                        timings = ram.Timings,
                        capacity_db = ram.Capacity_db,
                        price = ram.Price,
                        description = ram.Description,
                        image = imagePath,
                        amount = ram.Amount,
                        power = ram.Power,
                        likes = ram.Likes,

                    };

                    connection.Open();
                    int id = connection.QueryFirstOrDefault<int>("INSERT INTO public.ram (id, brand, model, country, frequency, timings, capacity_db," +
                        "price, description, image, amount, power, likes)" +
                        "VALUES (@brand, @model, @country, @frequency, @timings, @capacity_db," +
                        " @price, @description, @image, @amount, @power, @likes) RETURNING id", data);

                    logger.LogInformation("Ram data saved to database");
                    return Ok(new { id = id, data });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Ram data did not save in database. Exception: {ex}");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getRam/{id}")]
        public async Task<IActionResult> GetRamById(int id)
        {
            try
            {
               
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var ram = connection.QueryFirstOrDefault<RAM<string>>("SELECT * FROM public.ram WHERE Id = @Id",
                        new { Id = id });

                    if (ram != null)
                    {
                        logger.LogInformation($"Retrieved Ram with Id {id} from the database");
                        return Ok(new { id = id, ram });

                    }
                    else
                    {
                        logger.LogInformation($"Ram with Id {id} not found in the database");
                        return NotFound(new {error = $"Ram NotFound with {id}"});
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve Ram data from the database. \nException {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("updateRam/{id}")]
        public async Task<IActionResult> UpdateRam(int id, [FromForm] UpdatedRam updatedRam)
        {
            try
            {
               
                if (updatedRam.Frequency < 0 || updatedRam.Frequency > 100000)
                {
                    return BadRequest(new { error = "Frequency must be between 0 and 100000" });
                }

                if (updatedRam.Timings <0 || updatedRam.Timings > 10000)
                {
                    return BadRequest(new { error = "Timings must be between 0 and 10000" });
                }

                if (updatedRam.Capacity_db < 0 || updatedRam.Capacity_db > 10000)
                {
                    return BadRequest(new { error = "Capacity_db must be between 0 and 10000" });
                }

                if (updatedRam.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if (updatedRam.Amount < 0)
                {
                    return BadRequest(new { error = "Amount must be less than 0" });
                }

                if (updatedRam.Power < 0 || updatedRam.Power > 10)
                {
                    return BadRequest(new { error = "Power must be between 0 and 10" });
                }

                if(updatedRam.Likes < 0)
                {
                    return BadRequest(new { error = "Likes must not be less than 0" });
                }

                string imagePath = string.Empty;
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    string filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.ram WHERE Id = @id");

                    if (updatedRam.updated)
                    {

                        BackupWriter.Delete(filePath);
                        imagePath = BackupWriter.Write(updatedRam.Image);
                    }
                    else
                    {
                        imagePath = filePath;
                    }

                    var data = new
                    {
                        id = id,
                        brand = updatedRam.Brand,
                        model = updatedRam.Model,
                        country = updatedRam.Country,
                        frequency = updatedRam.Frequency,
                        timings = updatedRam.Timings,
                        capacity_db = updatedRam.Capacity_db,
                        price = updatedRam.Price,
                        description = updatedRam.Description,
                        image = imagePath,
                        amount = updatedRam.Amount,
                        power = updatedRam.Power,
                        likes = updatedRam.Likes,
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.ram SET Brand = @brand, Model = @model," +
                        " Country = @country, Frequency = @frequency," +
                        " Timings = @timings, Capacity_db = @capacity_db," +
                        " Price = @price, Description = @description," +
                        " Image = @image, Amount = @amount, Power = @power, Likes = @likes WHERE Id = @id", data);

                    logger.LogInformation("RAM data updated in the database");

                    return Ok(new { id = id, data });

                }

                
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update RAM data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("deleteRam/{id}")]
        public async Task<IActionResult> DeleteRam(int id)
        {
            try
            {
                string filePath;
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.ram WHERE Id = @id", new { Id = id });
                    BackupWriter.Delete(filePath);

                    connection.Execute("DELETE FROM public.ram WHERE Id = @id", new { id });

                    logger.LogInformation("RAM data deleted from the database");

                    return Ok(new {id=id});
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete RAM data in database. \nException: {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("getAllRam")]
        public async Task<IActionResult> GetAllRams(int limit, int offset)
        {
            logger.LogInformation("Get method has started");
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var computerCases = connection.Query<RAM<string>>("SELECT * FROM public.ram LIMIT @Limit OFFSET @Offset",
                        new {Limit = limit, Offset = offset});

                    logger.LogInformation("Retrieved all RAM data from the database");

                    return Ok(new { data = computerCases });
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"RAM data did not get from database. Exception: {ex}");
                return NotFound(new {error=ex.Message});
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRam(string keyword, int limit = 1, int offset = 0)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ram " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT @Limit OFFSET @Offset", new { Keyword = "%" + keyword + "%", Limit = limit, Offset = offset });

                    return Ok(new { ram });

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

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ram " +
                    "WHERE country = @Country " +
                    "LIMIT @Limit OFFSET @Offset", new { Country = country, Limit = limit, Offset = offset });

                    return Ok(new { ram });

                }
            }
            catch (Exception ex)
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

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ram " +
                    "WHERE brand = @Brand " +
                    "LIMIT @Limit OFFSET @Offset", new { Brand = brand, Limit = limit, Offset = offset });

                    return Ok(new { ram });

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

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ram " +
                    "WHERE model = @Model " +
                    "LIMIT @Limit OFFSET @Offset", new { Model = model, Limit = limit, Offset = offset });

                    return Ok(new { ram });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with model filter");
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

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ram " +
                    "WHERE price >=  @MinPrice AND price <= @MaxPrice " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinPrice = minPrice,
                        MaxPrice = maxPrice,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { ram });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with price filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByFrequency")]
        public async Task<IActionResult> FilterByFrequency(int minFrequency, int maxFrequency, int limit, int offset)
        {
            try
            {
                if (minFrequency < 0 || maxFrequency < 0)
                {
                    return BadRequest(new { error = "frequency must not be 0" });
                }

                if (maxFrequency < minFrequency)
                {
                    return BadRequest(new { error = "maxFrequecny could not be less than minFrequency" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ram " +
                    "WHERE frequency >=  @MinFrequency AND frequency <= @MaxFrequency " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinFrequency = minFrequency,
                        MaxFrequency = maxFrequency,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { ram });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with frequency filter");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("FilterByCapacity")]
        public async Task<IActionResult> FilterByCapacity(int minCapacity, int maxCapacity, int limit, int offset)
        {
            try
            {
                if (minCapacity < 0 || maxCapacity < 0)
                {
                    return BadRequest(new { error = "capacity_db must not be 0" });
                }

                if (maxCapacity < minCapacity)
                {
                    return BadRequest(new { error = "maxCapacity could not be less than minCapacity" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var ram = connection.Query<RAM<string>>(@"SELECT * FROM public.ram " +
                    "WHERE capacity_db >=  @MinCapacity AND capacity_db <= @MaxCapacity " +
                    "LIMIT @Limit OFFSET @Offset", new
                    {
                        MinCapacity = minCapacity,
                        MaxCapacity = maxCapacity,
                        Limit = limit,
                        Offset = offset
                    });

                    return Ok(new { ram });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with capacity_db filter");
                return BadRequest(new { error = ex.Message });
            }
        }
        
        [HttpPost("addComment")]
        public async Task<IActionResult> AddComputerCaseComment(Comment computerCaseComment)
        {
            return await AddComment(computerCaseComment, "ram_comment");
        }
        
        [HttpPut("updateComment")]
        public async Task<IActionResult> UpdateComputerCaseComment(Comment computerCaseComment)
        {
            return await UpdateComment(computerCaseComment, "ram_comment");
        }
        
        [HttpDelete("{productId}/deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComputerCaseComment(int productId, int commentId)
        {
            return await DeleteComment(productId, commentId, "ram_comment");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "ram_comment");
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "ram_comment");
        }

        [HttpPut("likeRam/{id}")]
        public async Task<IActionResult> LikeRam(int id, User user)
        {
            return await LikeComponent(id, user, "ram");
        }
    }
}
