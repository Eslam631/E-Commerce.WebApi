using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
   public class OrderController(IServiceManager _serviceManager):ApiBaseController
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto) {

            var Order = await _serviceManager.OrderService.CreateOrderAsync(orderDto,GetEmailFromToken());
            return Ok(Order);
        
        }


        [AllowAnonymous]
        [HttpGet("DeliveryMethod")]

        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetAllDeliveryMethod()
        {
        var Delivery= await  _serviceManager.OrderService.GetAllDeliveryMethodAsync();
        
            return Ok(Delivery);
        
        }

        [HttpGet()]
        [Authorize]

        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrder()
        {
            var Order =await _serviceManager.OrderService.GetAllOrderAsync(GetEmailFromToken());
            return Ok(Order);
        }

        [HttpGet("{id:guid}")]
        [Authorize]

        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id) {

            var Order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
        
            return Ok(Order);
        }


    }
}
