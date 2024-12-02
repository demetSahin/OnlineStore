using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Enums;

namespace OnlineStore.WebUI.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserTypeEnum UserType { get; set; }
        public List<ProductLikeViewModel> ProductLikeViewModels { get; set; }
        public List<CommentViewModel> CommentViewModels { get; set; }


    }
}
