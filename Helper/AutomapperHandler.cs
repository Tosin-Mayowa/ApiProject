using ApiProject.Model;
using ApiProject.Model.Dto;
using AutoMapper;

namespace ApiProject.Helper
{
    public class AutomapperHandler:Profile
    {
        public AutomapperHandler()
        {
            CreateMap<Category, CatGetAllDto>();
            CreateMap<Category, CategoryDto>().ReverseMap();
            
        }
    }
}
