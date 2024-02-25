using backend.Entities;

namespace backend.IRepositories
{
    public interface IAssemblyRepository
    {
        Task<List<Assembly>> GetAllAssemblies();
        Task<Assembly> GetAssemblyById(long id);
        Task AddAssembly(Assembly assembly);
        Task UpdateAssembly(Assembly assembly);
        Task DeleteAssembly(long id);
        Task<List<Assembly>> GetAllAssembliesSortedByTimeAdded();
    }
}
