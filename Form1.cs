using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS點餐機
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OrderHandler.showPanelHander += ShowPanelHandler;
            string menuPath = ConfigurationManager.AppSettings["MenuPath"];
            string menuContent = File.ReadAllText(menuPath);
            //物件轉json字串 => 序列化 Serialize
            //json字串 轉物件 => 反序列化 DeSerialize
            MenuSpec menuSpec = JsonConvert.DeserializeObject<MenuSpec>(menuContent);
            MenuSpec.Menu[] menus = menuSpec.Menus;


            //var temp = menus.GroupBy(x => x.Title).Select(x => new
            //{
            //    Title = x.Key,
            //    TotalPrice = x.Sum(y => y.Foods.Sum(z => int.Parse(z.Price)))
            //}).ToList();






            for (int i = 0; i < menus.Length; i++)
            {
                Label label = new Label();
                FlowLayoutPanel flow = new FlowLayoutPanel();
                label.Text = menus[i].Title;
                flow.Controls.Add(label);
                flow.Width = 200;
                flow.Height = 300;
                for (int j = 0; j < menus[i].Foods.Length; j++)
                {
                    FlowLayoutPanel foodFlow = new FlowLayoutPanel();
                    foodFlow.Width = 200;
                    foodFlow.Height = 30;
                    CheckBox productName = new CheckBox();
                    productName.CheckedChanged += Checkbox_CheckedChange;
                    productName.Width = 130;
                    productName.Height = 25;
                    NumericUpDown numericUpDown = new NumericUpDown();
                    numericUpDown.Width = 40;
                    numericUpDown.ValueChanged += Numericupdown_ValueChange;
                    productName.Tag = numericUpDown;
                    numericUpDown.Tag = productName;
                    productName.Text = menus[i].Foods[j].Name + "$" + menus[i].Foods[j].Price;
                    foodFlow.Controls.Add(productName);
                    foodFlow.Controls.Add(numericUpDown);
                    flow.Controls.Add(foodFlow);
                }
                MenuContainer.Controls.Add(flow);

            }

            comboBox1.DataSource = menuSpec.Discounts;
            comboBox1.DisplayMember = "Name";
        }


        private void Checkbox_CheckedChange(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            NumericUpDown numeric = (NumericUpDown)checkBox.Tag;
            numeric.Value = checkBox.Checked ? 1 : 0;

        }

        private void Numericupdown_ValueChange(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            CheckBox checkBox = (CheckBox)numericUpDown.Tag;
            checkBox.Checked = numericUpDown.Value > 0 ? true : false;
            Item item = new Item(checkBox.Text.Split('$')[0], checkBox.Text.Split('$')[1], numericUpDown.Value.ToString());
            Order.Add((MenuSpec.Discount)comboBox1.SelectedValue, item);


        }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    label1.Text = ShowPanel.checkoutPrice.ToString();
        //}

        public void ShowPanelHandler(object sender, (FlowLayoutPanel, String) response)
        {
            //(FlowLayoutPanel panel, string lab) = response;

            flowLayoutPanel5.Controls.Clear();
            flowLayoutPanel5.Controls.Add(response.Item1);

            label1.Text = response.Item2.ToString();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MenuSpec.Discount discountItem = (MenuSpec.Discount)comboBox1.SelectedValue;
            Order.RefreshOrder(discountItem);
        }
    }
}
