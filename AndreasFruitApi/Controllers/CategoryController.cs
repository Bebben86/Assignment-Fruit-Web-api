using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AndreasFruit_api.ViewModels.Category;
using AndreasFruit_api.Models;
using AndreasFruit_api.Interfaces;
using AutoMapper;

namespace AndreasFruit_api.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost()]
        public async Task<IActionResult> AddCategory(PostViewModel category)
        {
            var fruitCategory = _mapper.Map<Category>(category);

            if (await _unitOfWork.CategoryRepository.AddCategoryAsync(fruitCategory))
                if (await _unitOfWork.Complete())
                    return StatusCode(201, _mapper.Map<ViewModel>(fruitCategory));

            return StatusCode(500, "Something went wrong");
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await _unitOfWork.CategoryRepository.ListCategoriesAsync();

            if (result == null) return NotFound("Couldn't find any categories in the database.");

            return Ok(_mapper.Map<IList<ViewModel>>(result));
        }

        [HttpGet("{categoryName}")]
        public async Task<IActionResult> Get(string categoryName)
        {
            var result = await _unitOfWork.CategoryRepository.GetCategoryAsync(categoryName);
            if (result == null) return NotFound($"Couldn't find category {categoryName}");

            return Ok(_mapper.Map<ViewModel>(result));
        }

        [HttpPut("{categoryName}")]
        public async Task<IActionResult> UpdateCategory(string categoryName, [FromBody] PutViewModel category)
        {
            var toUpdate = await _unitOfWork.CategoryRepository.GetCategoryAsync(categoryName);
            if (toUpdate == null) return NotFound($"Couldn't find category with name {categoryName}");

            toUpdate.CategoryName = category.CategoryName;

            if (_unitOfWork.CategoryRepository.UpdateCategory(toUpdate))
                if (await _unitOfWork.Complete()) return NoContent();

            return StatusCode(500, "Something went wrong.");
        }

        [HttpDelete("{categoryName}")]
        public async Task<IActionResult> RemoveCategory(string categoryName){
            var toDelete = await _unitOfWork.CategoryRepository.GetCategoryAsync(categoryName);
            if (toDelete == null) return NotFound($"Couldn't find fruit with name {categoryName}");

            if (_unitOfWork.CategoryRepository.RemoveCategory(toDelete))
            if(await _unitOfWork.Complete()) return Ok("Category deleted successfully!");

            return StatusCode(500, "Couldn't delete category.");
        }
    }
}