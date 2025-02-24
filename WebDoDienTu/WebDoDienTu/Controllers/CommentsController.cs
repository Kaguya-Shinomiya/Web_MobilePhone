using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDoDienTu.Models;
using Microsoft.AspNetCore.Authorization;
using WebDoDienTu.Repository;

namespace WebDoDienTu.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentsController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Comment(int postId, string content)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var comment = new Comment
            {
                Content = content,
                CreatedAt = DateTime.UtcNow,
                AuthorId = currentUserId,
                PostId = postId
            };

            await _commentRepository.AddCommentAsync(comment);

            return RedirectToAction("Details", "Posts", new { id = postId });
        }

        [HttpGet]
        public async Task<IActionResult> LoadMoreComments(int postId, int offset, int count = 5)
        {
            var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
            var moreComments = comments.Skip(offset).Take(count).ToList();

            var result = moreComments.Select(c => new
            {
                authorName = c.Author.LastName,
                content = c.Content,
                createdAt = c.CreatedAt.ToString("dd/MM/yyyy HH:mm")
            });

            if (!result.Any())
            {
                return Json(new { message = "No more comments available" });
            }

            return Json(result);
        }
    }
}
