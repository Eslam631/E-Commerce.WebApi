using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModules
{
   public class Order:BaseEntity<Guid>
    {

        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress address, ICollection<OrderItem> orderItems, decimal subTotal, DeliveryMethod deliveryMethod)
        {
            UserEmail = userEmail;
            Address = address;
            OrderItems = orderItems;
            SubTotal = subTotal;
            DeliveryMethod = deliveryMethod;
        }

        public string UserEmail { get; set; } = default!;
        public OrderAddress Address { get; set; } = default!;
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        public decimal SubTotal {  get; set; }
        public DeliveryMethod DeliveryMethod { get; set; } = default!;



        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;

        public OrderStatus OrderStatus { get; set; } 


        public int DeliveryMethodId {  get; set; }



        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;


     






    }
}
