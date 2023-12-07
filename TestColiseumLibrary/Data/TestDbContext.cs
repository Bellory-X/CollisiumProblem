using GodsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TestColiseumLibrary.Data;

public sealed class TestDbContext: DbContext
{
    public TestDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("FileName=testdb.db");
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<ExperimentDbModel> ExperimentDbModels { get; set; }
}