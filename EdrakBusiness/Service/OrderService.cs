using AutoMapper;
using Domain.Entities;
using EdrakBusiness.Dto;
using EdrakBusiness.Dto.Common;
using EdrakBusiness.Enums;
using EdrakBusiness.IService;
using EdrakBusiness.Service.Base;
using EdrakBusiness.Settings;
using Persistence.IUnitOfWork;
using System.Text;

namespace EdrakBusiness.Service
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(AppSettings appSettings, IMapper mapper, IUnitOfWork unitOfWork) : base(appSettings, mapper, unitOfWork)
        {

        }
        public async Task<Response<OrderDto>> CreateOrder(OrderRequestDto orderDto)
        {
            Response<OrderDto> response = new();
            var order = Mapper.Map<Order>(orderDto);
            if (ValidateOrderItemsQuantities(order.OrderItems.ToList(), out string message))
            {
                var total = default(decimal);
                foreach (var item in order.OrderItems)
                {
                    var product = UnitOfWork.ProductRepository.GetFirstOrDefault(x => x.ProductId == item.ProductID);
                    product.StockQuantity -= item.Quantity;
                    UnitOfWork.ProductRepository.Update(product);
                    total += item.Quantity * product.Price;
                }
                order.TotalAmount = total;
                UnitOfWork.OrderRepository.Add(order);
                if (await UnitOfWork.SaveAsync())
                {
                    response.Code = ResponseStatusEnum.Success;
                    response.Message = "Success";
                    response.Data = Mapper.Map<OrderDto>(order);
                }
            }
            else
                response.Message = message;
            return response;
        }

        public async Task<Response<List<OrderDto>>> GetByCustomerId(int customerId)
        {
            Response<List<OrderDto>> response = new();
            var orders = await UnitOfWork.OrderRepository.GetWhere(x => x.CustomerId == customerId, null, "Customer", "OrderItems", "OrderItems.Product");
            if (orders.Count > 0)
            {
                response.Code = ResponseStatusEnum.Success;
                response.Message = "Success";
                response.Data = Mapper.Map<List<OrderDto>>(orders);
            }
            return response;
        }

        public Response<OrderDto> GetByOrderId(int orderId)
        {
            Response<OrderDto> response = new();
            var order = UnitOfWork.OrderRepository.GetFirstOrDefault(x => x.OrderId == orderId, "Customer", "OrderItems", "OrderItems.Product");
            if (order != null)
            {
                response.Code = ResponseStatusEnum.Success;
                response.Message = "Success";
                response.Data = Mapper.Map<OrderDto>(order);
            }
            return response;
        }
        public async Task<Response<bool>> UpdateOrderStatus(int orderId, string status)
        {
            Response<bool> response = new();
            var order = UnitOfWork.OrderRepository.GetFirstOrDefault(x => x.OrderId == orderId, "Customer", "OrderItems", "OrderItems.Product");
            if (order != null)
            {
                order.Status = status;
                UnitOfWork.OrderRepository.Update(order);
                if (await UnitOfWork.SaveAsync())
                {
                    response.Code = ResponseStatusEnum.Success;
                    response.Message = "Updated Successfully";
                    response.Data = true;
                }
            }
            return response;
        }


        public async Task<Response<bool>> CancelOrder(int orderId)
        {
            Response<bool> response = new();
            var order = UnitOfWork.OrderRepository.GetFirstOrDefault(x => x.OrderId == orderId, "Customer", "OrderItems", "OrderItems.Product");
            if (order != null)
            {
                foreach (var item in order.OrderItems)
                {
                    var product = UnitOfWork.ProductRepository.GetFirstOrDefault(x => x.ProductId == item.ProductID);
                    product.StockQuantity += item.Quantity;
                    UnitOfWork.ProductRepository.Update(product);
                }
                order.Status = "Cancelled";
                UnitOfWork.OrderRepository.Update(order);
                if (await UnitOfWork.SaveAsync())
                {
                    response.Code = ResponseStatusEnum.Success;
                    response.Message = "Caancelled Successfully.";
                    response.Data = true;
                }
            }
            return response;
        }

        public async Task<Response<List<OrderDto>>> GetOrders(int limit)
        {
            Response<List<OrderDto>> response = new();
            var orders = await UnitOfWork.OrderRepository.GetPaged(null, null, 1, limit, "Customer", "OrderItems", "OrderItems.Product");
            if (orders != null && orders.Count > 0)
            {
                response.Code = ResponseStatusEnum.Success;
                response.Message = "Success";
                response.Data = Mapper.Map<List<OrderDto>>(orders);
            }
            return response;
        }

        #region private methods
        private bool ValidateOrderItemsQuantities(List<OrderItem> orderItems, out string message)
        {
            StringBuilder sb = new();
            var isValid = true;
            foreach (var item in orderItems)
            {
                var product = UnitOfWork.ProductRepository.GetFirstOrDefault(x => x.ProductId == item.ProductID);
                if (product.StockQuantity < item.Quantity)
                    sb.Append($"The available qty for the product id : {item.ProductID} is {product.StockQuantity};");
                isValid = isValid && product.StockQuantity >= item.Quantity;
            }
            message = sb.ToString();
            return isValid;
        }
        #endregion private methods
    }
}
