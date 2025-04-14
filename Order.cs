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


        }
        public static FlowLayoutPanel Show()
        {
            FlowLayoutPanel flows = new FlowLayoutPanel();
            flows.Height = 621;
            flows.Width = 323;

            FlowLayoutPanel flow = getFlow("品名", "數量", "單價", "小計", flows.Width);
            flows.Controls.Add(flow);

            for (int i = 0; i < orders.Count; i++)
            {
                flow = getFlow(orders[i].Name, orders[i].Count, orders[i].Price, orders[i].SubTotal, flows.Width);
                flows.Controls.Add(flow);
            }

            return flows;
        }

        private static FlowLayoutPanel getFlow(string name, string count, string price, string total, int width)
        {
            FlowLayoutPanel flow = new FlowLayoutPanel();
            flow.BorderStyle = BorderStyle.FixedSingle;
            flow.Height = 25;
            flow.Width = width;
            Label productName = new Label();
            productName.Height = 25;
            productName.Width = 70;
            Label unitPrice = new Label();
            unitPrice.Height = 25;
            unitPrice.Width = 40;
            Label productCount = new Label();
            productCount.Height = 25;
            productCount.Width = 40;
            Label totalPrice = new Label();
            totalPrice.Height = 25;
            totalPrice.Width = 40;
            productName.Text = name;
            unitPrice.Text = price;
            productCount.Text = count;
            totalPrice.Text = total;
            flow.Controls.Add(productName);
            flow.Controls.Add(unitPrice);
            flow.Controls.Add(productCount);
            flow.Controls.Add(totalPrice);
            return flow;
        }
    }
}
