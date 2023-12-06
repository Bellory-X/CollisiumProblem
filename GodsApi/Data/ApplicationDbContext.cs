using GodsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GodsApi.Data;

public sealed class ApplicationDbContext: DbContext
{
    public ApplicationDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("FileName=sqlitedb1.db");
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<ExperimentDbModel> ExperimentDbModels { get; set; }
}