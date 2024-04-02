using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.User;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/powerUnit")]
    [ApiController]
    public class PowerUnitController : ComponentController
    {
        public PowerUnitController(ILogger<PowerUnitController> logger):base(logger, "powerUnit")
        {
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePowerUnit([FromForm] PowerUnit<IFormFile> powerUnit)
        {
            powerUnit.Likes = 0;
            powerUnit.ProductType = ComponentType;

            return await CreateComponent<PowerUnit<IFormFile>>(powerUnit, ["battery", "voltage"], "power_units");
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetPowerUnitById(int id)
        {
            return await GetComponent<PowerUnit<string>>(id, "power_units_view", ["battery", "voltage"]);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePowerUnit(int id, [FromForm] PowerUnit<IFormFile> powerUnit, [FromQuery] bool isUpdated)
        {
            powerUnit.ProductId = id;
            powerUnit.ProductType = ComponentType;
            return await UpdateComponent<PowerUnit<IFormFile>>(powerUnit, isUpdated, "power_units", ["battery", "voltage"]);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePowerUnit(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllPowerUnits(int limit, int offset)
        {
            return await GetAllComponents<PowerUnit<string>>(limit, offset, "power_units_view", ["battery", "voltage"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPowerUnit(string keyword, int limit = 1, int offset = 0)
        {
            return await SearchComponent(keyword, limit, offset);
        }
        
        [HttpPost("addFilter")]
        public async Task<IActionResult> AddPowerUnitFilter(Filter newFilter)
        {
            newFilter.ComponentType = ComponentType;
            return await AddFilter(newFilter);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(Filter[] filters)
        {
            return await FilterComponents<PowerUnit<string>>("power_units_view", filters, ["battery", "voltage"]);
        }

        [HttpGet("getFilters")]
        public async Task<IActionResult> GetFilters()
        {
            return await GetComponentFilters<PowerUnit<string>>();
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
            return await DeleteComment(productId, commentId, "power_unit");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "power_unit", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "power_unit");
        }

        [HttpPut("like/{id}")]
        public async Task<IActionResult> LikePowerUnit(int id, User user)
        {
            return await LikeComponent(id, user, "power_unit");
        }
    }
}
