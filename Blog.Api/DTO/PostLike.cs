namespace Blog.Api.DTO
{
    public class PostLike
    {
        public string Type { get; set; }
        public int PostId { get; set; } 
    }

    public class CommentLike
    {
        public string Type { get; set; }
        public int CommentId { get; set; }
    }
}
