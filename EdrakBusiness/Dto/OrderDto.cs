
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace EdrakBusiness.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        [Required]
        public string? Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
