using ApiProject.Data;
using ApiProject.Helper;
using ApiProject.Model;
using ApiProject.Model.Dto;
using ApiProject.Service;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Container
{
    public class CategoryList : ICategories
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryList> _logger;
        public CategoryList(ApiContext context, IMapper mapper, ILogger<CategoryList> logger) 
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<CatGetAllDto>> GetAllCategory()
        {
            List<CatGetAllDto> _resource= new List<CatGetAllDto>();
        var data=await _context.Categories.ToListAsync();
            _resource = _mapper.Map<List<CatGetAllDto>>(data);
            return _resource;
        }
        public async Task<CategoryDto> GetCategory(int id)
        {   CategoryDto _resource= new CategoryDto();
            var data = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (data != null)
            {
                _resource=_mapper.Map<CategoryDto>(data);
            }
            return _resource;
        }

        public async Task<APIResponse> Create(CategoryDto categoryDto)
        {
            APIResponse response = new APIResponse();
            try
            {
                _logger.LogInformation("Create Begins");
                Category cat = _mapper.Map<Category>(categoryDto);
                 _context.Categories.AddAsync(cat);
                await _context.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result=categoryDto.Name;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ResponseCode = 400;
                response.Result=ex.Message;
                
            }
            return response;
        }

        public async Task<APIResponse> Delete(int id)
        {
            APIResponse response = new APIResponse();
            try
            {

                var category =await _context.Categories.FindAsync(id);
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                response.ResponseCode = 200;
                response.Result = $"{category.Name} has been successfully deleted";

            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.Result = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> Update(CategoryDto data,int id)
        {
            APIResponse response = new APIResponse();
            try
            {

                var category = await _context.Categories.FindAsync(id);
               if(category != null)
                {
                   category.Name = data.Name;
                    category.DisplayOrder = data.DisplayOrder;
                    category.Quantity = data.Quantity;
                    await _context.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = $"{category.Id} has been successfully updated";
                }
               

            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.Result = ex.Message;
            }
            return response;
        }
    }
}
