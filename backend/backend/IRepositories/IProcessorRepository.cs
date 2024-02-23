using backend.Entities;

namespace backend.IRepositories
{
    public interface IProcessorRepository
    {
        Task<List<Processor>> GetAllProcessors();
        Task<Processor> GetProcessorById(long id);
        Task AddProcessor(Processor processor);
        Task UpdateProcessor(Processor processor);
        Task DeleteProcessor(long id);
    }
}
