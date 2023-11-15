using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdrakBusiness.Dto
{
    public class OrderItemDto
    {
        public int OrderItemID { get; set; }
        //public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public int? ProductID { get; set; }
        public ProductDto Product { get; set; }
    }
}
