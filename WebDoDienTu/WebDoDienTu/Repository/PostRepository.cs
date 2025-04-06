using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.Include(p => p.Category).Include(ac => ac.ActionPosts).ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Posts.Include(p => p.Category).Include(ac => ac.ActionPosts).FirstOrDefaultAsync(p => p.PostId == id);
        }

        public async Task AddPostAsync(Post post)
        {
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            post.UpdatedAt = DateTime.Now;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task LikePostAsync(Post post, string userId)
        {
            var existingAction = post.ActionPosts.FirstOrDefault(ap => ap.UserId == userId);

            if (existingAction != null)
            {
                existingAction.Like = true;
                existingAction.Dislike = false;
            }
            else
            {
                post.ActionPosts.Add(new ActionPost
                {
                    UserId = userId,
                    Like = true,
                    Dislike = false
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task DisLikePostAsync(Post post, string userId)
        {
            var existingAction = post.ActionPosts.FirstOrDefault(ap => ap.UserId == userId);

            if (existingAction != null)
            {
                existingAction.Dislike = true;
                existingAction.Like = false;
            }
            else
            {
                post.ActionPosts.Add(new ActionPost
                {
                    UserId = userId,
                    Dislike = true,
                    Like = false
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveLikeAsync(Post post, string userId)
        {
            var existingAction = post.ActionPosts.FirstOrDefault(ap => ap.UserId == userId);

            if (existingAction != null && existingAction.Like)
            {
                existingAction.Like = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveDislikeAsync(Post post, string userId)
        {
            var existingAction = post.ActionPosts.FirstOrDefault(ap => ap.UserId == userId);

            if (existingAction != null && existingAction.Dislike)
            {
                existingAction.Dislike = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
