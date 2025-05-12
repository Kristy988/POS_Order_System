using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機
{
    internal class Discount
    {
        public static void DiscountOrder(string discountType, List<Item> orders)
        {
            orders.RemoveAll(x => x.Name.Contains("贈送") || x.Name.Contains("折扣"));
            //雞肉飯買二送一
            //排骨飯買三個210元
            //雞腿飯加滷排骨150元
            //草莓蛋糕買三個送焦糖布丁
            //雞肉飯加鹽酥雞加烏龍奶茶加原味鬆餅折扣100
            //宮保雞丁買三個打8折
            //全場品項滿399打9折
            //飲料任選五杯送一杯(單價最低品項)
            //飲料均一價30元
            //所有品項買二送一
            //所有品項打折95折

            if (discountType == "雞肉飯買二送一")
            {
                Item food = orders.FirstOrDefault(x => x.Name == "雞肉飯");

                if (food != null)
                {
                    int freeCount = int.Parse(food.Count) / 2;
                    if (freeCount > 0)
                        orders.Add(new Item("(贈送)雞肉飯", "0", freeCount.ToString()));
                }
            }
            else if (discountType == "排骨飯買三個210元")
            {
                Item food = orders.FirstOrDefault(x => x.Name == "排骨飯");

                if (food != null)
                {
                    int freeCount = int.Parse(food.Count) / 3;
                    if (freeCount > 0)
                        orders.Add(new Item("(折扣)排骨飯", "-5", food.Count));
                }
            }
            else if (discountType == "雞腿飯加滷排骨150元")
            {
                Item food = orders.FirstOrDefault(x => x.Name == "雞腿飯");
                Item food2 = orders.FirstOrDefault(x => x.Name == "滷排骨");
                if (food != null && food2 != null)
                {
                    int freeCount = (int.Parse(food.Count) + int.Parse(food2.Count)) / 2;
                    if (freeCount > 0)
                        orders.Add(new Item("(折扣)雞腿飯加滷排骨", "-10", freeCount.ToString()));
                }
            }
            else if (discountType == "草莓蛋糕買三個送焦糖布丁")
            {
                Item food = orders.FirstOrDefault(x => x.Name == "草莓蛋糕");

                if (food != null)
                {
                    int freeCount = int.Parse(food.Count) / 3;
                    if (freeCount > 0)
                        orders.Add(new Item("(贈送)焦糖布丁", "0", freeCount.ToString()));
                }
            }
            else if (discountType == "雞肉飯加鹽酥雞加烏龍奶茶加原味鬆餅折扣100")
            {
                Item food = orders.FirstOrDefault(x => x.Name == "雞肉飯");
                Item food2 = orders.FirstOrDefault(x => x.Name == "鹽酥雞");
                Item food3 = orders.FirstOrDefault(x => x.Name == "烏龍奶茶");
                Item food4 = orders.FirstOrDefault(x => x.Name == "原味鬆餅");
                if (food != null && food2 != null && food3 != null && food4 != null)
                {
                    int freeCount = (int.Parse(food.Count) + int.Parse(food2.Count) + int.Parse(food3.Count) + int.Parse(food4.Count)) / 4;
                    if (freeCount > 0)
                        orders.Add(new Item("(折扣)套餐折扣100元", "-100", freeCount.ToString()));
                }
            }
            else if (discountType == "宮保雞丁買三個打8折")
            {
                Item food = orders.FirstOrDefault(x => x.Name == "宮保雞丁飯");

                if (food != null)
                {
                    int freeCount = int.Parse(food.Count) / 3;
                    if (freeCount > 0)
                        orders.Add(new Item("(折扣)宮保雞丁飯8折", (-80 * 0.2).ToString(), freeCount.ToString()));
                }
            }
            else if (discountType == "全場品項滿399打9折")
            {
                //Item food = orders.FirstOrDefault(x => x.Name == "雞肉飯");
                int total = orders.Sum(x => int.Parse(x.SubTotal));
                double discountP = Math.Round(-total * 0.1);
                if (total >= 399)
                {
                    orders.Add(new Item("(折扣)九折", discountP.ToString(), "1"));
                }
            }
            else if (discountType == "飲料任選五杯送一杯(單價最低品項)")
            {


                string[] drinks = { "珍珠奶茶", "烏龍奶茶", "奶青", "紅茶", "烏龍茶" };
                int buyQty = 0;
                int minPrice = 999;
                string itemName = "";

                for (int i = 0; i < orders.Count; i++)
                {
                    if (drinks.Contains(orders[i].Name))
                    {
                        buyQty += int.Parse(orders[i].Count);
                        if (int.Parse(orders[i].Price) < minPrice)
                        {
                            minPrice = int.Parse(orders[i].Price);
                            itemName = orders[i].Name;
                        }
                    }

                }
                if (buyQty / 5 > 0)
                {
                    orders.Add(new Item($"(贈送){itemName}", "0", (buyQty / 5).ToString()));
                }
            }
            else if (discountType == "飲料均一價30元")
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

            else if (discountType == "所有品項買二送一")
            {
                int buyQty = 0;
                int minPrice = 999;
                string itemName = "";
                for (int i = 0; i < orders.Count; i++)
                {
                    buyQty += int.Parse(orders[i].Count);
                    if (int.Parse(orders[i].Price) < minPrice)
                    {
                        minPrice = int.Parse(orders[i].Price);
                        itemName = orders[i].Name;
                    }
                }
                if (buyQty / 3 > 0)
                {
                    orders.Add(new Item($"(贈送){itemName}", "0", (buyQty / 3).ToString()));
                }
            }
            else if (discountType == "所有品項打折95折")
            {
                int total = orders.Sum(x => int.Parse(x.SubTotal));
                double discountPrice = Math.Round(-total * 0.05);
                orders.Add(new Item("(折扣)九五折", discountPrice.ToString(), "1"));

            }
            ShowPanel.Show(orders);
        }
    }
}