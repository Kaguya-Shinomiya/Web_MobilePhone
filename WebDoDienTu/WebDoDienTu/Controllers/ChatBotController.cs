using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebDoDienTu.Models;
using WebDoDienTu.Service.ChatBot;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    public class ChatBotController : Controller
    {
        private readonly DialogflowService _dialogflowService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatBotController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            var jsonCredentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "E:\\DO AN CO SO\\moblie-shop-440711-780c1259a847.json");
            _dialogflowService = new DialogflowService("moblie-shop-440711", jsonCredentialsPath);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] string userMessage)
        {
            var user = await _userManager.GetUserAsync(User);
            var sessionId = user.Id; 
            var response = await _dialogflowService.DetectIntentAsync(sessionId, userMessage);
            return Json(new { response });
        }
    }
}
