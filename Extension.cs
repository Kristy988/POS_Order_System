using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS點餐機
{
    internal static class Extension
    {

        public static int CountingDigitalOfString(this string input, int number)
        {
            int count = 0;
            foreach (var item in input)
            {
                if (Char.IsDigit(item))
                    count++;
            }

            return count;
        }
        public static void GenerateCheckbox(this FlowLayoutPanel flowLayoutPanel, string[] foodList, EventHandler checkedChange, EventHandler valueChange)
        {
            for (int i = 0; i < foodList.Length; i++)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.CheckedChanged += checkedChange;
                NumericUpDown numericUpDown = new NumericUpDown();
                numericUpDown.ValueChanged += valueChange;
                checkbox.Tag = numericUpDown;
                numericUpDown.Tag = checkbox;
                FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel();
                flowLayoutPanel1.Height = 30;
                numericUpDown.Width = 50;
                checkbox.Text = foodList[i];
                flowLayoutPanel1.Controls.Add(checkbox);
                flowLayoutPanel1.Controls.Add(numericUpDown);
                flowLayoutPanel.Controls.Add(flowLayoutPanel1);

            }
        }
        public static int CheckoutPrice(this FlowLayoutPanel flowLayoutPanel)
        {
            int price = 0;
            for (int i = 0; i < flowLayoutPanel.Controls.Count; i++)
            {
                FlowLayoutPanel flow = (FlowLayoutPanel)flowLayoutPanel.Controls[i];
                NumericUpDown num = (NumericUpDown)flow.Controls[1];
                int count = (int)num.Value;

                CheckBox checkbox = (CheckBox)flow.Controls[0];
                price += checkbox.Checked ? int.Parse(checkbox.Text.Split('$')[1]) * count : 0;
            }
            return price;
        }


    }
}
