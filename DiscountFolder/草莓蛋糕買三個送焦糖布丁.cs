using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountFolder
{
    internal class 草莓蛋糕買三個送焦糖布丁 : ADiscount
    {
        public 草莓蛋糕買三個送焦糖布丁(List<Item> orders) : base(orders)
        {
        }

        public override void DiscountOrder()
        {
            Item food = orders.FirstOrDefault(x => x.Name == "草莓蛋糕");

            if (food != null)
            {
                int freeCount = int.Parse(food.Count) / 3;
                if (freeCount > 0)
                    orders.Add(new Item("(贈送)焦糖布丁", "0", freeCount.ToString()));
            }
        }
    }
}
