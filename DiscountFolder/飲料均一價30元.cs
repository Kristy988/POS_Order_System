using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountFolder
{
    internal class 飲料均一價30元 : ADiscount
    {
        public 飲料均一價30元(List<Item> orders) : base(orders)
        {
        }

        public override void DiscountOrder()
        {
            string[] drinks = { "珍珠奶茶", "烏龍奶茶", "奶青", "紅茶", "烏龍茶" };
            Item food = orders.FirstOrDefault(x => x.Name == "珍珠奶茶");
            Item food2 = orders.FirstOrDefault(x => x.Name == "烏龍奶茶");
            Item food3 = orders.FirstOrDefault(x => x.Name == "奶青");
            Item food4 = orders.FirstOrDefault(x => x.Name == "紅茶");
            Item food5 = orders.FirstOrDefault(x => x.Name == "烏龍茶");
            if (food != null)
            {
                orders.Add(new Item("(折扣)珍珠奶茶", "-35", food.Count.ToString()));
            }
            if (food2 != null)
            {
                orders.Add(new Item("(折扣)烏龍奶茶", "-25", food2.Count.ToString()));
            }
            if (food3 != null)
            {
                orders.Add(new Item("(折扣)奶青", "-20", food3.Count.ToString()));
            }
            if (food4 != null)
            {
                orders.Add(new Item("(折扣)紅茶", "-5", food4.Count.ToString()));
            }
            if (food5 != null)
            {
                orders.Add(new Item("(折扣)烏龍茶", "-5", food5.Count.ToString()));
            }
        }
    }
}
