using WebDoDienTu.Models;

namespace WebDoDienTu.Repository
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task AddCommentAsync(Comment comment);
    }
}
