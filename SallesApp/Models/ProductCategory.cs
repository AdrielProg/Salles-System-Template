namespace SallesApp.Models
{
    public class ProductCategory
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products{ get; set; }

    }
}
