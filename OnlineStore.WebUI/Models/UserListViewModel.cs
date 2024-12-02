using OnlineStore.Business.Dtos;

namespace OnlineStore.WebUI.Models
{
    public class UserListViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
