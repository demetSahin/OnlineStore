using System.ComponentModel.DataAnnotations;

namespace OnlineStore.WebUI.Models
{
    public class CommentViewModel
    {
        public UserViewModel UserViewModel { get; set; }
        public int UserId { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Yorum alanı boş bırakılamaz.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Lütfen bir puan seçin.")]
        [Range(1, 5, ErrorMessage = "Geçerli bir puan seçin.")]
        public int Rating { get; set; }
    }
}

