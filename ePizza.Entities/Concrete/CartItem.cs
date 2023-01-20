using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ePizza.Entities.Concrete
{
    public class CartItem
    {
        public CartItem()
        {
            // required by EF
        }
        public CartItem(int productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public Guid CartId { get; set; }
        public int ProductId { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public Cart Cart { get; set; }
    }
}
