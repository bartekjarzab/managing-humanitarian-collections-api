namespace managing_humanitarian_collections_api.Models.Collection
{
    public class EditCollectionDto
    {
        public string RegistrationNumber { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CollectionStatusId { get; set; }
    }
}
