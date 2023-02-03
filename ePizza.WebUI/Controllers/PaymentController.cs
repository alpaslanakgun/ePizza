using ePizza.Entities.Concrete;
using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using ePizza.Services.Models;
using ePizza.WebUI.Helpers;
using ePizza.WebUI.Interfaces;
using ePizzaHub.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using ePizza.WebUI.Models;

namespace ePizza.WebUI.Controllers
{
    public class PaymentController :BaseController
    {
        private readonly IOptions<RazorPayConfig> _razorPayConfig;
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        public PaymentController(IPaymentService paymentService, IOptions<RazorPayConfig> razorPayConfig, IUserAccessor userAccessor, IOrderService orderService) : base(userAccessor)
        {
            _razorPayConfig = razorPayConfig;
            _paymentService = paymentService;
            _orderService = orderService;
        }


        public IActionResult Index()
        {
            PaymentViewModel payment = new PaymentViewModel();
            CartModel cart = TempData.Peek<CartModel>("Cart");
            if (cart != null)
            {
                payment.Cart = cart;
            }
            payment.GrandTotal = Math.Round(cart.GrandTotal);
            payment.Currency = "INR";
            string items = "";
            foreach (var item in cart.Products)
            {
                items += item.Name + ",";
            }
            payment.Description = items;
            payment.RazorpayKey = _razorPayConfig.Value.Key;
            payment.Receipt = Guid.NewGuid().ToString();

            //flow in razor pay: need to create order first
            payment.OrderId = _paymentService.CreateOrder(payment.GrandTotal * 100, payment.Currency, payment.Receipt);

            return View(payment);
        }

        [HttpPost]
        public IActionResult Status(IFormCollection form)
        {
            try
            {
                if (form.Keys.Count > 0 && !String.IsNullOrWhiteSpace(form["rzp_paymentid"]))
                {
                    string paymentId = form["rzp_paymentid"];
                    string orderId = form["rzp_orderid"];
                    string signature = form["rzp_signature"];
                    string transactionId = form["Receipt"];
                    string currency = form["Currency"];

                    var payment = _paymentService.GetPaymentDetails(paymentId);
                    bool IsSignVerified = _paymentService.VerifySignature(signature, orderId, paymentId);

                    if (IsSignVerified && payment != null)
                    {
                        CartModel cart = TempData.Get<CartModel>("Cart");
                        PaymentDetails model = new PaymentDetails();

                        model.CartId = cart.Id;
                        model.Total = cart.Total;
                        model.Tax = cart.Tax;
                        model.GrandTotal = cart.GrandTotal;

                        model.Status = payment.Attributes["status"]; //captured
                        model.TransactionId = transactionId;
                        model.Currency = payment.Attributes["currency"];
                        model.Email = payment.Attributes["email"];
                        model.Id = paymentId;
                        model.UserId = CurrentUser.Id;

                        var status =Convert.ToInt32(_paymentService.SavePaymentDetails(model));
                        if (status>0)
                        {
                            Response.Cookies.Append("CId", ""); //resettingg cartId in cookie

                            Address address = TempData.Get<Address>("Address");
                            _orderService.PlaceOrder(CurrentUser.Id, orderId, paymentId, cart, address);

                            //TO DO: Send email
                            TempData.Set("PaymentDetails", model);
                            return RedirectToAction("Receipt");
                        }
                        else
                        {
                            ViewBag.Message = "Odeme alınırken bir hata olustu Yakında sizinle iletişime geçecegiz.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.Message = "Ödemeniz başarısız oldu support@epizza.com ile iletişime geçiniz.";
            return View();
        }

        public IActionResult Receipt()
        {
            PaymentDetails model = TempData.Peek<PaymentDetails>("PaymentDetails");
            return View(model);
        }



    }
}
