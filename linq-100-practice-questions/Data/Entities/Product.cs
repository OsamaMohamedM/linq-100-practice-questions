namespace LinqQuestion;

public class Product : IEquatable<Product>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }

    public bool Equals(Product? other)
    {
        return Id == other?.Id;
    }

    public override string ToString()
        => $"Id:{Id}, Name:{Name}, Category:{Category}, UnitPrice:{UnitPrice}, UnitsInStock:{UnitsInStock}";
}