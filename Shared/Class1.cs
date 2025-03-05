namespace Shared;

using System.ComponentModel.DataAnnotations;

// COPILOT SUGGESTED THESE ANNOTATIONS FOR VALIDATION OF THE JSON
public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Range(0.01, double.MaxValue)]
    public double Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    public required Category Category { get; set; }
}

public class Category
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
}