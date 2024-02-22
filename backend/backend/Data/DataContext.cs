using Microsoft.EntityFrameworkCore;
using backend.Entities;

namespace backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ComputerCase> ComputerCases { get; set; }
    }
}
