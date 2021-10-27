using System.Collections.Generic;

namespace AndreasFruit_api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Fruit> Fruits { get; set; }
    }
}