namespace managing_humanitarian_collections_api.Models.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CreatedById { get; set; }
        public string CreatedCommentDate { get; set; }
    }
}
