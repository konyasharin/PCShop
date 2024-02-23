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
        public DbSet<Cooler> Coolers { get; set; }
        public DbSet<MotherBoard> MotherBoards { get; set; }
        public DbSet<PowerUnit> PowerUnits { get; set; }
        public DbSet<Processor> Processors { get; set; }
        public DbSet<RAM> RAMs { get; set; }
        public DbSet<SSD> SSDs { get; set; }
        public DbSet<VideoCard> VideoCards { get; set; }
    }
}
