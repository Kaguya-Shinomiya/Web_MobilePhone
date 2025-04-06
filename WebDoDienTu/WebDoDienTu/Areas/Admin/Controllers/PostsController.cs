using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using WebDoDienTu.Models;
using WebDoDienTu.Repository;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostCategoryRepository _postCategoryRepository;
        private readonly ICloudinary _cloudinary;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsController(IPostRepository postRepository, IPostCategoryRepository postCategoryRepository, ICloudinary cloudinary, UserManager<ApplicationUser> userManager)
        {
            _postRepository = postRepository;
            _postCategoryRepository = postCategoryRepository;
            _cloudinary = cloudinary;
            _userManager = userManager;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return View(posts);
        }

        // GET: Posts/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _postCategoryRepository.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        public async Task<IActionResult> Create(Post post, IFormFile imageFile)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            post.AuthorId = currentUserId;
            post.CreatedAt = DateTime.UtcNow;
            post.UpdatedAt = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                post.Content = ConvertMarkdownToHtml(post.Content);
                if (imageFile != null)
                {
                    post.ImageUrl = await SaveImageToCloudinary(imageFile);
                }

                await _postRepository.AddPostAsync(post);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _postCategoryRepository.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View(post);
        }

        // GET: Posts/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var categories = await _postCategoryRepository.GetAllCategoriesAsync();
            var user = await _userManager.FindByIdAsync(post.AuthorId);
            ViewBag.Author = user.Email;
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View(post);
        }

        // POST: Posts/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Post post, IFormFile? imageFile)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            var existingPost = await _postRepository.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existingPost.Title = post.Title;
                existingPost.Content = ConvertMarkdownToHtml(post.Content);
                existingPost.Content = post.Content;
                existingPost.CategoryId = post.CategoryId;
                existingPost.IsPublished = post.IsPublished;
                existingPost.UpdatedAt = DateTime.UtcNow;

                if (imageFile != null)
                {
                    existingPost.ImageUrl = await SaveImageToCloudinary(imageFile);
                }

                await _postRepository.UpdatePostAsync(existingPost);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _postCategoryRepository.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View(post);
        }


        // GET: Posts/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _postRepository.DeletePostAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveImageToCloudinary(IFormFile imageFile)
        {
            try
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
                    PublicId = $"posts/{Guid.NewGuid()}"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.ToString();
                }
                else
                {
                    throw new Exception("Image upload failed.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload image to Cloudinary.", ex);
            }
        }

        private string ConvertMarkdownToHtml(string markdown)
        {
            if (string.IsNullOrEmpty(markdown))
            {
                return string.Empty;
            }

            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build();

            return Markdown.ToHtml(markdown, pipeline);
        }
    }
}
