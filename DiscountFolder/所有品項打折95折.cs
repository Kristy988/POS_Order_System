using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountFolder
{
    internal class 所有品項打折95折 : ADiscount
    {
        public 所有品項打折95折(List<Item> orders) : base(orders)
        {
        }

        public override void DiscountOrder()
        {
            int total = orders.Sum(x => int.Parse(x.SubTotal));
            double discountPrice = Math.Round(-total * 0.05);
            orders.Add(new Item("(折扣)九五折", discountPrice.ToString(), "1"));
        }
    }
}
