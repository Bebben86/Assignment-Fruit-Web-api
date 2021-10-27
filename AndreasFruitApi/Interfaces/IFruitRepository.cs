using System.Collections.Generic;
using System.Threading.Tasks;
using AndreasFruit_api.Models;

namespace AndreasFruit_api.Interfaces
{
    public interface IFruitRepository
    {
        Task<bool> AddNewFruitAsync(Fruit fruit);
        bool UpdateFruit(Fruit fruit);
        bool RemoveFruit(Fruit fruit);
        Task<IList<Fruit>> ListAllFruitsAsync();
        Task<Fruit> FindFruitAsync(int id);
        Task<Fruit> FindFruitByPluNumberAsync(string plu);
        Task<IList<Fruit>> FindFruitByCategoryAsync(string category);
    }
}