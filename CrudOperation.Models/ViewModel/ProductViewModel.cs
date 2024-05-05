using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperation.Models.ViewModel
{
    public class ProductViewModel
    {
        public string? Product { get; set; }
        public int? Quantity { get; set; }
        public int? Rate { get; set; }
        public int? Amount { get; set; }
    }
}
