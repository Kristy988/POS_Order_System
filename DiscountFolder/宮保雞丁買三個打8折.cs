using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountFolder
{
    internal class 宮保雞丁買三個打8折 : ADiscount
    {
        public 宮保雞丁買三個打8折(List<Item> orders) : base(orders)
        {
        }

        public override void DiscountOrder()
        {
            Item food = orders.FirstOrDefault(x => x.Name == "宮保雞丁飯");

            if (food != null)
            {
                int freeCount = int.Parse(food.Count) / 3;
                if (freeCount > 0)
                    orders.Add(new Item("(折扣)宮保雞丁飯8折", (-80 * 0.2).ToString(), freeCount.ToString()));
            }
        }
    }
}
