using backend.Entities;
using backend.IRepositories;
using backend.IServices;

namespace backend.Services
{
    public class ComputerCaseService : IComputerCaseService
    {
        private readonly IComputerCaseRepository _repository;

        public ComputerCaseService(IComputerCaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ComputerCase>> GetAllComputerCases()
        {
            return await _repository.GetAllComputerCases();
        }

        public async Task<ComputerCase> GetComputerCaseById(long id)
        {
            return await _repository.GetComputerCaseById(id);
        }

        public async Task AddComputerCase(ComputerCase computerCase)
        {
            await _repository.AddComputerCase(computerCase);
        }

        public async Task UpdateComputerCase(ComputerCase computerCase)
        {
            await _repository.UpdateComputerCase(computerCase);
        }

        public async Task DeleteComputerCase(long id)
        {
            await _repository.DeleteComputerCase(id);
        }

        public Task<ComputerCase> GetComputerCaseById(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteComputerCase(int id)
        {
            throw new NotImplementedException();
        }
    }
}
