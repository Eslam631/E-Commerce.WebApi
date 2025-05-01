using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
   public sealed class NotFoundEmailException(string email):NotFoundException($"User With Email ={email} Is Not Found")
    {
    }
}
