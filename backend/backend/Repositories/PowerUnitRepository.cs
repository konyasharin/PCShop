using backend.Data;
using backend.Entities;
using Microsoft.EntityFrameworkCore;
using backend.IRepositories;

namespace backend.Repositories
{
    public class PowerUnitRepository : IPowerUnitRepository
    {
        private readonly DataContext _context;

        public PowerUnitRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<PowerUnit>> GetAllPowerUnits()
        {
            return await _context.PowerUnits.ToListAsync();
        }

        public async Task<PowerUnit> GetPowerUnitById(long id)
        {
            return await _context.PowerUnits.FindAsync(id);
        }

        public async Task AddPowerUnit(PowerUnit powerUnit)
        {
            _context.PowerUnits.Add(powerUnit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePowerUnit(PowerUnit powerUnit)
        {
            _context.Entry(powerUnit).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePowerUnit(long id)
        {
            var powerUnit = await _context.PowerUnits.FindAsync(id);
            if (powerUnit != null)
            {
                _context.PowerUnits.Remove(powerUnit);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}
