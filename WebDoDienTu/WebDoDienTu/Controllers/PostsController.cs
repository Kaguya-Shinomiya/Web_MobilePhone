using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using WebDoDienTu.Hubs;
using WebDoDienTu.Repository;
using WebDoDienTu.ViewModels;

namespace WebDoDienTu.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IHubContext<PostHub> _hubContext;

        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository, IHubContext<PostHub> hubContext)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null) return NotFound();

            var comments = await _commentRepository.GetCommentsByPostIdAsync(id);
            var postContentAsHtml = Markdown.ToHtml(post.Content);

            // Lấy userId của người dùng đang đăng nhập
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userAction = post.ActionPosts.FirstOrDefault(ap => ap.UserId == userId);

            var viewModel = new PostDetailsViewModel
            {
                Post = post,
                PostContentAsHtml = postContentAsHtml,
                Comments = comments,
                UserLiked = userAction?.Like ?? false,
                UserDisliked = userAction?.Dislike ?? false
            };

            return View(viewModel);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LikePost([FromBody] int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postRepository.LikePostAsync(post, userId);

            int totalLikes = post.ActionPosts.Count(ap => ap.Like); 
            int totalDislikes = post.ActionPosts.Count(ap => ap.Dislike);

            await _hubContext.Clients.All.SendAsync("UpdateLikes", postId, totalLikes, totalDislikes);

            return Json(new { likes = totalLikes, dislikes = totalDislikes });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DislikePost([FromBody] int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postRepository.DisLikePostAsync(post, userId);

            int totalLikes = post.ActionPosts.Count(ap => ap.Like);
            int totalDislikes = post.ActionPosts.Count(ap => ap.Dislike);

            await _hubContext.Clients.All.SendAsync("UpdateLikes", postId, totalLikes, totalDislikes);

            return Json(new { likes = totalLikes, dislikes = totalDislikes });
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveLike([FromBody] int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postRepository.RemoveLikeAsync(post, userId);

            int totalLikes = post.ActionPosts.Count(ap => ap.Like);
            int totalDislikes = post.ActionPosts.Count(ap => ap.Dislike);

            await _hubContext.Clients.All.SendAsync("UpdateLikes", postId, totalLikes, totalDislikes);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveDislike([FromBody] int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postRepository.RemoveDislikeAsync(post, userId);

            int totalLikes = post.ActionPosts.Count(ap => ap.Like);
            int totalDislikes = post.ActionPosts.Count(ap => ap.Dislike);

            await _hubContext.Clients.All.SendAsync("UpdateLikes", postId, totalLikes, totalDislikes);

            return Ok();
        }

    }
}
