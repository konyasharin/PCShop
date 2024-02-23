using backend.Entities;

namespace backend.IRepositories
{
    public interface IComputerCaseRepository
    {
        Task<List<ComputerCase>> GetAllComputerCases();
        Task<ComputerCase> GetComputerCaseById(long id);
        Task AddComputerCase(ComputerCase computerCase);
        Task UpdateComputerCase(ComputerCase computerCase);
        Task DeleteComputerCase(long id);
    }
}
