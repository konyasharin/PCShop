using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.User;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Npgsql;
using System.Reflection;


namespace backend.Controllers
{
    [Route("api/assembly")]
    [ApiController]
    public class AssemblyController : ProductController
    {

        public AssemblyController(ILogger<AssemblyController> logger) : base(logger)
        {
        }
        
        
        /// <summary>
        /// Метод для создания сборки в базе данных.
        /// </summary>
        /// <param name="assembly">Сборка, которую необходимо создать.</param>
        /// <returns>Результат операции создания сборки в базе данных.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateAssembly([FromForm] Assembly<IFormFile> assembly)
        {
            string[] characteristics =
            [
                "name", "price", "computer_case_id", "cooler_id", "mother_board_id",
                "processor_id", "ram_id", "ssd_id", "video_card_id", "power_unit_id", "likes", "creation_time", "power",
                "image"
            ];
            try
            {
                assembly.Likes = 0;
                assembly.CreationTime = DateTime.Now;
                string imagePath = BackupWriter.Write(assembly.Image);
                Dictionary<string, object> request = new Dictionary<string, object>();
                PropertyInfo[] properies = typeof(Assembly<IFormFile>).GetProperties();
                foreach (var property in properies)
                {
                    if (property.Name != "Image")
                    {
                        request[property.Name] = property.GetValue(assembly);   
                    }
                    else
                    {
                        request[property.Name] = imagePath;
                    }
                }
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();

                    request["Id"] = connection.QueryFirstOrDefault<int>($"INSERT INTO public.assemblies " +
                                                                      $"({DatabaseRequestsHelper.TransformCharacteristicsToString(characteristics)}) " +
                                                                      $"VALUES({DatabaseRequestsHelper.TransformCharacteristicsToString(DatabaseRequestsHelper.SnakeCasesToPascalCase(characteristics), "@")}) " +
                                                                      $"RETURNING id", request);
                    logger.LogInformation("Assembly data saved to database");
                    return Ok(new { assembly =  request });
                }
            }

            catch (Exception ex)
            {
                logger.LogError($"Assembly data failed to save in database. Exception: {ex}");
                return BadRequest(new {error = ex.Message });
            }
        }
        
        /// <summary>
        /// Метод для получения популярных сборок из базы данных.
        /// </summary>
        /// <returns>Результат операции получения популярных сборок из базы данных.</returns>
        [HttpGet("getPopular")]
        public async Task<IActionResult> GetPopularAssemblies()
        {
            string[] characteristics =
            [
                "id", "name", "price", "computer_case_id AS computerCaseId", "cooler_id AS coolerId", "mother_board_id AS motherBoardId",
                "processor_id AS processorId", "ram_id AS ramId", "ssd_id AS ssdId", "video_card_id AS videoCardId", "power_unit_id AS powerUnitId", 
                "likes", "creation_time AS creationTime", "power", "image"
            ];
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    var assemblies = await connection.QueryAsync<Assembly<string>>($"SELECT {DatabaseRequestsHelper.TransformCharacteristicsToString(characteristics)} " +
                        $"FROM public.assemblies ORDER BY likes DESC LIMIT 3");
        
                    logger.LogInformation("Retrieved all Assembly data from the database");
                    return Ok(new { assemblies });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Assembly data did not get from database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }
        
        /// <summary>
        /// Метод для получения последних сборок из базы данных с учетом смещения.
        /// </summary>
        /// <param name="offset">Смещение для запроса последних сборок.</param>
        /// <returns>Результат операции получения последних сборок из базы данных.</returns>
        [HttpGet("getLast")]
        public async Task<IActionResult> GetLastBuilds(int offset)
        {
            string[] characteristics =
            [
                "id", "name", "price", "computer_case_id AS computerCaseId", "cooler_id AS coolerId", "mother_board_id AS motherBoardId",
                "processor_id AS processorId", "ram_id AS ramId", "ssd_id AS ssdId", "video_card_id AS videoCardId", "power_unit_id AS powerUnitId", 
                "likes", "creation_time AS creationTime", "power", "image"
            ];

            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    var assemblies =
                        await connection.QueryAsync<Assembly<string>>(
                            $"SELECT {DatabaseRequestsHelper.TransformCharacteristicsToString(characteristics)} " +
                            $"FROM public.assemblies ORDER BY creation_time DESC LIMIT 3 OFFSET {offset}");
                    logger.LogInformation("Retrieved all Assembly data from the database");
                    return Ok(new { assemblies });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Assembly data did not get from database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }
        
        /// <summary>
        /// Метод для получения всех сборок из базы данных с учетом смещения и лимита.
        /// </summary>
        /// <param name="offset">Смещение для запроса сборок.</param>
        /// <param name="limit">Максимальное количество сборок, которые требуется вернуть.</param>
        /// <returns>Результат операции получения всех сборок из базы данных.</returns>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetBuilds(int offset, int limit)
        {
            string[] characteristics =
            [
                "id", "name", "price", "computer_case_id AS computerCaseId", "cooler_id AS coolerId", "mother_board_id AS motherBoardId",
                "processor_id AS processorId", "ram_id AS ramId", "ssd_id AS ssdId", "video_card_id AS videoCardId", "power_unit_id AS powerUnitId", 
                "likes", "creation_time AS creationTime", "power", "image"
            ];

            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    var assemblies =
                        await connection.QueryAsync<Assembly<string>>(
                            $"SELECT {DatabaseRequestsHelper.TransformCharacteristicsToString(characteristics)} " +
                            $"FROM public.assemblies LIMIT {limit} OFFSET {offset}");
                    logger.LogInformation("Retrieved all Assembly data from the database");
                    return Ok(new { assemblies });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Assembly data did not get from database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }

        // [HttpGet("getAllAssemblies")]
        // public async Task<IActionResult> GetAllAssemblieses(int limit, int offset)
        // {
        //    
        //     try
        //     {
        //        
        //         await using var connection = new NpgsqlConnection(connectionString);
        //         {
        //             connection.Open();
        //             logger.LogInformation("Connection started");
        //
        //             var assemblies = connection.Query<Entities.Assembly>("SELECT * FROM public.assembly LIMIT @Limit OFFSET @Offset",
        //                 new {Limit = limit, Offset = offset});
        //
        //             logger.LogInformation("Retrieved all Assembly data from the database");
        //
        //             return Ok(new { data = assemblies });
        //         }
        //
        //
        //     }
        //     catch (Exception ex)
        //     {
        //         logger.LogError($"Assembly data did not get gtom database. Exception: {ex}");
        //         return NotFound(new {error = ex.Message});
        //     }
        // }
        //
        // [HttpGet("getAssembly/{id}")]
        // public async Task<IActionResult> GetAssemblyById(int id)
        // {
        //     try
        //     {
        //         
        //         await using var connection = new NpgsqlConnection(connectionString);
        //         {
        //             connection.Open();
        //             logger.LogInformation("Connection started");
        //
        //
        //             var assembly = connection.QueryFirstOrDefault<Entities.Assembly>("SELECT * FROM public.assembly WHERE Id = @Id",
        //                 new { Id = id });
        //
        //             if (assembly != null)
        //             {
        //                 logger.LogInformation($"Retrieved Assembly with Id {id} from the database");
        //                 return Ok(new { id = id, assembly });
        //
        //             }
        //             else
        //             {
        //                 logger.LogInformation($"Assembly with Id {id} not found in the database");
        //                 return NotFound();
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         logger.LogError($"Failed to retrieve Assembly data from the database. \nException {ex}");
        //         return StatusCode(500, new { error = ex.Message});
        //     }
        // }
        //
        // [HttpPut("updateAssembly/{id}")]
        // public async Task<IActionResult> UpdateAssembly(int id, Entities.Assembly updatedAssembly)
        // {
        //     try
        //     {
        //         
        //         if (updatedAssembly.Price < 0)
        //         {
        //             return BadRequest(new { error = "Price must not be less than 0" });
        //         }
        //         
        //
        //         if(updatedAssembly.Likes < 0)
        //         {
        //             return BadRequest(new { error = "Likes must not be less than 0" });
        //         }
        //
        //         await using var connection = new NpgsqlConnection(connectionString);
        //         {
        //             var data = new
        //             {
        //                 id = id,
        //                 name = updatedAssembly.Name,
        //                 price = updatedAssembly.Price,
        //                 computerCaseId = updatedAssembly.ComputerCaseId,
        //                 coolerId = updatedAssembly.CoolerId,
        //                 motherBoardId = updatedAssembly.MotherBoardId,
        //                 processorId = updatedAssembly.ProcessorId,
        //                 ramId = updatedAssembly.RamId,
        //                 ssdId = updatedAssembly.SsdId,
        //                 videoCardId = updatedAssembly.VideoCardId,
        //                 powerUnitId = updatedAssembly.PowerUnitId,
        //                 creation_time = updatedAssembly.CreationTime,
        //                 power = updatedAssembly.Power,
        //
        //             };
        //
        //         }
        //
        //         connection.Open();
        //         logger.LogInformation("Connection started");
        //
        //         connection.Execute("UPDATE public.assembly SET Name = @name, Price = @price, ComputerCaseId = @computerCaseId, " +
        //             "CoolerId = @coolerId, MotherBoardId = @motherBoardId, ProcessorId = @processorId, RamId = @ramId," +
        //             " SsdId = @ssdId, VideoCardId = @videoCardId, PowerUnitId = @powerUnitId," +
        //             " Creation_time = @creation_time, Amount = @amount, Power = @power WHERE Id = @id", updatedAssembly);
        //
        //         logger.LogInformation("Assembly data updated in the database");
        //
        //         return Ok("Assembly data updated successfully");
        //     }
        //     catch (Exception ex)
        //     {
        //         logger.LogError($"Failed to update Assembly data in database. \nException: {ex}");
        //         return StatusCode(500, new {error = ex.Message});
        //     }
        // }
        //
        // [HttpDelete("deleteAssembly/{id}")]
        // public async Task<IActionResult> DeleteAssembly(int id)
        // {
        //     try
        //     {
        //         
        //         await using var connection = new NpgsqlConnection(connectionString);
        //         {
        //             connection.Open();
        //             logger.LogInformation("Connection started");
        //
        //             connection.Execute("DELETE FROM public.assembly WHERE Id = @id", new { id });
        //
        //             connection.Execute("DELETE FROM public.like WHERE componentid = @id AND component = assembly",
        //                 new { id });
        //
        //             connection.Execute("DELETE FROM public.comment WHERE component_id = @id AND component = assembly",
        //                 new { id });
        //
        //             logger.LogInformation("Assembly data deleted from the database");
        //
        //             return Ok(new {id = id});
        //         }
        //
        //     }
        //     catch (Exception ex)
        //     {
        //         logger.LogError($"Failed to delete Assembly data in database. \nException: {ex}");
        //         return StatusCode(500, new { error = ex.Message});
        //     }
        // }
        //
        // [HttpGet("getAllAssemblies/desc")]
        // public async Task<IActionResult> GetAllByTimesDesc(int limit, int offset)
        // {
        //     try
        //     {
        //         
        //         await using var connection = new NpgsqlConnection(connectionString);
        //         {
        //             connection.Open();
        //             logger.LogInformation("Connection started");
        //
        //             var assemblies = await connection.QueryAsync<Entities.Assembly>("SELECT * FROM public.assembly ORDER BY creation_time DESC" +
        //                 " LIMIT @Limit OFFSET @Offset", new {Limit = limit, Offset = offset});
        //
        //             logger.LogInformation("Retrieved all Assembly data from the database");
        //
        //             return Ok(new { data = assemblies });
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         logger.LogError($"Assembly data did not get from database. Exception: {ex}");
        //         return NotFound(new {error = ex.Message});
        //     }
        // }
        //
        // [HttpGet("getAllAssemblies/asc")]
        // public async Task<IActionResult> GetAllByTimeAsc(int limit, int offset)
        // {
        //     try
        //     {
        //         
        //         await using var connection = new NpgsqlConnection(connectionString);
        //         {
        //             connection.Open();
        //             logger.LogInformation("Connection started");
        //
        //             var assemblies = await connection.QueryAsync<Entities.Assembly>("SELECT * FROM public.assembly ORDER BY creation_time ASC" +
        //                 " LIMIT @Limit OFFSET @Offset", new {Limit = limit, Offset = offset});
        //
        //             logger.LogInformation("Retrieved all Assembly data from the database");
        //
        //             return Ok(new { data = assemblies });
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         logger.LogError($"Assembly data did not get from database. Exception: {ex}");
        //         return NotFound(new {error = ex.Message});
        //     }
        // }
        //
        // [HttpPut("likeAssembly/{id}")]
        // public async Task<IActionResult> LikeAssembly(int id, User user)
        // {
        //     return await LikeComponent(id, user, "assembly");
        // }
        //
        // [HttpPost("addComment")]
        // public async Task<IActionResult> AddAssemblyComment(Comment assemblyComment)
        // {
        //     return await AddComment(assemblyComment);
        // }
        //
        // [HttpPut("updateComment")]
        // public async Task<IActionResult> UpdateAssemblyComment(Comment assemblyComment)
        // {
        //     return await UpdateComment(assemblyComment);
        // }
        //
        // [HttpDelete("{productId}/deleteComment/{commentId}")]
        // public async Task<IActionResult> DeleteAssemblyComment(int productId, int commentId)
        // {
        //     return await DeleteComment(productId, commentId, "assembly");
        // }
        //
        // [HttpGet("GetAllComments")]
        // public async Task<IActionResult> GetAssemblyAllComments(int productId, int limit = 1, int offset = 0)
        // {
        //     return await GetAllComments(limit, offset, "assembly", productId);
        // }
        //
        // [HttpGet("{productId}/getComment/{commentId}")]
        // public async Task<IActionResult> GetAssemblyComment(int productId, int commentId)
        // {
        //     return await GetComment(productId, commentId, "assembly");
        // }
        //
        //
        // [HttpGet("search")]
        // public async Task<IActionResult> SearchAssembly(string keyword, int limit = 1, int offset = 0)
        // {
        //     try
        //     {
        //         await using var connection = new NpgsqlConnection(connectionString);
        //         {
        //             connection.Open();
        //             logger.LogInformation("Connection started");
        //
        //             var assemblies = connection.Query<Entities.Assembly>(@"SELECT * FROM public.assembly " +
        //             "WHERE name LIKE @Keyword " +
        //                 "LIMIT @Limit OFFSET @Offset", new { Keyword = "%" + keyword + "%", Limit = limit, Offset = offset });
        //
        //             return Ok(new { assemblies });
        //
        //         }
        //     }
        //     catch(Exception ex)
        //     {
        //         logger.LogError("Error with search");
        //         return StatusCode(500, new { error = ex.Message});
        //     }
        // }
        //
        // [HttpGet("Filter")]
        // public async Task<IActionResult> FilterAssembly(string name, int minPrice, 
        //     int maxPrice, int limit = 1, int offset = 0)
        // {
        //     try
        //     {
        //         if (minPrice < 0 || maxPrice < 0)
        //         {
        //             return BadRequest(new { error = "price must not be 0" });
        //         }
        //
        //         if (maxPrice < minPrice)
        //         {
        //             return BadRequest(new { error = "maxPrice could not be less than minPrice" });
        //         }
        //
        //         await using var connection = new NpgsqlConnection(connectionString);
        //         {
        //             connection.Open();
        //             logger.LogInformation("Connection started");
        //
        //             var assemblies = connection.Query<Entities.Assembly>(@"SELECT * FROM public.assembly " +
        //             "WHERE name = @Name AND price >=  @MinPrice AND price <= @MaxPrice " +
        //             "LIMIT @Limit OFFSET @Offset", new { Name = name, MinPrice = minPrice, 
        //                 MaxPrice = maxPrice, Limit = limit, Offset = offset });
        //
        //             return Ok(new { assemblies });
        //
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         logger.LogError("Error with name filter");
        //         return BadRequest(new { error = ex.Message });
        //     }
        // }


    }
}
