using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BasketModel
{
    public class CustomerBasket
    {
        public string Id { get; set; } = default!;

        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
