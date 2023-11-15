using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdrakBusiness.Dto
{
    public class OrderRequestDto
    {
        [Required]
        [RegularExpression(@"^\d*[1-9]\d*$", ErrorMessage = "CustomerId must be greater than zero.")]
        public int? CustomerId { get; set; }
        [Required]
        public DateTime? OrderDate { get; set; }
        [Required]
        public string? Status { get; set; }
        public List<OrderItemRequestDto> OrderItems { get; set; }
    }
}
