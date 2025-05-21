using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountFolder
{
    internal class 所有品項買二送一 : ADiscount
    {
        public 所有品項買二送一(List<Item> orders) : base(orders)
        {
        }

        public override void DiscountOrder()
        {
            int buyQty = 0;
            int minPrice = 999;
            string itemName = "";
            for (int i = 0; i < orders.Count; i++)
            {
                buyQty += int.Parse(orders[i].Count);
                if (int.Parse(orders[i].Price) < minPrice)
                {
                    minPrice = int.Parse(orders[i].Price);
                    itemName = orders[i].Name;
                }
            }
            if (buyQty / 2 > 0)
            {
                orders.Add(new Item($"(贈送){itemName}", "0", (buyQty / 3).ToString()));
            }
        }
    }
}
