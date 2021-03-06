using System.Collections.Generic;
using System.Threading.Tasks;
using AndreasFruit_api.Models;

namespace AndreasFruit_api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> AddCategoryAsync(Category category);
        Task<IList<Category>> ListCategoriesAsync();
        Task<Category> GetCategoryAsync(string category);
        bool UpdateCategory (Category category);
        bool RemoveCategory (Category category);

    }
}