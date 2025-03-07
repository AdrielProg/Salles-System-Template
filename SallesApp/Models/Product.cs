namespace SallesApp.Models  
{
    public class Product
    {
        public int Id { get; set; }
        public short ProductCategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public bool IsAvailable { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
