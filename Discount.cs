using POS點餐機.DiscountFolder;
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


            String discountTypeName = "POS點餐機.DiscountFolder." + discountType;
            //設定
            Type type = Type.GetType(discountTypeName); //找到類別
            ADiscount getDiscount = (ADiscount)Activator.CreateInstance(type, new object[] { orders });
            //new 出來 -->把它需要的東西傳進去 
            getDiscount.DiscountOrder();
            //使用方法
            ShowPanel.Show(orders);
        }
    }
}