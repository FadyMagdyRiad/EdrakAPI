using EdrakBusiness.Dto;
using EdrakBusiness.Dto.Common;
using EdrakBusiness.IService;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EdrakAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }
        [HttpPost("CreateOrder")]
        public async Task<Response<OrderDto>> CreateOrder([FromBody] OrderRequestDto order)
        {
            Response<OrderDto> response = new Response<OrderDto>();
            if (!ModelState.IsValid)
            {
                response.Message = "Bad Request";
                return response;
            }
            try
            {
                response = await _orderService.CreateOrder(order);
            }
            catch (Exception ex)
            {
                response.Code = EdrakBusiness.Enums.ResponseStatusEnum.Exception;
                response.Message = "Error";
                _logger.LogDebug(ex, "Error");
            }
            return response;
        }
        [HttpGet("GetByCustomerId")]
        public async Task<Response<List<OrderDto>>> GetByCustomerId(int customerId)
        {
            Response<List<OrderDto>> response = new();
            if (customerId == 0)
            {
                response.Message = "Bad Request";
                return response;
            }
            try
            {
                response = await _orderService.GetByCustomerId(customerId);
            }
            catch (Exception ex)
            {
                response.Code = EdrakBusiness.Enums.ResponseStatusEnum.Exception;
                response.Message = "Error";
                _logger.LogDebug(ex, "Error");
            }
            return response;
        }

        [HttpGet("GetByOrderId")]
        public Response<OrderDto> GetByOrderId(int orderId)
        {
            Response<OrderDto> response = new();
            if (orderId == 0)
            {
                response.Message = "Bad Request";
                return response;
            }
            try
            {
                response = _orderService.GetByOrderId(orderId);
            }
            catch (Exception ex)
            {
                response.Code = EdrakBusiness.Enums.ResponseStatusEnum.Exception;
                response.Message = "Error";
                _logger.LogDebug(ex, "Error");
            }
            return response;
        }

        [HttpGet("UpdateOrderStatus")]
        public async Task<Response<bool>> UpdateOrderStatus(int orderId, string status)
        {
            Response<bool> response = new();
            if (orderId == 0 || string.IsNullOrWhiteSpace(status))
            {
                response.Message = "Bad Request";
                return response;
            }
            try
            {
                response = await _orderService.UpdateOrderStatus(orderId, status);
            }
            catch (Exception ex)
            {
                response.Code = EdrakBusiness.Enums.ResponseStatusEnum.Exception;
                response.Message = "Error";
                _logger.LogDebug(ex, "Error");
            }
            return response;
        }

        [HttpGet("CancelOrder")]
        public async Task<Response<bool>> CancelOrder(int orderId)
        {
            Response<bool> response = new();
            if (orderId == 0)
            {
                response.Message = "Bad Request";
                return response;
            }
            try
            {
                response = await _orderService.CancelOrder(orderId);
            }
            catch (Exception ex)
            {
                response.Code = EdrakBusiness.Enums.ResponseStatusEnum.Exception;
                response.Message = "Error";
                _logger.LogDebug(ex, "Error");
            }
            return response;
        }

        [HttpGet("GetOrders")]
        public async Task<Response<List<OrderDto>>> GetOrders(int limit)
        {
            Response<List<OrderDto>> response = new();
            if (limit == 0)
            {
                response.Message = "Bad Request";
                return response;
            }
            try
            {
                response = await _orderService.GetOrders(limit);
            }
            catch (Exception ex)
            {
                response.Code = EdrakBusiness.Enums.ResponseStatusEnum.Exception;
                response.Message = "Error";
                _logger.LogDebug(ex, "Error");
            }
            return response;
        }
    }
}