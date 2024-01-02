using ApiProject.Model.Dto;
using ApiProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ApiProject.Controllers
{
    [EnableRateLimiting("fixedwindow")]
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
        public async Task<IActionResult> Get() 
        {

           
        var cat=await _service.GetAllCategory();
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(int id)
        {

            if (id == 0)
            {
              return BadRequest();
            }
            var cat =await _service.GetCategory(id);
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(CategoryDto data)
        {


            var cat = await _service.Create(data);
            
            return Ok(cat);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {


            var cat = await _service.Delete(id);

            return Ok(cat);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(CategoryDto data,int id)
        {


            var cat = await _service.Update(data,id);

            return Ok(cat);
        }
    }
}
