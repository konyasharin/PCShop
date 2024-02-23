﻿using backend.Data;
using backend.Entities;
using Microsoft.EntityFrameworkCore;
using backend.IRepositories;

namespace backend.Repositories
{
    public class ComputerCaseRepository : IComputerCaseRepository
    {
        private readonly DataContext _context;

        public ComputerCaseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ComputerCase>> GetAllComputerCases()
        {
            return await _context.ComputerCases.ToListAsync();
        }

        public async Task<ComputerCase> GetComputerCaseById(int id)
        {
            return await _context.ComputerCases.FindAsync(id);
        }

        public async Task AddComputerCase(ComputerCase computerCase)
        {
            _context.ComputerCases.Add(computerCase);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComputerCase(ComputerCase computerCase)
        {
            _context.Entry(computerCase).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComputerCase(int id)
        {
            var computerCase = await GetComputerCaseById(id);
            if (computerCase != null)
            {
                _context.ComputerCases.Remove(computerCase);
                await _context.SaveChangesAsync();
            }
        }

        public Task<ComputerCase> GetComputerCaseById(long id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteComputerCase(long id)
        {
            throw new NotImplementedException();
        }
    }
}
