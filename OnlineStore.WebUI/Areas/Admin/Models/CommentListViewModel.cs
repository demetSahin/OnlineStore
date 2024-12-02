namespace OnlineStore.WebUI.Areas.Admin.Models
{
    public class CommentListViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
