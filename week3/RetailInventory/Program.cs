using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions; // ← make sure this is NOT underlined
using RetailInventory.Models;

class Program
{
    static async Task Main(string[] args)
    {
        using var context = new AppDbContext();

        var products = await context.Products.ToListAsync();
        products.ForEach(p => p.StockQuantity += 5);

        await context.BulkUpdateAsync(products); // ← this should now work

        Console.WriteLine("✅ Bulk updated");
    }
}
