using ApiProject.Helper;
using ApiProject.Model;
using ApiProject.Model.Dto;

namespace ApiProject.Service
{
    public interface ICategories
    {
        Task<List<CatGetAllDto>> GetAllCategory();
        Task<CategoryDto> GetCategory(int id);
        Task<APIResponse> Create(CategoryDto categoryDto);
        Task<APIResponse> Delete(int id);
        Task<APIResponse> Update(CategoryDto categoryDto, int id);

    }
}
