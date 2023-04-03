namespace managing_humanitarian_collections_api.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int CollectionId { get; set; }
        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

    }
}
