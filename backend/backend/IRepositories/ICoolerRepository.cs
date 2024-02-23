using backend.Entities;

namespace backend.IRepositories
{
    public interface ICoolerRepository
    {
        Task<List<Cooler>> GetAllCoolers();
        Task<Cooler> GetCoolerById(long id);
        Task AddCooler(Cooler cooler);
        Task UpdateCooler(Cooler cooler);
        Task DeleteCooler(long id);
    }
}
