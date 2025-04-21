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
        public static void Add(Item item)
        {

            Item food = orders.FirstOrDefault(x => x.Name == item.Name);
            if (food == null)
            {
                orders.Add(item);
                ShowPanel.Show(orders);
            }
            else
            {
                if (int.Parse(item.Count) != 0)
                {
                    food.Count = item.Count;
                    ShowPanel.Show(orders);
                }
                else
                {
                    orders.Remove(food);
                    ShowPanel.Show(orders);
                }

            }

        }

    }
}
