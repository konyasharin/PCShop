using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.ComponentsInfo;
using backend.Entities.User;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Npgsql;
using System.Drawing;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotherBoardController : ComponentController
    {

        public MotherBoardController(ILogger<MotherBoardController> logger):base(logger)
        {
          
        }

        [HttpPost("createMotherBoard")]
        public async Task<IActionResult> CreateMotherBoard([FromForm] MotherBoard<IFormFile> motherBoard)
        {
            if (motherBoard.Frequency < 0 || motherBoard.Frequency > 100000)
            {
                return BadRequest(new { error = "Frequency must be between 0 and 100000" });
            }

            motherBoard.Likes = 0;
            motherBoard.ProductType = "mother_board";

            return await CreateComponent<MotherBoard<IFormFile>>(motherBoard, ["frequency", "socket", "chipset"], "mother_board");
        }


    

        [HttpGet("getMotherBoard/{id}")]
        public async Task<IActionResult> GetComputerCaseById(int id)
        {
            return await GetComponent<MotherBoardInfo>(id, "mother_board", ["frequency", "socket", "chipset"]);
        }

        [HttpPut("updateMotherBoard/{id}")]
        public async Task<IActionResult> UpdateMotherBoard(int id, [FromForm] MotherBoard<IFormFile> motherBoard, [FromQuery] bool isUpdated)
        {
            motherBoard.ProductId = id;
            motherBoard.ProductType = "mother_board";
            return await UpdateComponent<MotherBoard<IFormFile>>(motherBoard, isUpdated, "mother_board", ["frequency", "socket", "chipset"]);
        }

        [HttpDelete("deleteMotherBoard/{id}")]
        public async Task<IActionResult> DeleteMotherBoard(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAllMotherBoards")]
        public async Task<IActionResult> GetAllMotherBoards(int limit, int offset)
        {
            return await GetAllComponents<CoolerInfo>(limit, offset, "mother_board", ["frequency", "socket", "chipset"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMotherBoard(string keyword, int limit = 1, int offset = 0)
        {
            return await SearchComponent(keyword, limit, offset);
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterMotherBoard(string country, string brand, string model, 
            int minPrice, int maxPrice, int minFrequency, int maxFrequency, int limit, int offset)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var motherBoards = connection.Query<MotherBoard<string>>(@"SELECT * FROM public.mother_board " +
                    "WHERE country = @Country AND brand = @Brand AND model = @Model " +
                    "AND price >=  @MinPrice AND price <= @MaxPrice AND " +
                    "frequency >=  @MinFrequency AND frequency <= @MaxFrequency " +
                    "LIMIT @Limit OFFSET @Offset", new { Country = country, Brand = brand, Model = model, MinPrice = minPrice,
                    MaxPrice = maxPrice, MinFrequency = minFrequency, MaxFrequency = maxFrequency,
                        Limit = limit, Offset = offset });

                    return Ok(new { motherBoards });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with country filter");
                return BadRequest(new { error = ex.Message });
            }
        }
        
        [HttpPost("addComment")]
        public async Task<IActionResult> AddComputerCaseComment(Comment computerCaseComment)
        {
            return await AddComment(computerCaseComment);
        }
        
        [HttpPut("updateComment")]
        public async Task<IActionResult> UpdateComputerCaseComment(Comment computerCaseComment)
        {
            return await UpdateComment(computerCaseComment);
        }
        
        [HttpDelete("{productId}/deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComputerCaseComment(int productId, int commentId)
        {
            return await DeleteComment(productId, commentId, "mother_board");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "mother_board", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "mother_board");
        }

        [HttpPut("likeMotherBoard/{id}")]
        public async Task<IActionResult> LikeMotherBoard(int id, User user)
        {
            return await LikeComponent(id, user, "mother_board");
        }
    }
}
