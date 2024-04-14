namespace WebApiLab.Dal.Entities;

public class Category
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public ICollection<Product> Products { get; } = new List<Product>();
}
