using backend.Entities;
using System.Runtime.Intrinsics.Arm;

namespace backend.IRepositories
{
    public interface IRamRepository
    {
        Task<List<RAM>> GetAllRams();
        Task<Processor> GetRamById(long id);
        Task AddRam(RAM ram);
        Task UpdateRam(RAM ram);
        Task DeleteRam(long id);
    }
}
