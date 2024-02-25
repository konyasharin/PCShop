using backend.Data;
using backend.Entities;
using Microsoft.EntityFrameworkCore;
using backend.IRepositories;

namespace backend.Repositories
{
    public class RamRepository : IRamRepository
    {
        private readonly DataContext _context;

        public RamRepository(DataContext _context)
        {
            this._context = _context;
        }

        public async Task<List<RAM>> GetAllRams()
        {
            return await _context.RAMs.ToListAsync();
        }

        public async Task<RAM> GetRamById(long id)
        {
            return await _context.RAMs.FindAsync(id);
        }

        public async Task AddRam(RAM ram)
        {
            _context.RAMs.Add(ram);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRam(RAM ram)
        {
            _context.Entry(ram).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRam(long id)
        {
            var ram = await _context.RAMs.FindAsync(id);
            if (ram != null)
            {
                _context.RAMs.Remove(ram);
                await _context.SaveChangesAsync();
            }
        }

        Task<RAM> IRamRepository.GetRamById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
