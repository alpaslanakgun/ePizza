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
            Products = new List<CartItem>();
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<CartItem> Products { get; private set; }

        public bool IsActive { get; set; }
    }
}
