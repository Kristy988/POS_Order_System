using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountFolder
{
    internal class 全場品項滿399打9折 : ADiscount
    {
        public 全場品項滿399打9折(List<Item> orders) : base(orders)
        {
        }

        public override void DiscountOrder()
        {
            int total = orders.Sum(x => int.Parse(x.SubTotal));
            double discountP = Math.Round(-total * 0.1);
            if (total >= 399)
            {
                orders.Add(new Item("(折扣)九折", discountP.ToString(), "1"));
            }
        }
    }
}
