using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountFolder
{
    internal class 雞肉飯加鹽酥雞加烏龍奶茶加原味鬆餅折扣100 : ADiscount
    {
        public 雞肉飯加鹽酥雞加烏龍奶茶加原味鬆餅折扣100(List<Item> orders) : base(orders)
        {
        }

        public override void DiscountOrder()
        {
            Item food = orders.FirstOrDefault(x => x.Name == "雞肉飯");
            Item food2 = orders.FirstOrDefault(x => x.Name == "鹽酥雞");
            Item food3 = orders.FirstOrDefault(x => x.Name == "烏龍奶茶");
            Item food4 = orders.FirstOrDefault(x => x.Name == "原味鬆餅");
            if (food != null && food2 != null && food3 != null && food4 != null)
            {
                int freeCount = (int.Parse(food.Count) + int.Parse(food2.Count) + int.Parse(food3.Count) + int.Parse(food4.Count)) / 4;
                if (freeCount > 0)
                    orders.Add(new Item("(折扣)套餐折扣100元", "-100", freeCount.ToString()));
            }
        }
    }
}
