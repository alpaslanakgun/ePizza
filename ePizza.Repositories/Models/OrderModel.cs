using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repositories.Models
{
    public class OrderModel
    {
        public OrderModel()
        {
            Products = new List<ProductModel>();
        }
        public string Id { get; set; }
        public string PaymentId { get; set; }
        public int UserId { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<ProductModel> Products { get; set; }
        public string Locality { get; internal set; }
    }
}
