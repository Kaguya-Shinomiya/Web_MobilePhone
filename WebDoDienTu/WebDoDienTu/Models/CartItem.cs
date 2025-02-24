namespace WebDoDienTu.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string NameProduct { get; set; }
        public string Image {  get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
