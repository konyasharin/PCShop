using backend.Entities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Drawing;
using backend.Entities.CommentEntities;
using backend.Entities.User;

namespace backend.Controllers
{
    [Route("api/cooler")]
    [ApiController]
    public class CoolerController : ComponentController
    {

        public CoolerController(ILogger<CoolerController> logger):base(logger, "cooler")
        { 
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCooler([FromForm] Cooler<IFormFile> cooler)
        {
                cooler.Likes = 0;
                cooler.ProductType = ComponentType;

            return await CreateComponent<Cooler<IFormFile>>(cooler, ["speed", "cooler_power"], "coolers");
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetCoolerById(int id)
        {
            return await GetComponent<Cooler<string>>(id, "coolers_view", ["speed", "cooler_power AS coolerPower"]);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCooler(int id, [FromForm] Cooler<IFormFile> cooler, [FromQuery] bool isUpdated)
        {
            cooler.ProductId = id;
            cooler.ProductType = ComponentType;
            return await UpdateComponent<Cooler<IFormFile>>(cooler, isUpdated, "coolers", ["speed", "cooler_power"]);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCooler(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCoolers(int limit, int offset)
        {
            return await GetAllComponents<Cooler<string>>(limit, offset, "coolers_view", ["speed", "cooler_power AS coolerPower"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCooler(string keyword, int limit = 1, int offset = 0)
        {
            return await SearchComponent(keyword, limit, offset);
        }
        
        [HttpPost("addFilter")]
        public async Task<IActionResult> AddCoolerFilter(Filter newFilter)
        {
            newFilter.ComponentType = ComponentType;
            return await AddFilter(newFilter);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(Filter[] filters)
        {
            return await FilterComponents<Cooler<string>>("coolers_view", filters, ["speed", "cooler_power AS coolerPower"]);
        }

        [HttpGet("getFilters")]
        public async Task<IActionResult> GetFilters()
        {
            return await GetComponentFilters<Cooler<string>>();
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
            return await DeleteComment(productId, commentId, "cooler");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "cooler", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "cooler");
        }

        [HttpPut("likeCooler/{id}")]
        public async Task<IActionResult> LikeCooler(int id, User user)
        {
            return await LikeComponent(id, user, "cooler");
        }
    }
}
