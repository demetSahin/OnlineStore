using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.Dtos;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.ConcreteServices
{
  
    public class CategoryService : ICategoryService
    {

        private readonly IRepository<CategoryEntity> _categoryRepository;
        private readonly IRepository<ProductEntity> _productRepository;
        public CategoryService(IRepository<CategoryEntity> categoryRepository, IRepository<ProductEntity> productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;

        }

        public async Task<bool> AddCategory(CategoryAddDto categoryAddDto)
        {
            var hasCategory =  _categoryRepository.GetAll(x => x.Name.ToLower() == categoryAddDto.Name.ToLower());
            hasCategory.ToList();
            if (hasCategory.Any()) // hasCategory.Count != 0
            {
                return false;
                // Bu isimde kategori zaten mevcutsa, ekleme yapmayacağımdan geriye false dönüyorum.
            }


            var entity = new CategoryEntity()
            {
                Name = categoryAddDto.Name,
                Description = categoryAddDto.Description
            };

            await _categoryRepository.AddAsync(entity);
            return true;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var firstProduct = await _productRepository.GetAsync(x => x.CategoryId == id);

            if (firstProduct is not null)
            {
                return false;
                //silme işlemi yapılamaz, içerisinde en az 1 ürün var.
            }

            await _categoryRepository.DeleteAsync(id);
            return true;
        }

        public List<CategoryListDto> GetAllCategories()
        {
            var categoryEntities = _categoryRepository.GetAll().OrderBy(x => x.Name);

            var categoryDtoList = categoryEntities.Select(x => new CategoryListDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
           

            return categoryDtoList;

        }

        public async Task<List<CategoryListDto>> GetCategories()
        {
            var categoryEntities = _categoryRepository.GetAll();
            categoryEntities.OrderBy(x => x.Name);

            var categoryDtoList = categoryEntities.Select(x => new CategoryListDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
            // Her bir entity için 1 adet CategoryListDto newliyor. veri aktarımı gerçekleştirip listeye çeviriyor.

            return categoryDtoList;
        }

        public async Task<CategoryUpdateDto> GetCategory(int id)
        {
            var entity = await _categoryRepository.GetByIdAsync(id);

            var categoryUpdateDto = new CategoryUpdateDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };

            return categoryUpdateDto;
        }

        public async Task<bool> UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            var hasCategory =  _categoryRepository.GetAll(x => x.Name.ToLower() == categoryUpdateDto.Name.ToLower() && x.Id != categoryUpdateDto.Id);
            hasCategory.ToList();
            // Kendisi hariç aynı isimde başka veriyle eşleşiyorsa listeye çekiyorum. Bunu yapmamdaki neden ismi aynı tutup diğer özellikleri değiştirmek istediğimizde kendi verisini çekip zaten mevcut dememesi için.


            if (hasCategory.Any()) // hasCategory.Count != 0
            {
                return false;
                // Aynı isimde kategori olduğundan o isme güncelleme yapmıyorum.
            }

            var entity = await _categoryRepository.GetByIdAsync(categoryUpdateDto.Id);

            entity.Name = categoryUpdateDto.Name;
            entity.Description = categoryUpdateDto.Description;

            await _categoryRepository.UpdateAsync(entity);
            return true;

        }
    }
}
