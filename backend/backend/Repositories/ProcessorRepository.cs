using backend.Data;
using backend.Entities;
using Microsoft.EntityFrameworkCore;
using backend.IRepositories;

namespace backend.Repositories
{
    public class ProcessorRepository : IProcessorRepository
    {
        private readonly DataContext _context;

        public ProcessorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Processor>> GetAllProcessors()
        {
            return await _context.Processors.ToListAsync();
        }

        public async Task<Processor> GetProcessorById(int id)
        {
            return await _context.Processors.FindAsync(id);
        }

        public async Task AddProcessor(Processor processor)
        {
            _context.Processors.Add(processor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProcessor(Processor processor)
        {
            _context.Entry(processor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProcessor(int id)
        {
            var processor = await _context.Processors.FindAsync(id);
            if (processor != null)
            {
                _context.Processors.Remove(processor);
                await _context.SaveChangesAsync();
            }
        }

    

        public Task DeleteProcessor(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Processor> GetProcessorById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
