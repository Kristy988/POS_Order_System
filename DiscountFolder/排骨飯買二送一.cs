using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountFolder
{
    internal class 排骨飯買二送一 : ADiscount
    {
        public 排骨飯買二送一(List<Item> orders) : base(orders)
        {
        }

        public override void DiscountOrder()
        {
            Item food = orders.FirstOrDefault(x => x.Name == "排骨飯");

            if (food != null)
            {
                int freeCount = int.Parse(food.Count) / 2;
                if (freeCount > 0)
                    orders.Add(new Item("(贈送)排骨飯", "0", freeCount.ToString()));
            }
        }
    }
}
