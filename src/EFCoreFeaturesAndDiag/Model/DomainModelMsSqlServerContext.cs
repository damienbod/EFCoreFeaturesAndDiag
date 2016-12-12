using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCoreFeaturesAndDiag.Model
{
    // >dotnet ef migration add testMigration
    public class DomainModelMsSqlServerContext : DbContext
    {
        public DomainModelMsSqlServerContext(DbContextOptions<DomainModelMsSqlServerContext> options) :base(options)
        { }
        
        public DbSet<DataEventRecord> DataEventRecords { get; set; }

        public DbSet<SourceInfo> SourceInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);
            builder.Entity<SourceInfo>().HasKey(m => m.SourceInfoId);

            // shadow properties
            builder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<SourceInfo>().Property<DateTime>("UpdatedTimestamp");

            builder.Entity<DataEventRecord>()
                .Property(b => b.MadDescription)
                .HasField("_description");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            updateUpdatedProperty<SourceInfo>();
            updateUpdatedProperty<DataEventRecord>();

            return base.SaveChanges();
        }

        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}