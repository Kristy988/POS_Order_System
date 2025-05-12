using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountFolder
{
    internal abstract class ADiscount
    {
        protected List<Item> orders = new List<Item>();
        public ADiscount(List<Item> orders)
        {
            this.orders = orders;
        }
        public abstract void DiscountOrder();
    }
}
