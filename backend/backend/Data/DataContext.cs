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
        public DbSet<Assembly> Assemblies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assembly>()
                .HasKey(a => new
                {
                    a.SsdId,
                    a.CoolerId,
                    a.RamId,
                    a.MotherBoardId,
                    a.ComputerCaseId,
                    a.ProcessorId,
                    a.PowerUnitId,
                    a.VideoCardId
        });

            modelBuilder.Entity<Assembly>()
                .HasOne(a => a.computerCase)  
                .WithOne(cc => cc.assembly)  
                .HasForeignKey<Assembly>(a => a.ComputerCaseId);

            modelBuilder.Entity<Assembly>()
                .HasOne(a => a.processor)
                .WithOne(p => p.assembly)
                .HasForeignKey<Assembly>(a => a.ProcessorId);

            modelBuilder.Entity<Assembly>()
                .HasOne(a => a.cooler)
                .WithOne(p => p.assembly)
                .HasForeignKey<Assembly>(a => a.CoolerId);

            modelBuilder.Entity<Assembly>()
                .HasOne(a => a.motherBoard)
                .WithOne(p => p.assembly)
                .HasForeignKey<Assembly>(a => a.MotherBoardId);

            modelBuilder.Entity<Assembly>()
                .HasOne(a => a.powerUnit)
                .WithOne(p => p.assembly)
                .HasForeignKey<Assembly>(a => a.PowerUnitId);

            modelBuilder.Entity<Assembly>()
                .HasOne(a => a.ram)
                .WithOne(p => p.assembly)
                .HasForeignKey<Assembly>(a => a.RamId);

            modelBuilder.Entity<Assembly>()
                .HasOne(a => a.ssd)
                .WithOne(p => p.assembly)
                .HasForeignKey<Assembly>(a => a.SsdId);

            modelBuilder.Entity<Assembly>()
                .HasOne(a => a.videoCard)
                .WithOne(p => p.assembly)
                .HasForeignKey<Assembly>(a => a.VideoCardId);

        }


    }
}
