using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.Dtos;
using OnlineStore.WebUI.Areas.Admin.Models;

namespace OnlineStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;
        private readonly ICommentService _commentService;
        
      
        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment,ICommentService commentService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _commentService = commentService;
            _environment = environment;


        }
        
        public async Task<IActionResult> List()
        {
            var productDtoList =await _productService.GetProducts();

            var viewModel = productDtoList.Select(x => new ProductListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                CategoryName = x.CategoryName,
                ImagePath = x.ImagePath
            }).ToList();

            return View(viewModel);
        }
        public async Task<IActionResult> New()
        {

            ViewBag.Categories = await _categoryService.GetCategories();

            return View("Form", new ProductFormViewModel());

        }

        public async Task<IActionResult> Update(int id)
        {
            var updateProductDto = await _productService.GetProductById(id);

            var viewModel = new ProductFormViewModel()
            {
                Id = updateProductDto.Id,
                Name = updateProductDto.Name,
                Description = updateProductDto.Description,
                UnitPrice = updateProductDto.UnitPrice,
                UnitsInStock = updateProductDto.UnitsInStock,
                CategoryId = updateProductDto.CategoryId,
            };

            ViewBag.ImagePath = updateProductDto.ImagePath;

            ViewBag.Categories =await _categoryService.GetCategories();
            return View("Form", viewModel);

        }

        public async Task<IActionResult> Delete(int id)
        {
           
            await _productService.DeleteProductWithComments(id);

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Save(ProductFormViewModel formData)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetCategories();
                return View("Form", formData);
            }

            var newFileName = "";

            if (formData.File is not null) // dosya yüklenmek isteniyorsa
            {
                var allowedFileTypes = new string[] { "image/jpeg", "image/jpg", "image/png", "image/jfif", "image/avif" };

                var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".jfif", ".avif" };


                var fileContentType = formData.File.ContentType;
                // dosyanın içeriği

                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.File.FileName);
                // dosyanın uzantısız ismi

                var fileExtension = Path.GetExtension(formData.File.FileName);
                // dosyanın uzantısı

                if (!allowedFileTypes.Contains(fileContentType) ||
                    !allowedFileExtensions.Contains(fileExtension))
                {

                    ViewBag.ImageErrorMessage = "Yüklediğiniz dosya" + fileExtension + " uzantısında. Sisteme yalnızca .jpg .pjeg .png .jfif .avif formatında dosyalar yüklenebilir.";
                    ViewBag.Categories =await _categoryService.GetCategories();
                    return View("Form", formData);
                }

                newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;

                // Aynı isimde iki dosya yüklediğimizde hata almamak için her dosyayı birbiriyle asla eşleşmeyecek şekilde isimlendiriyorum. Guid : unique bir string verir.

                // Görseli yükleyeceğim adresi ayarlıyorum.

                var folderPath = Path.Combine("images", "products");
                // images/products

                var wwwrootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
                // .../wwwroot/images/products (hangi bilgisayardaysa ona göre yol oluşacak.)

                var filePath = Path.Combine(wwwrootFolderPath, newFileName);
                // .../wwwroot/images/products/urunGorsel-123123adwaw13daw.jpg

                Directory.CreateDirectory(wwwrootFolderPath); // wwwroot/images/products klasörü yoksa oluştur.

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formData.File.CopyTo(fileStream);
                    // asıl dosya kopyalamasının yapıldığı kısım.
                }
                // using içerisinde new'lenen fileStream nesnesi scope boyunca yaşar, scope bitiminde silinir.


            }

            if (formData.Id == 0) // EKLEME
            {

                var productAddDto = new ProductAddDto()
                {
                    Name = formData.Name,
                    Description = formData.Description,
                    UnitPrice = formData.UnitPrice,
                    UnitsInStock = formData.UnitsInStock,
                    CategoryId = formData.CategoryId,
                    ImagePath = newFileName
                };

                await _productService.AddProduct(productAddDto);
                return RedirectToAction("List");


            }
            else // GÜNCELLEME
            {
                var productUpdateDto = new ProductUpdateDto()
                {
                    Id = formData.Id,
                    Name = formData.Name,
                    Description = formData.Description,
                    UnitPrice = formData.UnitPrice,
                    UnitsInStock = formData.UnitsInStock,
                    CategoryId = formData.CategoryId,
                    ImagePath = newFileName
                };

                await _productService.UpdateProduct(productUpdateDto);
                return RedirectToAction("List");

            }


        }
    }
}
