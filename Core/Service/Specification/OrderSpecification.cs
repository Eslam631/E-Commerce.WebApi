using Domain.Models.OrderModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification
{
    internal class OrderSpecification:BaseSpecification<Order,Guid>
    {
        public OrderSpecification(string Email):base(O=>O.UserEmail==Email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O=>O.Address);
            AddOrderByDesc(O=>O.OrderDate);
            
        }

        public OrderSpecification(Guid id):base(O=>O.Id==id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Address);

        }
    }
}
