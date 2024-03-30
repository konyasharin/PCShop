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
    [Route("api/[controller]")]
    [ApiController]
    public class PowerUnitController : ComponentController
    {
       
        public PowerUnitController(ILogger<PowerUnitController> logger):base(logger)
        {
        }

        [HttpPost("createPowerUnit")]
        public async Task<IActionResult> CreatePowerUnit([FromForm] PowerUnit<IFormFile> powerunit)
        {


            if (powerunit.Voltage < 0 || powerunit.Voltage > 50000)
            {
                return BadRequest(new { error = "Voltage must be between 0 and 50000" });
            }

            powerunit.Likes = 0;
            powerunit.ProductType = "powerUnit";

            return await CreateComponent<PowerUnit<IFormFile>>(powerunit, ["battery", "voltage"], "power_units");
        }

        [HttpGet("getPowerUnit/{id}")]
        public async Task<IActionResult> GetPowerUnitById(int id)
        {
            return await GetComponent<PowerUnit<string>>(id, "power_units_view", ["battery", "voltage"]);
        }

        [HttpPut("updatePowerUnit/{id}")]
        public async Task<IActionResult> UpdatePowerUnit(int id, [FromForm] PowerUnit<IFormFile> powerUnit, [FromQuery] bool isUpdated)
        {
            powerUnit.ProductId = id;
            powerUnit.ProductType = "power_unit";
            return await UpdateComponent<PowerUnit<IFormFile>>(powerUnit, isUpdated, "power_units", ["battery", "voltage"]);
        }

        [HttpDelete("deletePowerUnit/{id}")]
        public async Task<IActionResult> DeletePowerUnit(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAllPowerUnits")]
        public async Task<IActionResult> GetAllPowerUnits(int limit, int offset)
        {
            return await GetAllComponents<PowerUnit<string>>(limit, offset, "power_units_view", ["battery", "voltage"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPowerUnit(string keyword, int limit = 1, int offset = 0)
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

        [HttpPut("likePowerUnit/{id}")]
        public async Task<IActionResult> LikePowerUnit(int id, User user)
        {
            return await LikeComponent(id, user, "power_unit");
        }
    }
}
