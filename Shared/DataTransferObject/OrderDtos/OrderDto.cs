using Shared.DataTransferObject.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.OrderDtos
{
    public class OrderDto
    {
        public string BasketId { get; set; } = default!;

        public AddressDto shipToAddress { get; set; } = default!;
        public int DeliveryMethodId {  get; set; }


    }
}
