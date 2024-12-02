using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.Dtos;
using OnlineStore.WebUI.Areas.Admin.Models;

namespace OnlineStore.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] //program cs'teki area:exists ile eşleşir.
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> List()
        {

            var categoryDtoList = await  _categoryService.GetCategories();


            var viewModel = categoryDtoList.Select(x => new CategoryListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description?.Length > 20 ? x.Description?.Substring(0, 20) + "..." : x.Description
            }).ToList();

            return View(viewModel);
        }
        public async Task<IActionResult> New()
        {

            //ekleme ve güncelleme için aynı formu kullandığım için ayrımı id üzerinden olacak bu yüzden boş bir model ile açıyorum.

            return View("Form", new CategoryFormViewModel());
        }
        public async Task< IActionResult> Update(int id)
        {
            var categoryDto = await _categoryService.GetCategory(id);

            var viewModel = new CategoryFormViewModel()
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            return View("Form", viewModel);
        }

        [HttpPost]
        public async Task< IActionResult> Save(CategoryFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", formData);
            }

            if (formData.Id == 0) // EKLEME İŞLEMİ
            {
                var categoryAddDto = new CategoryAddDto()
                {
                    Name = formData.Name.Trim(),
                    Description = formData.Description?.Trim()

                };

                var result = await _categoryService.AddCategory(categoryAddDto);

                if (result)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.ErrorMessage = "Bu isimde bir kategori zaten mevcut.";
                    return View("Form", formData);

                }
            }
            else // GÜNCELLEME İŞLEMİ
            {

                var categoryUpdateDto = new CategoryUpdateDto()
                {
                    Id = formData.Id,
                    Name = formData.Name,
                    Description = formData.Description
                };

                var result = await _categoryService.UpdateCategory(categoryUpdateDto);

                if (!result)
                {
                    ViewBag.ErrorMessage = "Bu isimde bir kategori zaten mevcut olduğundan, güncelleme yapamazsınız.";
                    return View("Form", formData);
                }

                return RedirectToAction("List");

            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteCategory(id);

            if (!result)
            {
                TempData["CategoryErrorMessage"] = "İlgili kategoride ürünler bulunduğundan silme işlemi gerçekleştirilemez.";

            }
            return RedirectToAction("List");
        }
    }
}
