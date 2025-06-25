using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountStrategy
{
    internal class Combo_Fixed_Price_Discount : AStrategy
    {
        public Combo_Fixed_Price_Discount(MenuSpec.Discount discount, List<Item> orders) : base(discount, orders)
        {

        }

        public override void DiscountSrategy()
        {
        }
    }
}
