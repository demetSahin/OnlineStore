namespace OnlineStore.WebUI.Models
{
    public class UserLikedProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string ImagePath { get; set; }
    }
}
