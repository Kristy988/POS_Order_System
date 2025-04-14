using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        //FlowLayoutPanel flowDetail = new FlowLayoutPanel();
        //Dictionary<string, FlowLayoutPanel> flows = new Dictionary<string, FlowLayoutPanel>();
        string[] foods = { "雞肉飯$70", "排骨飯$75", "雞腿飯$90", "糖醋小排飯$85", "宮保雞丁飯$80" };
        string[] sides = { "鹽酥雞$60", "炸雞排$60", "滷排骨$70", "炸雞腿$80", "小黃瓜$40" };
        string[] drinks = { "珍珠奶茶$65", "烏龍奶茶$55", "奶青$50", "紅茶$35", "烏龍茶$35" };
        string[] sweets = { "草莓蛋糕$150", "原味鬆餅$120", "抹茶奶凍$60", "焦糖布丁$75", "草莓雪媚娘$60" };
        string[] labels = { "品名", "單價", "數量", "小計" };
        private void Form1_Load(object sender, EventArgs e)
        {

            //string words = "abc123LL123";
            //Console.WriteLine(words.CountingDigitalOfString(10));
            flowLayoutPanel1.GenerateCheckbox(foods, Checkbox_CheckedChange, Numericupdown_ValueChange);
            flowLayoutPanel2.GenerateCheckbox(sides, Checkbox_CheckedChange, Numericupdown_ValueChange);
            flowLayoutPanel3.GenerateCheckbox(drinks, Checkbox_CheckedChange, Numericupdown_ValueChange);
            flowLayoutPanel4.GenerateCheckbox(sweets, Checkbox_CheckedChange, Numericupdown_ValueChange);

            flowLayoutPanel5.Controls.Add(Order.Show());

        }

        #region First version
        //private FlowLayoutPanel Added_Receipt(CheckBox checkbox, NumericUpDown numericUpDown)
        //{

        //FlowLayoutPanel flow = new FlowLayoutPanel();
        //flow.BorderStyle = BorderStyle.FixedSingle;
        //flow.Height = 25;
        //flow.Width = flowLayoutPanel5.Width;
        //Label productName = new Label();
        //productName.Height = 25;
        //productName.Width = 70;
        //Label unitPrice = new Label();
        //unitPrice.Height = 25;
        //unitPrice.Width = 40;
        //Label count = new Label();
        //count.Height = 25;
        //count.Width = 40;
        //Label totalPrice = new Label();
        //totalPrice.Height = 25;
        //totalPrice.Width = 40;
        //productName.Text = checkbox.Text.Split('$')[0];
        //unitPrice.Text = checkbox.Text.Split('$')[1];
        //count.Text = numericUpDown.Value.ToString();
        //totalPrice.Text = (int.Parse(unitPrice.Text) * numericUpDown.Value).ToString();


        //if (flows.ContainsKey(productName.Text))
        //{
        //    flowLayoutPanel5.Controls.Remove(flows[productName.Text]);
        //    flows.Remove(productName.Text);
        //}


        //if (int.Parse(count.Text) != 0)
        //{

        //    flow.Controls.Add(productName);
        //    flow.Controls.Add(unitPrice);
        //    flow.Controls.Add(count);
        //    flow.Controls.Add(totalPrice);
        //    flows.Add(productName.Text, flow);
        //}

        //return flow;

        //}
        #endregion
        private void Checkbox_CheckedChange(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            #region Parent寫法
            //NumericUpDown numeric = (NumericUpDown)checkBox.Parent.Controls[1];
            #endregion

            NumericUpDown numeric = (NumericUpDown)checkBox.Tag;
            if (checkBox.Checked)
            {
                numeric.Value = 1;

            }
            else
                numeric.Value = 0;



        }

        private void Numericupdown_ValueChange(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            #region Parent寫法
            //CheckBox checkBox = (CheckBox)numericUpDown.Parent.Controls[0];
            #endregion

            CheckBox checkBox = (CheckBox)numericUpDown.Tag;

            if (numericUpDown.Value > 0)
            {
                checkBox.Checked = true;

            }
            else
                checkBox.Checked = false;

            Item item = new Item(checkBox.Text.Split('$')[0], checkBox.Text.Split('$')[1], numericUpDown.Value.ToString());
            Order.Add(item);
            flowLayoutPanel5.Controls.Clear();
            flowLayoutPanel5.Controls.Add(Order.Show());


            //flowDetail = Added_Receipt(checkBox, numericUpDown);
            //if (checkBox.Checked)
            //{
            //    flowLayoutPanel5.Controls.Add(flowDetail);

            //}

        }


        private void button1_Click(object sender, EventArgs e)
        {
            int totalPrice = flowLayoutPanel1.CheckoutPrice() + flowLayoutPanel2.CheckoutPrice() + flowLayoutPanel3.CheckoutPrice() + flowLayoutPanel4.CheckoutPrice();
            label1.Text = totalPrice.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
