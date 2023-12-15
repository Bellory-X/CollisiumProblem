using ColiseumLibrary.Model.Experiments;
using Microsoft.EntityFrameworkCore;

namespace GodsApi.Data;

public sealed class ExperimentDbContext: DbContext
{
    public DbSet<ExperimentDbModel> ExperimentDbModels { get; set; } = null!;
    public ExperimentDbContext(DbContextOptions<ExperimentDbContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}