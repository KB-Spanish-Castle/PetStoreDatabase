using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBMDSFinalProject
{
    public class Order
    {
        //Defining all the Variables in the Order Class.
        public int Id { get; set; }
        public List<OrderItem> OrderItemList { get; set; } = new List<OrderItem>();
        public Customer Customer { get; set; }
        public DateTime DateItem { get; set; }
        public string Status { get; set; } = "In Cart";

    }
}
