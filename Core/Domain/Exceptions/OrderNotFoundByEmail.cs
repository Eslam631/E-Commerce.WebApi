using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
   public sealed class OrderNotFoundByEmail:NotFoundException
    {
        public OrderNotFoundByEmail(string Email):base($"this Is Email={Email} Not Have Any Order")
        {
            
        }
        public OrderNotFoundByEmail(Guid id) : base($"Order With ID={id} Not Found")
        {

        }
    }
}
