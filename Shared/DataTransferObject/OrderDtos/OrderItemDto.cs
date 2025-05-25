namespace Shared.DataTransferObject.OrderDtos
{
    public class OrderItemDto
    {
        
        public string ProductName { get; set; } = default!;

        public string Picture { get; set; } = default!;

        public decimal  Price { get; set; }

        public int Quantity {  get; set; }

    }
}