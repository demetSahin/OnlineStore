using OnlineStore.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.AbstractServices
{
    public interface ICategoryService
    {
        Task< bool> AddCategory(CategoryAddDto categoryAddDto);

        Task<List<CategoryListDto>> GetCategories();

        Task<CategoryUpdateDto> GetCategory(int id);

        Task<bool> UpdateCategory(CategoryUpdateDto categoryUpdateDto);

        Task<bool> DeleteCategory(int id);
        List<CategoryListDto> GetAllCategories();
    }
}
