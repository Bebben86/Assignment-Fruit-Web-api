using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndreasFruit_api.Interfaces;
using AndreasFruit_api.Models;
using AndreasFruit_api.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AndreasFruit_api.ViewModels.Fruit;

namespace AndreasFruit_api.Controllers
{
    [ApiController]
    [Route("api/fruit")]
    public class FruitController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FruitController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost()]
        public async Task<IActionResult> AddFruit(PostViewModel name)
        {
            try
            {
                var fruit = _mapper.Map<Fruit>(name, opt => opt.Items["repo"] = _unitOfWork.Context);
                if (await _unitOfWork.FruitRepository.AddNewFruitAsync(fruit))
                {
                    if (!await _unitOfWork.Complete())
                        return StatusCode(500, "Något gick fel när frukten skule sparas.");

                    var result = _mapper.Map<ViewModels.Fruit.ViewModel>(name);
                    return StatusCode(201, result);
                }
                return StatusCode(500, "Något gick fel när frukten skulle sparas.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFruit(int id)
        {
            var fruits = await _unitOfWork.FruitRepository.FindFruitAsync(id);
            return Ok(_mapper.Map<List<ViewModel>>(fruits));
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await _unitOfWork.FruitRepository.ListAllFruitsAsync();
            var fruits = _mapper.Map<List<ViewModel>>(result);

            return Ok(fruits);
        }

        [HttpGet("byplu/{plu}")]
        public async Task<IActionResult> FindByPlu(string plu)
        {

            var result = await _unitOfWork.FruitRepository.FindFruitByPluNumberAsync(plu);
            if (result == null) return NotFound($"Kunde inte hitta frukt med PLU {plu}");

            var response = _mapper.Map<ViewModel>(result);
            return Ok(result);
        }

        [HttpGet("byCategory/{category}")]
        public async Task<IActionResult> FindByCategory(string category)
        {
            var result = await _unitOfWork.FruitRepository.FindFruitByCategoryAsync(category);
            if (category == null) return NotFound($"Kunde inte hitta frukt med kategorin {category}");

            var response = _mapper.Map<List<ViewModel>>(result);
            return Ok(response);
        }
        // TODO VAD GÖRA MED CATEGORY???
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFruit(int id, [FromBody] PutViewModel fruit)
        {
            var toUpdate = await _unitOfWork.FruitRepository.FindFruitAsync(id);
            if (toUpdate == null) return NotFound($"Kunde inte hitta fruk med id {id}");

            toUpdate.PluNumber = fruit.PluNumber;
            toUpdate.Name = fruit.Name;

            if (_unitOfWork.FruitRepository.UpdateFruit(toUpdate))
                if (await _unitOfWork.Complete()) return NoContent();

            return StatusCode(500, "Något annat gick fel");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFruit(int id){
            var toDelete = await _unitOfWork.FruitRepository.FindFruitAsync(id);
            if (toDelete == null) return NotFound($"Vi kunde inte hitta frukt med id {id}");

            if (_unitOfWork.FruitRepository.RemoveFruit(toDelete))
            if(await _unitOfWork.Complete()) return NoContent();

            return StatusCode(500, "Kunde ej ta bort frukten");
        }

    }
}