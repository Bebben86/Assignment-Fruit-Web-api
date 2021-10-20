using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndreasFruit_api.Data;

namespace AndreasFruit_api.Interfaces
{
    public interface IUnitOfWork
    {
        IFruitRepository FruitRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        FruitContext Context { get; }
        Task<bool> Complete();
        bool HasChanged();
    }
}