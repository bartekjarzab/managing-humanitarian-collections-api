using System;

namespace managing_humanitarian_collections_api.Entities
{
    public class Collection
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CollectionPointId { get; set; }
        public int OrganizerId { get; set; }
        public int CommentId { get; set; }

        public virtual CollectionPoint CollectionPoint { get; set; }
        public virtual Organizer Organizer { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
