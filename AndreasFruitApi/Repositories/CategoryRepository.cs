using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndreasFruit_api.Interfaces;
using AndreasFruit_api.Models;
using AndreasFruit_api.Data;
using Microsoft.EntityFrameworkCore;

namespace AndreasFruit_api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FruitContext _context;
        public CategoryRepository(FruitContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Category> GetCategoryAsync(string category)
        {
            var result = await _context.Categories
            .FirstOrDefaultAsync(c => c.CategoryName.ToLower().Trim() == category.ToLower().Trim());
            return result;
        }

        public async Task<IList<Category>> ListCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public bool UpdateCategory(Category category)
        {
            try
            {
                _context.Categories.Update(category);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveCategory(Category category)
        {
            try
            {
                _context.Categories.Remove(category);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}