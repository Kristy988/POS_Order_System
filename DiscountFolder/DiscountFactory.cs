using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountFolder
{
    internal class DiscountFactory
    {
        public static ADiscount GetDiscount(string discountType, List<Item> orders)
        {
            ADiscount discount = null;
            switch (discountType)
            {
                case "雞肉飯買二送一":
                    discount = new 雞肉飯買二送一(orders);
                    break;
                case "排骨飯買三個210元":
                    discount = new 排骨飯買三個210元(orders);
                    break;
                case "草莓蛋糕買三個送焦糖布丁":
                    discount = new 草莓蛋糕買三個送焦糖布丁(orders);
                    break;
                case "雞肉飯加鹽酥雞加烏龍奶茶加原味鬆餅折扣100":
                    discount = new 雞肉飯加鹽酥雞加烏龍奶茶加原味鬆餅折扣100(orders);
                    break;
                case "宮保雞丁買三個打8折":
                    discount = new 宮保雞丁買三個打8折(orders);
                    break;
                case "全場品項滿399打9折":
                    discount = new 全場品項滿399打9折(orders);
                    break;
                case "飲料任選五杯送一杯(單價最低品項)":
                    discount = new 飲料任選五杯送一杯_單價最低品項_(orders);
                    break;
                case "飲料均一價30元":
                    discount = new 飲料均一價30元(orders);
                    break;
                case "所有品項買二送一":
                    discount = new 所有品項買二送一(orders);
                    break;
                case "所有品項打折95折":
                    discount = new 所有品項打折95折(orders);
                    break;
            }
            return discount;
        }
    }
}
