using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasFruit_api.Models
{
    public class Fruit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PluNumber { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}