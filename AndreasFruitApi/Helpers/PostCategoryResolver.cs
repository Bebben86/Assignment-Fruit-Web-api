using System;
using System.Linq;
using AndreasFruit_api.Data;
using AndreasFruit_api.Models;
using AutoMapper;

namespace AndreasFruit_api.Helpers
{
    public class AddCategoryResolver : IValueResolver<ViewModels.Fruit.PostViewModel, Fruit, Category>
    {
        public Category Resolve(ViewModels.Fruit.PostViewModel source, Fruit destination, Category destMember, ResolutionContext context)
        {
            var repo = context.Items["repo"] as FruitContext;
            var result = repo.Categories.FirstOrDefault(c => c.CategoryName.ToLower().Trim() == source.CategoryName.ToLower().Trim());

            if (result == null) throw new Exception($"Could not find a category with the name {source.CategoryName}");
            return result;

        }
    }
}