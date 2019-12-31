using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBMDSFinalProject
{
    public class OrderItem
    {
        //Defining all the Variables in the OrderItem Class.
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
