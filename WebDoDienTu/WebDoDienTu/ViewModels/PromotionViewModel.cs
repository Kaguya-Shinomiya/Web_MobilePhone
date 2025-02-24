using WebDoDienTu.Models;

namespace WebDoDienTu.ViewModels
{
    public class PromotionViewModel
    {
        public Promotion Promotion { get; set; }
        public IEnumerable<Promotion> Promotions { get; set; }
    }
}
