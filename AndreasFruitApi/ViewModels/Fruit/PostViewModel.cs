using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasFruit_api.ViewModels
{
    public class PostViewModel
    {
        public string Name { get; set; }
        public string PluNumber { get; set; }
        public int CategoryId { get; set; }
    }
}