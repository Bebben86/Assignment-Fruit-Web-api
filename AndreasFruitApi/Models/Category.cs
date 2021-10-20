using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasFruit_api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Fruit> Fruits { get; set; }
    }
}