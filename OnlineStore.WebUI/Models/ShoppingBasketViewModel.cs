using OnlineStore.DAL.Entities;

namespace OnlineStore.WebUI.Models
{
    public class ShoppingBasketViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserViewModel UserViewModel { get; set; }
        public int ProductId { get; set; }
        public ProductViewModel ProductViewModel { get; set; }

    }
}
