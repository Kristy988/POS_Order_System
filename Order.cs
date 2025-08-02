using POS點餐機.Models;
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
        public static async Task Add(OrderRequestModel orderRequestModel)
        {

            Item food = orders.FirstOrDefault(x => x.Name == orderRequestModel.OrderItem.Name);
            if (food == null)
            {
                orders.Add(orderRequestModel.OrderItem);
            }
            else
            {
                if (int.Parse(orderRequestModel.OrderItem.Count) != 0)
                {
                    food.Count = orderRequestModel.OrderItem.Count;
                }
                else
                {
                    orders.Remove(food);
                }

            }
            orderRequestModel.Orders = orders;
            await Discount.DiscountOrder(orderRequestModel);

        }

        public static async Task RefreshOrder(OrderRequestModel orderRequestModel)
        {
            orderRequestModel.Orders = orders;
            await Discount.DiscountOrder(orderRequestModel);
        }

    }
}
