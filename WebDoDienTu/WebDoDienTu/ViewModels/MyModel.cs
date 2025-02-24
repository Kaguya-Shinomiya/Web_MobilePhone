using WebDoDienTu.Models;

namespace WebDoDienTu.ViewModels
{
    public class MyModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
