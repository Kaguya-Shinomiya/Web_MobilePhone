namespace WebDoDienTu.Models
{
    public class ActionPost
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public bool Like { get; set; } = false;
        public bool Dislike { get; set; } = false;
        public virtual Post? Post { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
