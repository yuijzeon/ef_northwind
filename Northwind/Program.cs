using System.Text.Json;
using System.Text.Json.Serialization;
using Northwind.Infrastructure;
using Microsoft.EntityFrameworkCore;

var northwindContext = new NorthwindContextDev();

var categories = await northwindContext.Customers.ToListAsync();

foreach (var category in categories)
{
    Console.WriteLine(JsonSerializer.Serialize(category, new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    }));
}