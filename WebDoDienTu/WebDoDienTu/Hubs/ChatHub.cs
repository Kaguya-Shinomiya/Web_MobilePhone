using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatHub(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SendMessageToAdmin(string message)
        {
            var userName = Context.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var chatMessage = new ChatMessage
            {
                SenderId = user.Id,
                ReceiverId = "Admin",
                Message = message,
                Timestamp = DateTime.Now
            };
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.Group("Admins").SendAsync("ReceiveMessageFromUser", userName, message);
            await NotifyNewMessage("Admin");
        }

        public async Task SendMessageToUser(string userName, string message)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var chatMessage = new ChatMessage
            {
                SenderId = "Admin",
                ReceiverId = user.Id,
                Message = message,
                Timestamp = DateTime.Now
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.User(user.Id).SendAsync("ReceiveMessageFromAdmin", "Admin", message);
            await NotifyNewMessage(user.Id);
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.User.IsInRole(SD.Role_Admin))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
            }
            await base.OnConnectedAsync();
        }

        public async Task<List<ApplicationUser>> GetChatUsers()
        {
            var chatUserIds = await _context.ChatMessages
                .Where(msg => msg.ReceiverId != "Admin" || msg.SenderId != "Admin")
                .Select(msg => msg.SenderId == "Admin" ? msg.ReceiverId : msg.SenderId)
                .Distinct()
                .ToListAsync();

            var chatUsers = await _userManager.Users
            .Where(user => chatUserIds.Contains(user.Id))
            .ToListAsync();

            return chatUsers;
        }

        public async Task<List<ChatMessage>> GetChatHistory(string userName)
        {
            ApplicationUser user;
            if (String.IsNullOrEmpty(userName))
            {
                userName = Context.User.Identity.Name;
                user = await _userManager.FindByNameAsync(userName);
            }
            else
            {
                user = await _userManager.FindByNameAsync(userName);
            }

            if (user == null)
            {
                return new List<ChatMessage>();
            }

            var messages = await _context.ChatMessages
                .Where(msg => (msg.ReceiverId == user.Id && msg.SenderId == "Admin") ||
                              (msg.SenderId == user.Id && msg.ReceiverId == "Admin"))
                .OrderBy(msg => msg.Timestamp)
                .ToListAsync();

            return messages;
        }

        public async Task NotifyNewMessage(string receiverId)
        {
            await Clients.User(receiverId).SendAsync("NewMessageNotification");
        }
    }
}
