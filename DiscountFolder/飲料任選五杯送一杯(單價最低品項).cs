using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountFolder
{
    internal class 飲料任選五杯送一杯_單價最低品項_ : ADiscount
    {
        public 飲料任選五杯送一杯_單價最低品項_(List<Item> orders) : base(orders)
        {
        }

        public override void DiscountOrder()
        {
            string[] drinks = { "珍珠奶茶", "烏龍奶茶", "奶青", "紅茶", "烏龍茶" };
            int buyQty = 0;
            int minPrice = 999;
            string itemName = "";

            for (int i = 0; i < orders.Count; i++)
            {
                if (drinks.Contains(orders[i].Name))
                {
                    buyQty += int.Parse(orders[i].Count);
                    if (int.Parse(orders[i].Price) < minPrice)
                    {
                        minPrice = int.Parse(orders[i].Price);
                        itemName = orders[i].Name;
                    }
                }

            }
            if (buyQty / 5 > 0)
            {
                orders.Add(new Item($"(贈送){itemName}", "0", (buyQty / 5).ToString()));
            }
        }
    }
}
