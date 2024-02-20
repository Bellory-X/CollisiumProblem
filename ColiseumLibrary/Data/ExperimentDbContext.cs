using ColiseumLibrary.Model.Experiments;
using Microsoft.EntityFrameworkCore;

namespace ColiseumLibrary.Data;

public sealed class ExperimentDbContext: DbContext
{
    public DbSet<ExperimentDb> ExperimentDbs { get; set; } = null!;
    public ExperimentDbContext(DbContextOptions<ExperimentDbContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}