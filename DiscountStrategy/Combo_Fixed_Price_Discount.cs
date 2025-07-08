using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace POS點餐機.DiscountStrategy
{

    internal class Combo_Fixed_Price_Discount : AStrategy
    {
        public Combo_Fixed_Price_Discount(MenuSpec.Discount discount, List<Item> orders) : base(discount, orders)
        {

        }

        public override void DiscountSrategy()
        {
            var temp_conditions = discount.Conditions.SelectMany((x, index) => x.Item.Split(',').Select((y) => new
            {
                ConditionID = index,
                ConditionQty = x.Count,
                ProductNames = y

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

            //3.將符合條件的item項目 歸屬到指定的 condition group(這包就是要自己創建List)
            //4.將List下的每一個condition進行數量加總
            var conditionGroup = checkConditionStatus.GroupBy(x => new { x.ConditionID, x.ConditionQty }).Select(x => new
            {

                TotalQty = x.Sum(y => y.OrderQty),
                RequiredQty = x.Key.ConditionQty,

            }).Where(x => (x.TotalQty / x.RequiredQty) > 0).ToList();

            //5.比對每一群組的數量 跟原本的conditions count 是否一致
            if (conditionGroup.Count != discount.Conditions.Length)
                return;
            int freeCount = conditionGroup.Min(x => x.TotalQty / x.RequiredQty);




            var temp_Awards = discount.Awards.SelectMany((x, index) => x.Item.Split(',').Select(y => new
            {
                AwardProduct = y,
                AwardType = x.AwardType,
                AwardQty = x.Count,
                AwardID = index,
                AwardDisPrice = x.DisPrice,
                SetPrice = x.SetPrice

            })).ToList();

            var allocatingAwards = temp_Awards.Select(x =>
            {
                if (x.AwardType == "")

                {
                    if (x.SetPrice != 0)
                    {
                        //10 take(4) => 4 
                        //Enumerable.Repeat(a,b):根據b的數量重複a的內容

                        var AwardSetPrice = checkConditionStatus
                                            //根據ConditionID與ConditionQty進行群組分類(GroupBy)checkConditionStatus，並操作每一群組內的資料(Select)
                                            .GroupBy(condition => new { condition.ConditionID, condition.ConditionQty })
                                            //根據數量攤平(SelectMany)物件(item)後，依照價格升冪排列(OrderBy)，取需要的數量(Take)，進行加總(Sum)
                                            .Select(group => group.SelectMany(items => Enumerable.Repeat(items.OrderPrice, items.OrderQty))
                                                                  .OrderBy(order => order)
                                                                  .Take(group.Key.ConditionQty * freeCount)
                                                                  .Sum()
                                            )
                                            .Sum(); //將群組的內的資料加總

                        return new AwardMapping { AwardDisPrice = x.SetPrice * freeCount - AwardSetPrice, AwardQty = x.AwardQty, AwardName = "" };
                    }
                    return new AwardMapping { AwardDisPrice = x.AwardDisPrice, AwardQty = x.AwardQty * freeCount, AwardName = "" };
                }



                if (x.AwardType == "SamePrice")
                {
                    var AwardItem = orders.FirstOrDefault(orderItem => orderItem.Name == x.AwardProduct);
                    if (AwardItem == null)
                        return null;
                    var ItemPrice = AppData.Products[AwardItem.Name];
                    return new AwardMapping { AwardDisPrice = x.AwardDisPrice - ItemPrice, AwardQty = int.Parse(AwardItem.Count), AwardName = AwardItem.Name };
                }

                else
                    return null;
            }).Where(x => x != null).ToList();

            var addOrder = allocatingAwards.Select(x =>
            {

                return new Item("(折扣)" + x.AwardName, x.AwardDisPrice.ToString(), x.AwardQty.ToString());

            }).ToList();

            orders.AddRange(addOrder);





        }
    }
}
