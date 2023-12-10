using ApiProject.Data;
using ApiProject.Model;
using ApiProject.Service;

namespace ApiProject.Container
{
    public class CategoryList : ICategories
    {
        private readonly ApiContext _context;
        public CategoryList(ApiContext context) 
        {
            _context = context;
        }
        public List<Category> GetAllCategory()
        {
        return _context.Categories.ToList();
        }
    }
}
