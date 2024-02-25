using backend.Data;
using backend.Entities;
using backend.IRepositories;
using Microsoft.EntityFrameworkCore;
using backend.IRepositories;

namespace backend.Repositories
{
    public class SsdRepository : ISsdRepository
    {
        private readonly DataContext _context;

        public SsdRepository(DataContext _context)
        {
            this._context = _context;
        }

        public async Task<List<SSD>> GetAllSsds()
        {
            return await _context.SSDs.ToListAsync();
        }

        public async Task<SSD> GetSsdById(long id)
        {
            return await _context.SSDs.FindAsync(id);
        }

        public async Task AddSsd(SSD ssd)
        {
            _context.SSDs.Add(ssd);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSsd(SSD ssd)
        {
            _context.Entry(ssd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSsd(long id)
        {
            var ssd = await _context.SSDs.FindAsync(id);
            if (ssd != null)
            {
                _context.SSDs.Remove(ssd);
                await _context.SaveChangesAsync();
            }
        }

        Task<SSD> ISsdRepository.GetSsdById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
