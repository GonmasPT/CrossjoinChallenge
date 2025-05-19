namespace CrossjoinChallenge.Models;

public class Product(Product? dependentProduct, string productType)
{
    public Guid ProductId { get; } = Guid.NewGuid();
    public Product? DependentProduct { get; set; } = dependentProduct;
    public string ProductType { get; set; } = productType; // e.g., Electronics, Furniture
}