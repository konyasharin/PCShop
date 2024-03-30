using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.User;
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

                computerCase.ProductType = "computerCase";
                computerCase.Likes = 0;

                return await CreateComponent<ComputerCase<IFormFile>>(computerCase, ["material", "width", "height", "depth"], "computer_cases");
        }

        [HttpGet("getAllComputerCases")]
        public async Task<IActionResult> GetAllComputerCases(int limit, int offset)
        {
            return await GetAllComponents<ComputerCase<string>>(limit, offset, "computer_cases_view", ["material", "width", "height", "depth"]);
        }

        [HttpGet("getComputerCase/{id}")]
        public async Task<IActionResult> GetComputerCaseById (int id)
        {
            return await GetComponent<ComputerCase<string>>(id, "computer_cases_view", ["material", "width", "height", "depth"]);
        }

        [HttpPut("updateComputerCase/{id}")]
        public async Task<IActionResult> UpdateComputerCase(int id, [FromForm] ComputerCase<IFormFile> computerCase, [FromQuery] bool isUpdated)
        {
            computerCase.ProductId = id;
            computerCase.ProductType = "computer_cases";
            return await UpdateComponent<ComputerCase<IFormFile>>(computerCase, isUpdated, "computer_cases", ["material", "width", "height", "depth"]);
        }

        [HttpDelete("deleteComputerCase/{id}")]
        public async Task<IActionResult> DeleteComputerCase(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchComputerCase(string keyword, int limit = 1, int offset = 0)
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
            return await DeleteComment(productId, commentId, "computer_case");
        }

        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "computer_case", productId);
        }

        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "computer_case");
        }

        [HttpPut("likeComputerCase/{id}")]
        public async Task<IActionResult> LikeComputerCase(int id, User user)
        {
            return await LikeComponent(id, user, "computer_case");
        }
    }
}

