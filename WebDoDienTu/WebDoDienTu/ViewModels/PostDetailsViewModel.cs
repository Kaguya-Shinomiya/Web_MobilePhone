using WebDoDienTu.Models;

namespace WebDoDienTu.ViewModels
{
    public class PostDetailsViewModel
    {
        public Post Post { get; set; }
        public string PostContentAsHtml { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public Comment NewComment { get; set; } = new Comment();
        public bool UserLiked { get; set; }
        public bool UserDisliked { get; set; }
    }
}
