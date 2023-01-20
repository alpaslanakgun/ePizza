using ePizza.Repositories.Models;

namespace ePizza.WebUI.Models
{
    public class PaymentViewModel
    {
        public string Name { get; set; }
        public string RazorpayKey { get; set; }
        public decimal GrandTotal { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string OrderId { get; set; }
        public CartModel Cart { get; set; }
        public string Receipt { get; internal set; }
    }
}
