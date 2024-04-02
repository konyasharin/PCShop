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
using System.Diagnostics;

namespace backend.Controllers
{
    [Route("api/SSD")]
    [ApiController]
    public class SsdController : ComponentController
    {
       
        public SsdController(ILogger<SsdController> logger) : base(logger, "SSD")
        {
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSsd([FromForm] SSD<IFormFile> ssd)
        {
            ssd.Likes = 0;
            ssd.ProductType = ComponentType;
            return await CreateComponent<SSD<IFormFile>>(ssd, ["capacity"], "ssds");
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetSsdById(int id)
        {
            return await GetComponent<SSD<string>>(id, "ssds_view", ["capacity"]);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSsd(int id, [FromForm] SSD<IFormFile> ssd, [FromQuery] bool isUpdated)
        {
            ssd.ProductId = id;
            ssd.ProductType = ComponentType;
            return await UpdateComponent<SSD<IFormFile>>(ssd, isUpdated, "ssds", ["capacity"]);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSsd(int id)
        {
            return await DeleteComponent(id);
        }


        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllSsd(int limit, int offset)
        {
            return await GetAllComponents<SSD<string>>(limit, offset, "ssds_view", ["capacity"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchSsd(string keyword, int limit = 1, int offset = 0)
        {
            return await SearchComponent(keyword, limit, offset);
        }
        
        [HttpPost("addFilter")]
        public async Task<IActionResult> AddSSDFilter(Filter newFilter)
        {
            newFilter.ComponentType = ComponentType;
            return await AddFilter(newFilter);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(Filter[] filters)
        {
            return await FilterComponents<SSD<string>>("ssds_view", filters, ["capacity"]);
        }

        [HttpGet("getFilters")]
        public async Task<IActionResult> GetFilters()
        {
            return await GetComponentFilters<SSD<string>>();
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

        [HttpPut("like/{id}")]
        public async Task<IActionResult> LikeSsd(int id, User user)
        {
            return await LikeComponent(id, user, "ssd");
        }
    }
}
