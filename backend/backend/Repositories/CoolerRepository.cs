using backend.Data;
using backend.Entities;
using Microsoft.EntityFrameworkCore;
using backend.IRepositories;

namespace backend.Repositories
{
    public class CoolerRepository : ICoolerRepository
    {
        private readonly DataContext _context;

        public CoolerRepository(DataContext _context)
        {
            this._context = _context;
        }

        public async Task<List<Cooler>> GetAllCoolers()
        {
            return await _context.Coolers.ToListAsync();
        }

        public async Task<Cooler> GetCoolerById(long id)
        {
            return await _context.Coolers.FindAsync(id);
        }

        public async Task AddCooler(Cooler сooler)
        {
            _context.Coolers.Add(сooler);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCooler(Cooler сooler)
        {
            _context.Entry(сooler).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCooler(long id)
        {
            var сooler = await _context.Coolers.FindAsync(id);
            if (сooler != null)
            {
                _context.Coolers.Remove(сooler);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}
