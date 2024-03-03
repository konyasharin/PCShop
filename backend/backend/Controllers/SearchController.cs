using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ComponentController
    {
        public SearchController(ILogger<SearchController> logger) : base(logger)
        { }

        
    }
}
