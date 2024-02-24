﻿using backend.Data;
using backend.Entities;
using backend.IRepositories;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SsdController : ControllerBase
    {
        private readonly ILogger<SsdController> logger;
        private readonly DataContext dataContext;
        private readonly ISsdRepository ssdRepository;

        public SsdController(ILogger<SsdController> logger, DataContext dataContext,
            ISsdRepository ssdRepository)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.ssdRepository = ssdRepository;
        }

        [HttpPost("createSsd")]
        public async Task<IActionResult> CreateSsd(SSD ssd)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await ssdRepository.AddSsd(ssd);

                logger.LogInformation("Ssd created with ID {SsdId}", ssd.Id);

                return Ok(new
                {
                    Component = "Ssd",
                    id = ssd.Id,
                    Data = ssd
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating Ssd");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSsds()
        {
            try
            {
                var ssds = await ssdRepository.GetAllSsds();
                return Ok(ssds);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all SSDs");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSsdById(long id)
        {
            try
            {
                var ssd = await ssdRepository.GetSsdById(id);

                if (ssd == null)
                {
                    return NotFound();
                }

                return Ok(ssd);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting SSD");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRam(long id, SSD ssd)
        {
            if (id != ssd.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await ssdRepository.UpdateSsd(ssd);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating SSD");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSsd(long id)
        {
            try
            {
                await ssdRepository.DeleteSsd(id);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting SSD");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}