using Microsoft.EntityFrameworkCore;

namespace Northwind.Infrastructure;

public class NorthwindContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Shipper> Shippers { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Territory> Territories { get; set; }

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

            entity.Property(x => x.RegionInfo)
                .HasColumnName("RegionId")
                .HasConversion(
                    x => x.Id,
                    x => RegionInfo.Parse(x)
                );

            entity.HasMany(x => x.Employees).WithMany()
                .UsingEntity<EmployeeTerritory>(
                    x => x.HasOne(y => y.Employee).WithMany(),
                    x => x.HasOne(y => y.Territory).WithMany()
                );
        });
    }
}