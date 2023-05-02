namespace FashionCatalogue.Models
{
    public class Website
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string[] PageNumber { get; set; }
        public IDictionary<string, string> Catagories { get; set; }
    }
}
