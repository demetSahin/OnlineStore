namespace OnlineStore.WebUI.Models
{
    public class UserDetailViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<UserLikedProductViewModel> LikedProducts { get; set; }
    }
}
