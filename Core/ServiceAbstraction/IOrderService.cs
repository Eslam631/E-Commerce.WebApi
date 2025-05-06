﻿using Shared.DataTransferObject.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
  public interface IOrderService
    {

        public Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto,string Email);
    }
}
