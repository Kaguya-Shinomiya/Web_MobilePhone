using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;
using WebDoDienTu.ViewModels;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            ViewBag.Roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();
            return View(users);
        }

        public IActionResult Create()
        {          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Age = model.Age
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public IActionResult Details(string id)
        {
            var user = _context.Users.Find(id);
            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            // Kiểm tra xem người dùng có tồn tại không
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Kiểm tra xem người dùng có đơn đặt hàng nào không
            var hasOrders = await _context.Orders.AnyAsync(o => o.UserId == id);
            if (hasOrders)
            {
                // Nếu người dùng có đơn đặt hàng, trả về thông báo lỗi
                TempData["ErrorMessage"] = "Người dùng này có đơn đặt hàng và không thể xóa.";
                return RedirectToAction("Index");
            }

            // Nếu không có đơn đặt hàng, hiển thị view để xác nhận xóa
            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID is null or empty");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Age = user.Age,
                Address = user.Address,
                RoleList = _roleManager.Roles.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name,
                    Selected = userRoles.Contains(x.Name)
                }),
                ConcurrencyStamp = user.ConcurrencyStamp
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model, string selectedRole)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            // Kiểm tra concurrency stamp
            if (user.ConcurrencyStamp != model.ConcurrencyStamp)
            {
                // Lấy lại thông tin mới nhất của người dùng từ cơ sở dữ liệu
                user = await _userManager.FindByIdAsync(model.Id);

                // Ghi đè các thuộc tính của user
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.Age = model.Age;
                user.Address = model.Address;
                user.ConcurrencyStamp = Guid.NewGuid().ToString();
            }
            else
            {
                // Cập nhật các thuộc tính của user
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.Age = model.Age;
                user.Address = model.Address;
            }

            try
            {
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    // Cập nhật vai trò của người dùng
                    var resultRemoveRoles = await _userManager.RemoveFromRolesAsync(user, userRoles);
                    if (!resultRemoveRoles.Succeeded)
                    {
                        foreach (var error in resultRemoveRoles.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }

                    var resultAddRole = await _userManager.AddToRoleAsync(user, selectedRole);
                    if (!resultAddRole.Succeeded)
                    {
                        foreach (var error in resultAddRole.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }

                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The user was updated or deleted by another user.");
            }

            model.RoleList = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name,
                Selected = selectedRole == x.Name
            }).ToList();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> BlockUser(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.IsBlocked = true;
                _context.Users.Update(user);
                _context.SaveChanges();

                var userPrincipal = await _signInManager.UserManager.GetUserAsync(User);
                if (userPrincipal != null && userPrincipal.Id == userId)
                {
                    await _signInManager.SignOutAsync();
                }

                return RedirectToAction("Index", "Users");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UnBlockUser(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.IsBlocked = false; 
                _context.Users.Update(user); 
                _context.SaveChanges();

                return RedirectToAction("Index", "Users");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId, string role)
        {
            // Tìm user bằng UserManager
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Lấy danh sách các vai trò hiện tại của user
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Xóa các vai trò hiện tại
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Lỗi khi xóa các vai trò hiện tại.");
                return View();
            }

            // Thêm vai trò mới cho user
            var addResult = await _userManager.AddToRoleAsync(user, role);
            if (!addResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Lỗi khi thêm vai trò mới.");
                return View();
            }

            TempData["Message"] = "Cập nhật vai trò thành công!";
            return RedirectToAction("Index", "Users");
        }
    }
}
