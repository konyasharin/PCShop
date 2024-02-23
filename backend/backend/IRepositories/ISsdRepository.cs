using backend.Entities;

namespace backend.IRepositories
{
    public interface ISsdRepository
    {
        Task<List<SSD>> GetAllSsds();
        Task<Processor> GetSsdById(long id);
        Task AddSsd(SSD ssd);
        Task UpdateSsd(SSD ssd);
        Task DeleteSsd(long id);
    }
}
