using System;
using System.Collections.Generic;
using System.Linq;
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

            return StatusCode(500, "Något gick fel.");
        }

        [HttpGet()]
        public async Task<IActionResult> Get(){
            var result = await _unitOfWork.CategoryRepository.ListCategoriesAsync();

            if(result == null) return NotFound("Kunde inte hitta några kategorier :(");
            
            return Ok(_mapper.Map<ViewModel>(result));
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> Get (string category){
            var result = await _unitOfWork.CategoryRepository.GetCategoryAsync(category);
            if(result == null) return NotFound($"Kunde inte hitta kategori med namn {category}");

            return Ok(_mapper.Map<ViewModel>(result));
        }
    }
}