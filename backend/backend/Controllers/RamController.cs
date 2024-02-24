﻿using backend.Data;
using backend.Entities;
using backend.IRepositories;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamController : ControllerBase
    {
        private readonly ILogger<RamController> logger;
        private readonly DataContext dataContext;
        private readonly IRamRepository ramRepository;

        public RamController(ILogger<RamController> logger, DataContext dataContext,
            IRamRepository ramRepository)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.ramRepository = ramRepository;
        }

        [HttpPost("createRam")]
        public async Task<IActionResult> CreateRam(RAM ram)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await ramRepository.AddRam(ram);

                logger.LogInformation("Ram created with ID {RamId}", ram.Id);

                return Ok(new
                {
                    Component = "Ram",
                    id = ram.Id,
                    Data = ram
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating Ram");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRams()
        {
            try
            {
                var rams = await ramRepository.GetAllRams();
                return Ok(rams);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all RAMs");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRamById(long id)
        {
            try
            {
                var ram = await ramRepository.GetRamById(id);

                if (ram == null)
                {
                    return NotFound();
                }

                return Ok(ram);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting RAM");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRam(long id, RAM ram)
        {
            if (id != ram.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await ramRepository.UpdateRam(ram);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating RAM");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRam(long id)
        {
            try
            {
                await ramRepository.DeleteRam(id);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting RAM");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}