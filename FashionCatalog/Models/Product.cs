namespace FashionCatalog.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Store { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public float Price { get; set; }
        public string SiteURL { get; set; }
        public string ImageURL { get; set; }
        public string Sex { get; set; }
    }
}
