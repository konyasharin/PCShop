using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.ComponentsInfo;
using backend.Entities.User;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessorController : ComponentController
    {
        
        public ProcessorController(ILogger<ProcessorController> logger):base(logger)
        {
           
        }

        [HttpPost("createProcessor")]
        public async Task<IActionResult> CreateProcessor([FromForm] Processor<IFormFile> processor)
        {
            processor.Likes = 0;
            processor.ProductType = "processor";
            return await CreateComponent<Processor<IFormFile>>(processor, ["cores", "heat_dissipation", "clock_frequency", "turbo_frequency"], "processor");
        }

        [HttpGet("getProcessor/{id}")]
        public async Task<IActionResult> GetProcessorById(int id)
        {
            return await GetComponent<Processor<string>>(id, "power_unit", ["battery", "voltage"]);
        }

        [HttpPut("updateProcessor/{id}")]
        public async Task<IActionResult> UpdateProcessor(int id, [FromForm] Processor<IFormFile> processor, [FromQuery] bool isUpdated)
        {
            processor.ProductId = id;
            processor.ProductType = "processor";
            return await UpdateComponent<Processor<IFormFile>>(processor, isUpdated, "processor",
                ["cores", "clock_frequency", "turbo_frequency", "heat_dissipation"]);
        }

        [HttpDelete("deleteProcessor/{id}")]
        public async Task<IActionResult> DeleteProcessor(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAllProcessors")]
        public async Task<IActionResult> GetAllprocessors(int limit, int offset)
        {
            return await GetAllComponents<Processor<string>>(limit, offset, "processor",
                ["cores", "clock_frequency", "turbo_frequency", "heat_dissipation"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProcessor(string keyword, int limit = 1, int offset = 0)
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
            return await DeleteComment(productId, commentId, "processor");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "processor", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "processor");
        }

        [HttpPut("likeProcessor/{id}")]
        public async Task<IActionResult> LikeProcessor(int id, User user)
        {
            return await LikeComponent(id, user, "processor");
        }
    }
}
