using System.Text.Json;
using System.Text.Json.Serialization;
using AsyncForeach.Infrastructure;
using Microsoft.EntityFrameworkCore;

var northwindContext = new NorthwindContext();

var categories = await northwindContext.Customers.ToListAsync();

foreach (var category in categories)
{
    Console.WriteLine(JsonSerializer.Serialize(category, new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    }));
}