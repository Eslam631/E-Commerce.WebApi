﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class NotFoundAddressException(string userName):NotFoundException($"{userName} Hasn't  Address")
    {
    }
}
