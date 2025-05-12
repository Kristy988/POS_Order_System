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

        string[] foods = { "雞肉飯$70", "排骨飯$75", "雞腿飯$90", "糖醋小排飯$85", "宮保雞丁飯$80" };
        string[] sides = { "鹽酥雞$60", "炸雞排$60", "滷排骨$70", "炸雞腿$80", "小黃瓜$40" };
        string[] drinks = { "珍珠奶茶$65", "烏龍奶茶$55", "奶青$50", "紅茶$35", "烏龍茶$35" };
        string[] sweets = { "草莓蛋糕$150", "原味鬆餅$120", "抹茶奶凍$60", "焦糖布丁$75", "草莓雪媚娘$60" };
        private void Form1_Load(object sender, EventArgs e)
        {
            OrderHandler.showPanelHander += ShowPanelHandler;
            flowLayoutPanel1.GenerateCheckbox(foods, Checkbox_CheckedChange, Numericupdown_ValueChange);
            flowLayoutPanel2.GenerateCheckbox(sides, Checkbox_CheckedChange, Numericupdown_ValueChange);
            flowLayoutPanel3.GenerateCheckbox(drinks, Checkbox_CheckedChange, Numericupdown_ValueChange);
            flowLayoutPanel4.GenerateCheckbox(sweets, Checkbox_CheckedChange, Numericupdown_ValueChange);

            comboBox1.SelectedIndex = 0;

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
            Order.Add(comboBox1.SelectedItem.ToString(), item);


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
            string discountItem = comboBox1.SelectedItem.ToString();
            Order.RefreshOrder(discountItem);
        }
    }
}
