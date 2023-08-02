namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(IReadOnlyList<OrderItem> orderItems,string buyerEmail, Address shipToAddress
        , DeliveryMethod deliveryMethod
        , decimal subtotal)
        {
            BuyerEmail = buyerEmail;           
            this.shipToAddress = shipToAddress;
            this.deliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;            
        }

        public string BuyerEmail { get; set; }
        public DateTime OrderDate {get;set;} = DateTime.UtcNow;
        public Address shipToAddress { get; set; }
        public DeliveryMethod deliveryMethod {get;set;}
        public IReadOnlyList<OrderItem> OrderItems {get;set;}
        public decimal Subtotal { get; set; }
        public OrderStatus status {get;set;} = OrderStatus.Pending;

        public string PaymentIntentId {get;set;}

        public decimal GetTotal()
        {
            return Subtotal + deliveryMethod.Price;
        }
    }
}