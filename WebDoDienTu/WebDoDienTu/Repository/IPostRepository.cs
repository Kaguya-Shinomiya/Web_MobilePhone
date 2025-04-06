using WebDoDienTu.Models;

namespace WebDoDienTu.Repository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
        Task LikePostAsync(Post post, string userId);
        Task DisLikePostAsync(Post post, string userId);
        Task RemoveLikeAsync(Post post, string userId);
        Task RemoveDislikeAsync(Post post, string userId);
    }
}
