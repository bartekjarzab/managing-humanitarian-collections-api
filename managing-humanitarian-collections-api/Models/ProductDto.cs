namespace managing_humanitarian_collections_api.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProductPropertiesId { get; set; }
        public string Category { get; set; }
    }
}
