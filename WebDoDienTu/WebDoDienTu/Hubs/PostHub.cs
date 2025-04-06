using Microsoft.AspNetCore.SignalR;

namespace WebDoDienTu.Hubs
{
    public class PostHub : Hub
    {
        public async Task UpdateLikes(int postId, int likes, int dislikes)
        {
            await Clients.All.SendAsync("UpdateLikes", postId, likes, dislikes);
        }
    }
}
