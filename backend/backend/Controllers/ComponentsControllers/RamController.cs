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
    [Route("api/RAM")]
    [ApiController]
    public class RamController : ComponentController
    {
        public RamController(ILogger<RamController> logger):base(logger, "RAM")
        { }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRam([FromForm] RAM<IFormFile> ram)
        {
            ram.Likes = 0;
            ram.ProductType = ComponentType;
            return await CreateComponent<RAM<IFormFile>>(ram, ["frequency", "timings", "ram_db"], "rams");
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetRamById(int id)
        {
            return await GetComponent<RAM<string>>(id, "rams_view", ["frequency", "timings", "ram_db AS RAMDb"]);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateRam(int id, [FromForm] RAM<IFormFile> ram, [FromQuery] bool isUpdated)
        {
            ram.ProductId = id;
            ram.ProductType = ComponentType;
            return await UpdateComponent<RAM<IFormFile>>(ram, isUpdated, "rams",
                ["frequency", "timings", "ram_db"]);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRam(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllRams(int limit, int offset)
        {
            return await GetAllComponents<RAM<string>>(limit, offset, "rams_view", ["frequency", "timings", "ram_db AS RAMDb"]);
        }
        
        [HttpPost("addFilter")]
        public async Task<IActionResult> AddVideoCardFilter(Filter newFilter)
        {
            newFilter.ComponentType = ComponentType;
            return await AddFilter(newFilter);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(Filter[] filters)
        {
            return await FilterComponents<RAM<string>>("rams_view", filters, ["frequency", "timings", "ram_db AS RAMDb"]);
        }

        [HttpGet("getFilters")]
        public async Task<IActionResult> GetFilters()
        {
            return await GetComponentFilters<RAM<string>>();
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

        [HttpPut("like/{id}")]
        public async Task<IActionResult> LikeRam(int id, User user)
        {
            return await LikeComponent(id, user, "ram");
        }
    }
}
