using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static POS點餐機.MenuSpec;

namespace POS點餐機.DiscountStrategy
{
    class ConditionMapping
    {
        // condition ID; order count; order name; condition count 
        public int ConditionID { get; set; }
        public String OrderName { get; set; }

        public int OrderQty { get; set; }
        public int ConditionQty { get; set; }
        public int OrderPrice { get; set; }
    }

    class AwardMapping
    {
        // condition ID; order count; order name; condition count 
        public int AwardID { get; set; }
        //public String OrderName { get; set; }

        //public int OrderQty { get; set; }
        public int AwardQty { get; set; }

    }


    internal class Combo_Free_Item : AStrategy
    {
        public Combo_Free_Item(MenuSpec.Discount discount, List<Item> orders) : base(discount, orders)
        {
        }

        public override void DiscountSrategy()
        {
            Condition[] conditions = discount.Conditions;
            Award[] awards = discount.Awards;
            Dictionary<int, List<ConditionMapping>> mappingDict = new Dictionary<int, List<ConditionMapping>>();
            int minFreeCount = int.MaxValue;
            // 將購買的每一個品項歸屬到對應的Condition中
            for (int i = 0; i < orders.Count; i++)
            {
                for (int j = 0; j < conditions.Length; j++)
                {
                    string[] tempNames = conditions[j].Item.Split(',');
                    for (int k = 0; k < tempNames.Length; k++)
                    {
                        if (tempNames[k] == orders[i].Name)
                        {
                            ConditionMapping conditionMapping = new ConditionMapping();
                            conditionMapping.ConditionID = j;
                            conditionMapping.ConditionQty = conditions[j].Count;
                            conditionMapping.OrderQty = int.Parse(orders[i].Count);
                            conditionMapping.OrderName = orders[i].Name;
                            conditionMapping.OrderPrice = int.Parse(orders[i].Price);

                            if (!mappingDict.ContainsKey(j))
                            {
                                mappingDict.Add(j, new List<ConditionMapping>() { conditionMapping });
                            }
                            else
                            {
                                mappingDict[j].Add(conditionMapping);
                            }

                        }
                    }
                }
            }


            // 檢查每一個的ConditionGroup的條件是否與ConditionQty的數量一致
            if (mappingDict.Count != discount.Conditions.Length)
                return;

            for (int i = 0; i < mappingDict.Count; i++)
            {
                int qtyRequired = 0;
                int qty = 0;
                for (int j = 0; j < mappingDict[i].Count; j++)
                {
                    qtyRequired = mappingDict[i][j].ConditionQty;
                    qty += mappingDict[i][j].OrderQty;
                }

                if (qty < qtyRequired)
                {
                    return;
                }
                int freeCount = qty / qtyRequired;
                if (freeCount < minFreeCount)
                {
                    minFreeCount = freeCount;
                }

            }

            if (minFreeCount == 0 || minFreeCount == int.MaxValue)
            {
                return;
            }

            //Dictionary<int, List<ConditionMapping>> mappingAwardDict = new Dictionary<int, List<ConditionMapping>>();
            string awardItem = "";
            int minPrice = int.MaxValue;
            int maxPrice = int.MinValue;
            List<string> awardItems = new List<string>();

            for (int i = 0; i < awards.Length; i++)
            {
                string[] tempNames = awards[i].Item.Split(',');

                for (int j = 0; j < mappingDict.Count; j++)
                {
                    int price = 0;
                    for (int k = 0; k < mappingDict[i].Count; k++)
                    {
                        price = mappingDict[j][k].OrderPrice;
                        for (int q = 0; q < tempNames.Length; q++)
                        {
                            if (tempNames[q] == mappingDict[j][k].OrderName)
                            {
                                awardItems.Add(tempNames[i]);
                                if (awards[i].AwardType == "Min")
                                {
                                    if (price < minPrice)
                                    {
                                        minPrice = price;
                                        awardItem = mappingDict[j][k].OrderName;
                                    }
                                }
                                else if (awards[i].AwardType == "Max")
                                {
                                    if (price > maxPrice)
                                    {
                                        maxPrice = price;
                                        awardItem = mappingDict[j][k].OrderName;
                                    }
                                }
                                else if (awards[i].AwardType == "Random")
                                {
                                    Random random = new Random(Guid.NewGuid().GetHashCode());
                                    int randomAward = random.Next(0, awardItems.Count + 1);
                                    awardItem = awardItems[randomAward];
                                }
                            }
                        }

                    }

                }

            }






            for (int i = 0; i < discount.Awards.Length; i++)
            {
                int addFreeCount = discount.Awards[i].Count * minFreeCount;
                orders.Add(new Item("(贈送)" + discount.Awards[i].Item, "0", addFreeCount.ToString()));

            }


        }
    }
}
