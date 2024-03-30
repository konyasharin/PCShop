using backend.Entities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Runtime.Intrinsics.Arm;
using backend.Entities.CommentEntities;
using backend.Entities.User;
using System.Diagnostics;

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

            ram.Likes = 0;
            ram.ProductType = "RAM";
            return await CreateComponent<RAM<IFormFile>>(ram, ["frequency", "timings", "capacity_db"], "rams");
        }

        [HttpGet("getRam/{id}")]
        public async Task<IActionResult> GetRamById(int id)
        {
            return await GetComponent<RAM<string>>(id, "rams_view", ["battery", "voltage"]);
        }

        [HttpPut("updateRam/{id}")]
        public async Task<IActionResult> UpdateRam(int id, [FromForm] RAM<IFormFile> ram, [FromQuery] bool isUpdated)
        {
            ram.ProductId = id;
            ram.ProductType = "ram";
            return await UpdateComponent<RAM<IFormFile>>(ram, isUpdated, "rams",
                ["frequency", "timings", "capacity_db"]);
        }

        [HttpDelete("deleteRam/{id}")]
        public async Task<IActionResult> DeleteRam(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAllRam")]
        public async Task<IActionResult> GetAllRams(int limit, int offset)
        {
            return await GetAllComponents<RAM<string>>(limit, offset, "rams_view", ["frequency", "timings", "capacity_db"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRam(string keyword, int limit = 1, int offset = 0)
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
            return await DeleteComment(productId, commentId, "ram");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "ram", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "ram");
        }

        [HttpPut("likeRam/{id}")]
        public async Task<IActionResult> LikeRam(int id, User user)
        {
            return await LikeComponent(id, user, "ram");
        }
    }
}
