using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndreasFruit_api.Data;
using AndreasFruit_api.Interfaces;
using AndreasFruit_api.Models;
using Microsoft.EntityFrameworkCore;

namespace AndreasFruit_api.Repositories
{
    public class FruitRepository : IFruitRepository
    {
        private readonly FruitContext _context;
        public FruitRepository(FruitContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewFruitAsync(Fruit fruit)
        {
            try
            {
                await _context.Fruits.AddAsync(fruit);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Fruit> FindFruitAsync(int id)
        {
            return await _context.Fruits
            .Include(c => c.Category)
            .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IList<Fruit>> FindFruitByCategoryAsync(string category)
        {
            return await _context.Fruits
            .Include(c => c.Category)
            .Where(c => c.Category.CategoryName.Trim().ToLower() == category.Trim().ToLower()).ToListAsync();
        }

        public async Task<Fruit> FindFruitByPluNumberAsync(string plu)
        {
            return await _context.Fruits
            .Include(c => c.Category)
            .SingleOrDefaultAsync(c => c.PluNumber.Trim().ToLower() == plu.Trim().ToLower());
        }

        public async Task<IList<Fruit>> ListAllFruitsAsync()
        {
            return await _context.Fruits
            .Include(c => c.Category).ToListAsync();
        }

        public bool RemoveFruit(Fruit fruit)
        {
            try
            {
                _context.Fruits.Remove(fruit);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateFruit(Fruit fruit)
        {
            try
            {
                _context.Fruits.Update(fruit);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}