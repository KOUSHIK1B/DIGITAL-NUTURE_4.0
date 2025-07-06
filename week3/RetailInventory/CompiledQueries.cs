using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RetailInventory.Models;

public static class CompiledQueries
{
    
    public static readonly Func<AppDbContext, string, IEnumerable<Product>> ProductsByCategoryName =
        EF.CompileQuery((AppDbContext context, string categoryName) =>
            context.Products
                   .Where(p => p.Category.Name == categoryName));
}
