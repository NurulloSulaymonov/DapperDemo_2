using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController:ControllerBase
{
    private  OrderService _orderService;
    public OrderController()
    {
        _orderService = new OrderService();
    }   
    
    [HttpGet("Orders")]
    public List<GetOrderDto> GetOrders()
    {
        var orders = _orderService.Orders();
        return orders;
    }
}