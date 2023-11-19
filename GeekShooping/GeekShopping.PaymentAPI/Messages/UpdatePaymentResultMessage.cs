using GeekShopping.MessageBus;

namespace GeekShopping.PaymentAPIAPI.Messages
{
    public class UpdatePaymentResultMessage : BaseMessage
    {

        public long OrderId { get; set; }
        public bool Status { get; set; }        
        public string Email { get; set; }
    }
}
