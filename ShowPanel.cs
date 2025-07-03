using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS點餐機
{
    internal class ShowPanel
    {
        public static void Show(List<Item> orders)
        {
            FlowLayoutPanel flows = new FlowLayoutPanel();
            flows.Height = 698;
            flows.Width = 422;

            FlowLayoutPanel flow = getFlow("品名", "數量", "單價", "小計", flows.Width);
            flows.Controls.Add(flow);

            for (int i = 0; i < orders.Count; i++)
            {
                flow = getFlow(orders[i].Name, orders[i].Count, orders[i].Price, orders[i].SubTotal, flows.Width);
                flows.Controls.Add(flow);
            }

            string checkoutPrice = orders.Sum(x => int.Parse(x.SubTotal)).ToString();
            OrderHandler.NotifyShowPanel((flows, checkoutPrice));
        }

        public static FlowLayoutPanel getFlow(string name, string count, string price, string total, int width)
        {
            FlowLayoutPanel flow = new FlowLayoutPanel();
            flow.Height = 35;
            flow.Width = width;
            Label productName = new Label();
            productName.Height = 35;
            productName.Width = 180;
            productName.Font = new Font("微軟正黑體", 12f);
            Label unitPrice = new Label();
            unitPrice.Height = 35;
            unitPrice.Width = 65;
            unitPrice.Font = new Font("微軟正黑體", 12f);
            Label productCount = new Label();
            productCount.Height = 35;
            productCount.Width = 65;
            productCount.Font = new Font("微軟正黑體", 12f);

            Label totalPrice = new Label();
            totalPrice.Height = 25;
            totalPrice.Width = 65;
            totalPrice.Font = new Font("微軟正黑體", 12f);

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
