﻿using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.User;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Npgsql;
using System.Drawing;

namespace backend.Controllers
{
    [Route("api/motherBoard")]
    [ApiController]
    public class MotherBoardController : ComponentController
    {

        public MotherBoardController(ILogger<MotherBoardController> logger):base(logger, "motherBoard")
        {
          
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMotherBoard([FromForm] MotherBoard<IFormFile> motherBoard)
        {
            motherBoard.Likes = 0;
            motherBoard.ProductType = ComponentType;

            return await CreateComponent<MotherBoard<IFormFile>>(motherBoard, ["socket", "chipset"], "mother_boards");
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetComputerCaseById(int id)
        {
            return await GetComponent<MotherBoard<string>>(id, "mother_boards_view", ["socket", "chipset"]);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateMotherBoard(int id, [FromForm] MotherBoard<IFormFile> motherBoard, [FromQuery] bool isUpdated)
        {
            motherBoard.ProductId = id;
            motherBoard.ProductType = "mother_board";
            return await UpdateComponent<MotherBoard<IFormFile>>(motherBoard, isUpdated, "mother_boards", ["socket", "chipset"]);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteMotherBoard(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllMotherBoards(int limit, int offset)
        {
            return await GetAllComponents<MotherBoard<string>>(limit, offset, "mother_boards_view", ["socket", "chipset"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMotherBoard(string keyword, int limit = 1, int offset = 0)
        {
            return await SearchComponent(keyword, limit, offset);
        }
        
        [HttpPost("addFilter")]
        public async Task<IActionResult> AddMotherBoardFilter(Filter newFilter)
        {
            newFilter.ComponentType = "motherBoard";
            return await AddFilter(newFilter);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(Filter[] filters)
        {
            return await FilterComponents<MotherBoard<string>>("mother_boards_view", filters, ["chipset", "socket"]);
        }

        [HttpGet("getFilters")]
        public async Task<IActionResult> GetFilters()
        {
            return await GetComponentFilters<MotherBoard<string>>();
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
            return await DeleteComment(productId, commentId, "mother_board");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "mother_board", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "mother_board");
        }

        [HttpPut("like/{id}")]
        public async Task<IActionResult> LikeMotherBoard(int id, User user)
        {
            return await LikeComponent(id, user, "mother_board");
        }
    }
}
