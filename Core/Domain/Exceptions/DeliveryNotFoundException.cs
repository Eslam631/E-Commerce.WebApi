using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
  public sealed class DeliveryNotFoundException(int id):NotFoundException($"Delivery Method With Id= {id} Not Found")
    {
    }
}
