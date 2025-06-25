using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountStrategy
{
    internal abstract class AStrategy
    {
        protected List<Item> orders = new List<Item>();
        protected MenuSpec.Discount discount = new MenuSpec.Discount();
        public AStrategy(MenuSpec.Discount discount, List<Item> orders)
        {
            this.orders = orders;
            this.discount = discount;
        }

        public abstract void DiscountSrategy();
    }
}
