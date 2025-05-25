namespace Domain.Models.OrderModules
{
   public class Order:BaseEntity<Guid>
    {

        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress address, ICollection<OrderItem> orderItems, decimal subTotal, DeliveryMethod deliveryMethod)
        {
            BuyerEmail = userEmail;
            shipToAddress = address;
            Items = orderItems;
            SubTotal = subTotal;
            DeliveryMethod = deliveryMethod;
        }

        public string BuyerEmail { get; set; } = default!;
        public OrderAddress shipToAddress { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal {  get; set; }
        public DeliveryMethod DeliveryMethod { get; set; } = default!;



        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;

        public OrderStatus Status { get; set; } 


        public int DeliveryMethodId {  get; set; }



        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;


     






    }
}
