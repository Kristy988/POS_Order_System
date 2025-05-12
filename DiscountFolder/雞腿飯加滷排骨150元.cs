using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountFolder
{
    internal class 雞腿飯加滷排骨150元 : ADiscount
    {
        public 雞腿飯加滷排骨150元(List<Item> orders) : base(orders)
        {
        }

        public override void DiscountOrder()
        {
            Item food = orders.FirstOrDefault(x => x.Name == "雞腿飯");
            Item food2 = orders.FirstOrDefault(x => x.Name == "滷排骨");
            if (food != null && food2 != null)
            {
                int freeCount = (int.Parse(food.Count) + int.Parse(food2.Count)) / 2;
                if (freeCount > 0)
                    orders.Add(new Item("(折扣)雞腿飯加滷排骨", "-10", freeCount.ToString()));
            }
        }
    }
}
