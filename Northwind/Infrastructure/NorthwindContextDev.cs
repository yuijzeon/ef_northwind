using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Northwind.Infrastructure;

public class NorthwindContextDev : NorthwindContext
{
    public NorthwindContextDev()
    {
        var projectRootDir = new DirectoryInfo(Directory.GetCurrentDirectory())
            .FindParent("bin")
            .Parent?.FullName ?? Directory.GetCurrentDirectory();

        DbPath = Path.Combine(projectRootDir, "northwind.db");
    }

    private string DbPath { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));
        optionsBuilder.EnableSensitiveDataLogging();
    }
}