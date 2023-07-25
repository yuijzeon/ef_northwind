using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AsyncForeach.Infrastructure;

public class NorthwindContext : DbContext
{
    public NorthwindContext()
    {
        var projectRootDir = new DirectoryInfo(Directory.GetCurrentDirectory())
            .FindParent("bin")
            .Parent?.FullName ?? Directory.GetCurrentDirectory();

        DbPath = Path.Combine(projectRootDir, "northwind.db");
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Shipper> Shippers { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Territory> Territories { get; set; }

    private string DbPath { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");
            entity.HasKey(x => x.CategoryId);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customers");
            entity.HasKey(x => x.CustomerId);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employees");
            entity.HasKey(x => x.EmployeeId);

            entity.HasMany(x => x.Territories).WithMany()
                .UsingEntity<EmployeeTerritory>(
                    x => x.HasOne(et => et.Territory).WithMany(),
                    x => x.HasOne(et => et.Employee).WithMany()
                );
        });

        modelBuilder.Entity<EmployeeTerritory>(entity =>
        {
            entity.ToTable("EmployeeTerritories");
            entity.HasKey(x => new { x.EmployeeId, x.TerritoryId });
            entity.HasOne(x => x.Employee);
            entity.HasOne(x => x.Territory);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("Order Details");
            entity.HasKey(x => new { x.OrderId, x.ProductId });
            entity.HasOne(x => x.Order);
            entity.HasOne(x => x.Product);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");
            entity.HasKey(x => x.OrderId);
            entity.HasOne(x => x.Customer);
            entity.HasOne(x => x.Employee);
            entity.HasMany(x => x.OrderDetails);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(x => x.ProductId);
            entity.HasOne(x => x.Supplier);
            entity.HasOne(x => x.Category);
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.ToTable("Regions");
            entity.HasKey(x => x.RegionId);
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.ToTable("Shippers");
            entity.HasKey(x => x.ShipperId);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Suppliers");
            entity.HasKey(x => x.SupplierId);
        });

        modelBuilder.Entity<Territory>(entity =>
        {
            entity.ToTable("Territories");
            entity.HasKey(x => x.TerritoryId);
            entity.HasOne(x => x.Region);

            entity.HasMany(x => x.Employees).WithMany()
                .UsingEntity<EmployeeTerritory>(
                    x => x.HasOne(y => y.Employee).WithMany(),
                    x => x.HasOne(y => y.Territory).WithMany()
                );
        });
    }
}