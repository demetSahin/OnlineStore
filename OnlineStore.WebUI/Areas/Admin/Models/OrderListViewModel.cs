using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.Areas.Admin.Models
{
    public class OrderListViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string Fullname { get; set; }
        public string AddressName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public List<OrderDetailViewModel> OrderDetailViewModels { get; set; }
    }
}
