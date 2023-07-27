namespace Northwind.Infrastructure;

public class RegionInfo
{
    public int Id { get; set; }

    public string Name { get; set; }

    public static RegionInfo Parse(int id)
    {
        return new RegionInfo
        {
            Id = id,
            Name = id switch
            {
                1 => "Eastern",
                2 => "Western",
                3 => "Northern",
                4 => "Southern",
                _ => "Unknown"
            }
        };
    }
}