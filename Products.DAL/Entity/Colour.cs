using Products.DAL.Entity.Contract;

namespace Products.DAL.Entity;

public class Colour : BaseGuidEntity, ISoftDeletable
{
    public string Name { get; set; } = "";
    public string HexValue { get; set; } = "";

    public virtual ICollection<Product>? Products { get; set; }
    public bool IsDeleted { get; set; }
}
