using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountStrategy
{
    internal class Combo_Percentage_Discount : AStrategy
    {
        public Combo_Percentage_Discount(MenuSpec.Discount discount, List<Item> orders) : base(discount, orders)
        {
        }

        public override void DiscountSrategy()
        {
            var temp_conditions = discount.Conditions.SelectMany((x, index) => x.Item.Split(',').Select((y) => new
            {
                ConditionID = index,
                ConditionQty = x.Count,
                ProductNames = y,

            })).ToList();

            //2.將items(購買的) 的每一個品項去掃描每一個conditions下的array(split過後的)
            //去檢查該品項是否隸屬 / 隸屬於某一個某一個condition group
            var checkConditionStatus = orders.Select(x =>
            {
                var mapping = temp_conditions.FirstOrDefault(condition => condition.ProductNames.Contains(x.Name));
                if (mapping == null)
                    return null;
                return new ConditionMapping { ConditionID = mapping.ConditionID, OrderName = x.Name, OrderQty = int.Parse(x.Count), ConditionQty = mapping.ConditionQty, OrderPrice = int.Parse(x.Price) };
            }).Where(x => x != null).ToList();


            var conditionGroup = checkConditionStatus.GroupBy(x => new { x.ConditionID, x.ConditionQty }).Select(x => new
            {

                TotalQty = x.Sum(y => y.OrderQty),
                RequiredQty = x.Key.ConditionQty,
                TotalPrice = x.Sum(y => y.OrderPrice * y.OrderQty)

            }).Where(x => (x.TotalQty / x.RequiredQty) > 0).ToList();

            if (conditionGroup.Count != discount.Conditions.Length)
                return;



            var temp_Awards = discount.Awards.First();

            var SumTotal = conditionGroup.Sum(x => x.TotalPrice);

            var TotalDisPrice = (int)(temp_Awards.Off * SumTotal);

            orders.Add(new Item("(折扣)", TotalDisPrice.ToString(), "1"));
        }
    }
}

