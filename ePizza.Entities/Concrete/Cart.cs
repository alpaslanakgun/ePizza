using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Entities.Concrete
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<CartItem> CartItems { get; private set; }

        public bool IsActive { get; set; }
    }
}
