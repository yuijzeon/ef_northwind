namespace Northwind.Infrastructure;

public class Territory
{
    public string TerritoryId { get; set; }

    public string TerritoryDescription { get; set; }
    public virtual RegionInfo RegionInfo { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
}