using ApiProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategories _service;
        public CategoryController(ICategories service) 
        {
           _service = service;
        }

        [HttpGet]
        public IActionResult Get() 
        {
        var cat=_service.GetAllCategory();
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }
    }
}
