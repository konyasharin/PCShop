using backend.Data;
using backend.Entities;
using Microsoft.EntityFrameworkCore;
using backend.IRepositories;

namespace backend.Repositories
{
    public class AssemblyRepository : IAssemblyRepository
    {
        private readonly DataContext _context;

        public AssemblyRepository(DataContext _context)
        {
            this._context = _context;
        }

        public async Task<List<Assembly>> GetAllAssemblies()
        {
            return await _context.Assemblies.ToListAsync();
        }

        public async Task<Assembly> GetAssemblyById(long id)
        {
            return await _context.Assemblies.FindAsync(id);
        }

        public async Task AddAssembly(Assembly assembly)
        {
            var processor = await _context.Processors.FindAsync(assembly.ProcessorId);
            var computerCase = await _context.ComputerCases.FindAsync(assembly.ComputerCaseId);
            var cooler = await _context.Coolers.FindAsync(assembly.CoolerId);
            var motherboard = await _context.MotherBoards.FindAsync(assembly.MotherBoardId);
            var powerunit = await _context.PowerUnits.FindAsync(assembly.PowerUnitId);
            var ram = await _context.RAMs.FindAsync(assembly.RamId);
            var ssd = await _context.SSDs.FindAsync(assembly.SsdId);
            var videocard = await _context.VideoCards.FindAsync(assembly.VideoCardId);

            bool condition = processor != null && computerCase != null && cooler != null &&
                motherboard != null && powerunit != null && ram != null
                && ssd != null && videocard != null;

            if (condition)
            {
                assembly.Price = processor.Price + computerCase.Price + cooler.Price +
                    motherboard.Price + powerunit.Price + ram.Price + ssd.Price +
                    videocard.Price;

                _context.Assemblies.Add(assembly);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Cannot create assembly because" +
                    " one of components does not exist.");
            }
        }

        public async Task UpdateAssembly(Assembly assembly)
        {
            _context.Entry(assembly).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAssembly(long id)
        {
            var assembly = await _context.Assemblies.FindAsync(id);
            if (assembly != null)
            {
                _context.Assemblies.Remove(assembly);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Assembly>> GetAllAssembliesSortedByTimeAdded()
        {
            return await _context.Assemblies.OrderBy(a => a.Creation_time).ToListAsync();
        }
    }
}
