using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdrakBusiness.Dto
{
    public class OrderItemRequestDto
    {
        [Required]
        [RegularExpression(@"^\d*[1-9]\d*$", ErrorMessage = "Quantity must be greater than zero.")]
        public int? Quantity { get; set; }
        [Required]
        [RegularExpression(@"^\d*[1-9]\d*$", ErrorMessage = "ProductID must be greater than zero.")]
        public int? ProductID { get; set; }
    }
}
