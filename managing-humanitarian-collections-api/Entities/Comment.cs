namespace managing_humanitarian_collections_api.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Review { get; set; }
        public int UserId { get; set; }
        public int CollectionId { get; set; }

    }
}
