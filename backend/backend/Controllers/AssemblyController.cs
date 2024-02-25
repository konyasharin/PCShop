using backend.Data;
using backend.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssemblyController : ControllerBase
    {
        private readonly ILogger<AssemblyController> logger;
        private readonly DataContext dataContext;
        private readonly IAssemblyRepository assemblyRepository;

        public AssemblyController(ILogger<AssemblyController> logger, DataContext dataContext,
            IAssemblyRepository assemblyRepository)
        {
            this.logger = logger;
            this.dataContext = dataContext;
            this.assemblyRepository = assemblyRepository;
        }
    }
}
