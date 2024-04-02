using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.User;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/processor")]
    [ApiController]
    public class ProcessorController : ComponentController
    {
        public ProcessorController(ILogger<ProcessorController> logger):base(logger, "processor")
        {
           
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProcessor([FromForm] Processor<IFormFile> processor)
        {
            processor.Likes = 0;
            processor.ProductType = ComponentType;
            return await CreateComponent<Processor<IFormFile>>(processor, ["cores", "heat_dissipation", "clock_frequency", "turbo_frequency"], "processors");
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetProcessorById(int id)
        {
            return await GetComponent<Processor<string>>(id, "processors_view",
                ["cores", "heat_dissipation AS heatDissipation", "clock_frequency AS clockFrequency", "turbo_frequency AS turboFrequency"]);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProcessor(int id, [FromForm] Processor<IFormFile> processor, [FromQuery] bool isUpdated)
        {
            processor.ProductId = id;
            processor.ProductType = "processor";
            return await UpdateComponent<Processor<IFormFile>>(processor, isUpdated, "processors",
                ["cores", "clock_frequency", "turbo_frequency", "heat_dissipation"]);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProcessor(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllprocessors(int limit, int offset)
        {
            return await GetAllComponents<Processor<string>>(limit, offset, "processors_view",
                ["cores", "clock_frequency", "turbo_frequency", "heat_dissipation"]);
        }
        
        [HttpPost("addFilter")]
        public async Task<IActionResult> AddProcessorFilter(Filter newFilter)
        {
            newFilter.ComponentType = "processor";
            return await AddFilter(newFilter);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(Filter[] filters)
        {
            return await FilterComponents<Processor<string>>("processors_view", filters, 
                ["cores","clock_frequency AS clockFrequency", "turbo_frequency AS turboFrequency", "heat_dissipation AS heatDissipation"]);
        }

        [HttpGet("getFilters")]
        public async Task<IActionResult> GetFilters()
        {
            return await GetComponentFilters<Processor<string>>();
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

        [HttpPut("like/{id}")]
        public async Task<IActionResult> LikeProcessor(int id, User user)
        {
            return await LikeComponent(id, user, "processor");
        }
    }
}
