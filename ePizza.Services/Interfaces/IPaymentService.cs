using ePizza.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Razorpay.Api;

namespace ePizza.Services.Interfaces
{
    public interface IPaymentService
    {
        string CreateOrder(decimal amount, string currency, string receipt);
        string CapturePayment(string paymentId, string orderId);
        Payment GetPaymentDetails(string paymentId);
        bool VerifySignature(string signature, string orderId, string paymentId);
       Task<int> SavePaymentDetails(PaymentDetails model);
    }
}
