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

        public ComputerCaseController(ILogger<ComputerCaseController> logger):base(logger, "computerCase")
        {
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCreateCase([FromForm] ComputerCase<IFormFile> computerCase)
        {
                computerCase.ProductType = ComponentType;
                computerCase.Likes = 0;

                return await CreateComponent<ComputerCase<IFormFile>>(computerCase, ["material", "width", "height", "depth"], "computer_cases");
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllComputerCases(int limit, int offset)
        {
            return await GetAllComponents<ComputerCase<string>>(limit, offset, "computer_cases_view", ["material", "width", "height", "depth"]);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetComputerCaseById (int id)
        {
            return await GetComponent<ComputerCase<string>>(id, "computer_cases_view", ["material", "width", "height", "depth"]);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateComputerCase(int id, [FromForm] ComputerCase<IFormFile> computerCase, [FromQuery] bool isUpdated)
        {
            computerCase.ProductId = id;
            computerCase.ProductType = ComponentType;
            return await UpdateComponent<ComputerCase<IFormFile>>(computerCase, isUpdated, "computer_cases", ["material", "width", "height", "depth"]);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteComputerCase(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchComputerCase(string keyword, int limit = 1, int offset = 0)
        {
            return await SearchComponent(keyword, limit, offset);
        }
        
        [HttpPost("addFilter")]
        public async Task<IActionResult> AddComputerCaseFilter(Filter newFilter)
        {
            newFilter.ComponentType = ComponentType;
            return await AddFilter(newFilter);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(Filter[] filters)
        {
            return await FilterComponents<ComputerCase<string>>("computer_cases_view", filters, ["height", "width", "depth", "material"]);
        }

        [HttpGet("getFilters")]
        public async Task<IActionResult> GetFilters()
        {
            return await GetComponentFilters<ComputerCase<string>>();
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

        [HttpPut("like/{id}")]
        public async Task<IActionResult> LikeComputerCase(int id, User user)
        {
            return await LikeComponent(id, user, "computer_case");
        }
    }
}

