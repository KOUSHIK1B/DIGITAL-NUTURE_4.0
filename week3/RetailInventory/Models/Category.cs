namespace RetailInventory.Models;

public class Category
{
    public int Id { get; set; }          // Primary Key
    public string Name { get; set; }     = string.Empty; // e.g., "Electronics"
    public List<Product> Products { get; set; } = new List<Product>();// Navigation property
}
