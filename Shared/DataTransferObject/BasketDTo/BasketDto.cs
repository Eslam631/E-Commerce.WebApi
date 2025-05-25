namespace Shared.DataTransferObject.BasketDTo
{
    public class BasketDto
    {
        public string Id { get; set; } = default!;

        public ICollection<BasketItemDto> Items { get; set; } = [];

        public string? clientSecret { get; set; }

        public string? PaymentIntentId {  get; set; }

        public int? deliveryMethodId { get; set; }
        public decimal? shippingPrice { get; set; }


    }
}
