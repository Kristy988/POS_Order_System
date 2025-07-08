using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountStrategy
{
    internal class Order_Total_Discount : AStrategy
    {
        public Order_Total_Discount(MenuSpec.Discount discount, List<Item> orders) : base(discount, orders)
        {
        }

        public override void DiscountSrategy()
        {
            var temp_conditions = discount.Conditions.SelectMany((x, index) => x.Item.Split(',').Select((y) => new
            {
                ConditionID = index,
                ConditionQty = x.Count,
                ProductNames = y,
                MinPrice = x.MinPrice

            })).ToList();

            var checkConditionItem = temp_conditions.FirstOrDefault(x => String.IsNullOrEmpty(x.ProductNames));

            //2.將items(購買的) 的每一個品項去掃描每一個conditions下的array(split過後的)
            //去檢查該品項是否隸屬 / 隸屬於某一個某一個condition group
            var checkConditionStatus = orders.Select(x =>
            {
                var mapping = temp_conditions.FirstOrDefault(condition => condition.ProductNames.Contains(x.Name));
                if (mapping == null)
                {
                    if (checkConditionItem != null)
                    {
                        return new ConditionMapping { ConditionID = -1, OrderName = x.Name, OrderQty = int.Parse(x.Count), OrderPrice = int.Parse(x.Price), ConditionMinPrice = checkConditionItem.MinPrice };
                    }
                    return null;
                }
                return new ConditionMapping { ConditionID = mapping.ConditionID, OrderName = x.Name, OrderQty = int.Parse(x.Count), ConditionQty = mapping.ConditionQty, OrderPrice = int.Parse(x.Price), ConditionMinPrice = mapping.MinPrice };
            }).Where(x => x != null).ToList();


            var conditionGroup = checkConditionStatus.GroupBy(x => new { x.ConditionID, x.ConditionMinPrice }).Select(x =>
            {

                var TotalOrderPrice = x.Sum(y => y.OrderPrice * y.OrderQty);
                if (TotalOrderPrice < x.Key.ConditionMinPrice)
                    return 0;
                return TotalOrderPrice;
            }).Where(x => x > 0).ToList();

            if (conditionGroup.Count != discount.Conditions.Length)
                return;

            var SumTotal = conditionGroup.Sum();

            var temp_Awards = discount.Awards.First();

            var TotalDisPrice = (int)(temp_Awards.Off * SumTotal);

            orders.Add(new Item("(折扣)", TotalDisPrice.ToString(), "1"));
        }
    }
}
