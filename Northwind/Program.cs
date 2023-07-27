using System.Text.Json;
using System.Text.Json.Serialization;
using Northwind.Infrastructure;
using Microsoft.EntityFrameworkCore;

var northwindContext = new NorthwindContextDev();

// ERROR: The LINQ expression could not be translated.
/*
var categories = await northwindContext.Territories
    .Where(x => x.RegionInfo.Id == 1)
    .ToListAsync();
*/

// SUCCESS: The LINQ expression could be translated.
var categories = await northwindContext.Territories
    .Where(x => x.RegionInfo == RegionInfo.Parse(1))
    .ToListAsync();

foreach (var category in categories)
{
    Console.WriteLine(JsonSerializer.Serialize(category, new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    }));
}