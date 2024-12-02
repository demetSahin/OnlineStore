using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.ConcreteServices;
using OnlineStore.Business.Extensions;
using OnlineStore.WebUI.Areas.Admin.Models;


namespace OnlineStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] //program cs'teki area:exists ile eşleşir.
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService,IProductService productService)
        {
            _productService = productService;
            _commentService = commentService;
        }

        public IActionResult List()
        {
            var orderDtoList = _commentService.GetComments();
            var viewModel = orderDtoList.Select(x => new CommentListViewModel()

            {
                Id = x.Id,
                UserId = x.UserId,
                ProductId = x.ProductId,
                Content = x.Content,
                Rating = x.Rating,
                CreatedDate = x.CreatedDate

            }).ToList();

            return View(viewModel);

        }

        [HttpPost]
         public async Task<IActionResult> DeleteComment(int id)
        {
           
            await _commentService.DeleteComment(id);
           
            return RedirectToAction("List");


        }
    }
}
