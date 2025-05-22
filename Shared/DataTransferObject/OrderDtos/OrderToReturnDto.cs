using Shared.DataTransferObject.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.OrderDtos
{
    public class OrderToReturnDto
    {

        public Guid Id { get; set; }
        public string UserEmail { get; set; } = default!;

        public DateTimeOffset OrderDate { get; set; } 

        public string OrderStatus { get; set; } = default!;

        public AddressDto Address { get; set; } = default!;

        public string DeliveryMethod { get; set; } = default!;
       

        public ICollection<OrderItemDto> OrderItems { get; set; } = [];

        public decimal SubTotal { get; set; }

        public decimal Total {  get; set; }





    }
}
