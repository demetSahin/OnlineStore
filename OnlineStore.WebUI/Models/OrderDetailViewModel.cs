using OnlineStore.DAL.Entities;

namespace OnlineStore.WebUI.Models
{
    public class OrderDetailViewModel
    {
        
        //public DateTime CreatedDate { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public decimal? Price { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
        public OrderViewModel OrderViewModel { get; set; }
    }
}
