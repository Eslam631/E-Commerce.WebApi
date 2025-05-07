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

    }
}
