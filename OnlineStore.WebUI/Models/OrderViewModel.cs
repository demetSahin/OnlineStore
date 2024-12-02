using OnlineStore.DAL.Entities;

namespace OnlineStore.WebUI.Models
{
    public class OrderViewModel
    {
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string Fullname { get; set; }
        public string AddressName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public List<OrderDetailViewModel> OrderDetailsViewModels { get; set; }
    }
}
