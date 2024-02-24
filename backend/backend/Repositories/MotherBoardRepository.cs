using backend.Data;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class MotherBoardRepository
    {
        private readonly DataContext _context;

        public MotherBoardRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<MotherBoard>> GetAllMotherBoards()
        {
            return await _context.MotherBoards.ToListAsync();
        }

        public async Task<MotherBoard> GetMotherBoardById(int id)
        {
            return await _context.MotherBoards.FindAsync(id);
        }

        public async Task AddMotherBoard(MotherBoard motherBoard)
        {
            _context.MotherBoards.Add(motherBoard);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMotherBoard(MotherBoard motherBoard)
        {
            _context.Entry(motherBoard).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMotherBoard(int id)
        {
            var motherBoard = await _context.MotherBoards.FindAsync(id);
            if (motherBoard != null)
            {
                _context.MotherBoards.Remove(motherBoard);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<MotherBoard> GetMotherBoardById(long id)
        {
            return await _context.MotherBoards.FindAsync(id);
        }

        public async Task DeleteMotherBoard(long id)
        {
            var motherBoard = await _context.MotherBoards.FindAsync(id);
            if (motherBoard != null)
            {
                _context.MotherBoards.Remove(motherBoard);
                await _context.SaveChangesAsync();
            }
        }
    }
}
