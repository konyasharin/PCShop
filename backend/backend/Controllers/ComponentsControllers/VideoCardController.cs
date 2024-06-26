﻿using backend.Entities;
using backend.Entities.CommentEntities;
using backend.Entities.User;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;

namespace backend.Controllers
{
    [Route("api/videoCard")]
    [ApiController]
    public class VideoCardController : ComponentController
    {
        public VideoCardController(ILogger<VideoCardController> logger) : base(logger, "videoCard")
        {
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateVideoCard([FromForm] VideoCard<IFormFile> videoCard)
        {
            
            videoCard.Likes = 0;
            videoCard.ProductType = ComponentType;

            return await CreateComponent<VideoCard<IFormFile>>(videoCard, ["memory_db", "memory_type"], "video_cards");
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetVideoCardById(int id)
        {
            return await GetComponent<VideoCard<string>>(id, "video_cards_view", ["memory_db AS memoryDb", "memory_type AS memoryType"]);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateVideoCard(int id, [FromForm] VideoCard<IFormFile> videoCard, [FromQuery] bool isUpdated)
        {
            videoCard.ProductId = id;
            videoCard.ProductType = ComponentType;
            return await UpdateComponent<VideoCard<IFormFile>>(videoCard, isUpdated, "video_cards",
                ["memory_db", "memory_type"]);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVideoCard(int id)
        {
            return await DeleteComponent(id);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllComputerCases(int limit, int offset)
        {
            return await GetAllComponents<VideoCard<string>>(limit, offset, "video_cards_view",
               ["memory_db AS memoryDb", "memory_type AS memoryType"]);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRam(string keyword, int limit = 1, int offset = 0)
        {
            return await SearchComponent(keyword, limit, offset);
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
            return await FilterComponents<VideoCard<string>>("video_cards_view", filters, ["memory_db AS memoryDb", "memory_type AS memoryType"]);
        }

        [HttpGet("getFilters")]
        public async Task<IActionResult> GetFilters()
        {
            return await GetComponentFilters<VideoCard<string>>();
        }
        
        [HttpPost("addComment")]
        public async Task<IActionResult> AddComputerCaseComment(Comment videoCardComment)
        {
            return await AddComment(videoCardComment);
        }
        
        [HttpPut("updateComment")]
        public async Task<IActionResult> UpdateComputerCaseComment(Comment videoCardComment)
        {
            return await UpdateComment(videoCardComment);
        }
        
        [HttpDelete("{productId}/deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComputerCaseComment(int productId, int commentId)
        {
            return await DeleteComment(productId, commentId, "video_card");
        }
        
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetComputerCaseAllComments(int productId, int limit = 1, int offset = 0)
        {
            return await GetAllComments(limit, offset, "video_card", productId);
        }
        
        [HttpGet("{productId}/getComment/{commentId}")]
        public async Task<IActionResult> GetComputerCaseComment(int productId, int commentId)
        {
            return await GetComment(productId, commentId, "video_card");
        }

        [HttpPut("like/{id}")]
        public async Task<IActionResult> LikeVideoCard(int id, User user)
        {
            return await LikeComponent(id, user, "video_card");
        }
    }
}
