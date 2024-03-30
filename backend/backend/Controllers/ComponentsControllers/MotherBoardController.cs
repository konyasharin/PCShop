using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.User;
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
            motherBoard.ProductType = "motherBoard";

            return await CreateComponent<MotherBoard<IFormFile>>(motherBoard, ["frequency", "socket", "chipset"], "mother_boards");
        }


    

        [HttpGet("getMotherBoard/{id}")]
        public async Task<IActionResult> GetComputerCaseById(int id)
        {
            return await GetComponent<MotherBoard<string>>(id, "mother_boards_view", ["frequency", "socket", "chipset"]);
        }

        [HttpPut("updateMotherBoard/{id}")]
        public async Task<IActionResult> UpdateMotherBoard(int id, [FromForm] MotherBoard<IFormFile> motherBoard, [FromQuery] bool isUpdated)
        {
            motherBoard.ProductId = id;
            motherBoard.ProductType = "mother_board";
            return await UpdateComponent<MotherBoard<IFormFile>>(motherBoard, isUpdated, "mother_boards", ["frequency", "socket", "chipset"]);
        }

        [HttpDelete("deleteMotherBoard/{id}")]
        public async Task<IActionResult> DeleteMotherBoard(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAllMotherBoards")]
        public async Task<IActionResult> GetAllMotherBoards(int limit, int offset)
        {
            return await GetAllComponents<MotherBoard<string>>(limit, offset, "mother_boards_view", ["frequency", "socket", "chipset"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMotherBoard(string keyword, int limit = 1, int offset = 0)
        {
            return await SearchComponent(keyword, limit, offset);
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
