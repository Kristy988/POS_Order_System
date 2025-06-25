using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS點餐機
{
    internal class Order
    {
        public static List<Item> orders = new List<Item>();
        public static void Add(MenuSpec.Discount discountType, Item item)
        {

            Item food = orders.FirstOrDefault(x => x.Name == item.Name);
            if (food == null)
            {
                orders.Add(item);
            }
            else
            {
                if (int.Parse(item.Count) != 0)
                {
                    food.Count = item.Count;
                }
                else
                {
                    orders.Remove(food);
                }

            }

            Discount.DiscountOrder(discountType, orders);

        }

        public static void RefreshOrder(MenuSpec.Discount discountType)
        {
            Discount.DiscountOrder(discountType, orders);

        }

    }
}
