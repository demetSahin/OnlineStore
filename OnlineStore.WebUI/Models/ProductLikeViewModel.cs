namespace OnlineStore.WebUI.Models
{
    public class ProductLikeViewModel
    {
        public UserViewModel UserViewModel { get; set; }
        public int UserId { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
        public int ProductId { get; set; }
    }
}
