using EdrakBusiness.Dto;
using EdrakBusiness.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdrakBusiness.IService
{
    public interface IOrderService
    {
        Task<Response<OrderDto>> CreateOrder(OrderRequestDto orderDto);
        Task<Response<List<OrderDto>>> GetByCustomerId(int customerId);
        Response<OrderDto> GetByOrderId(int orderId);
        Task<Response<bool>> UpdateOrderStatus(int orderId, string status);
        Task<Response<bool>> CancelOrder(int orderId);
        Task<Response<List<OrderDto>>> GetOrders(int limit);
    }
}
