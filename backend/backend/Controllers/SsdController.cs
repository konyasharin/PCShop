using backend.Entities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Runtime.Intrinsics.X86;
using backend.Entities.CommentEntities;
using System.Runtime.Intrinsics.Arm;
using backend.Entities.User;
using backend.Entities.ComponentsInfo;
using System.Diagnostics;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SsdController : ComponentController
    {
       
        public SsdController(ILogger<SsdController> logger) : base(logger)
        {
        }

        [HttpPost("createSsd")]
        public async Task<IActionResult> CreateSsd([FromForm] SSD<IFormFile> ssd)
        {
            
            if (ssd.Capacity < 0 || ssd.Capacity > 10000)
            {
                return BadRequest(new { error = "Capacity must be between 0 and 10000" });
            }

            ssd.Likes = 0;
            ssd.ProductType = "ssd";
            return await CreateComponent<SSD<IFormFile>>(ssd, ["capacity"], "ssd");
        }

        [HttpGet("getSsd/{id}")]
        public async Task<IActionResult> GetSsdById(int id)
        {
            return await GetComponent<SSD<string>>(id, "power_unit", ["battery", "voltage"]);
        }

        [HttpPut("updateSsd/{id}")]
        public async Task<IActionResult> UpdateSsd(int id, [FromForm] SSD<IFormFile> ssd, [FromQuery] bool isUpdated)
        {
            ssd.ProductId = id;
            ssd.ProductType = "ssd";
            return await UpdateComponent<SSD<IFormFile>>(ssd, isUpdated, "ssd", ["capacity"]);
        }

        [HttpDelete("deleteSsd/{id}")]
        public async Task<IActionResult> DeleteSsd(int id)
        {
            return await DeleteComponent(id);
        }


        [HttpGet("getAllSsd")]
        public async Task<IActionResult> GetAllSsd(int limit, int offset)
        {
            return await GetAllComponents<SSD<string>>(limit, offset, "processor", ["capacity"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchSsd(string keyword, int limit = 1, int offset = 0)
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
            return await DeleteComment(productId, commentId, "ssd");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "ssd", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "ssd");
        }

        [HttpPut("likeSsd/{id}")]
        public async Task<IActionResult> LikeSsd(int id, User user)
        {
            return await LikeComponent(id, user, "ssd");
        }
    }
}
