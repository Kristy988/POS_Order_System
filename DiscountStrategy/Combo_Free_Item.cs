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

        public int ConditionMinPrice { get; set; }
    }

    class AwardMapping
    {
        public int AwardID { get; set; }
        public int SetPrice { get; set; }
        public string AwardName { get; set; }
        public int AwardQty { get; set; }
        public string AwardType { get; set; }
        public int AwardDisPrice { get; set; }
        public float AwardOff { get; set; }

    }


    internal class Combo_Free_Item : AStrategy
    {
        public Combo_Free_Item(MenuSpec.Discount discount, List<Item> orders) : base(discount, orders)
        {
        }

        public override void DiscountSrategy()
        {
            //select (將每一筆資料 轉換成另一種資料類型)
            //查詢所購買的品名
            //List<string> names = orders.Select(x =>
            //{
            //    if (x.Name.Contains("奶") && int.Parse(x.Price) > 50)
            //        return x.Name;
            //    else
            //    {
            //        return "";
            //    }

            //}).Where(x => x != "").ToList();

            //List<string> names = orders.Where(x =>
            //{
            //    if (x.Name.Contains("奶") && int.Parse(x.Price) > 50)
            //        return true;
            //    else
            //    {
            //        return false;
            //    }

            //}).Select(x => x.Name).ToList();

            // 匿名類別 => 創建一個臨時的類別來儲存資料
            //var list = orders.Select((x, index) => new
            //{
            //    ProductName = x.Name,
            //    x.Count,
            //    NumberID = index
            //}).ToList();


            //selectMany

            //List<List<int>> numbers = new List<List<int>>()
            //{
            //    new List<int> { 1,2,3 },
            //    new List<int> { 4,5,6 },
            //    new List<int> { 7,8,9 },
            //};

            //var result = numbers.SelectMany(x => x).ToList();


            //group by => 資料群組 用來計算各類數值(max min sum count avg,聚合函數)








            //1.需要先將Conditions內的每一筆condition 都給予一個 conditionID 作為群組編號
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
                AwardID = index

            })).ToList();

            // AnyMin Min Max Random
            var allocatingAwards = temp_Awards.GroupBy(x => new { x.AwardID, x.AwardType }).Select(x =>
          {
              if (x.Key.AwardType == "AnyMin")
              {
                  var AwardItem = x.OrderBy(y => AppData.Products[y.AwardProduct]).First();
                  return new AwardMapping { AwardQty = AwardItem.AwardQty, AwardName = AwardItem.AwardProduct };
              }
              if (x.Key.AwardType == "Min")
              {
                  var AwardItem = x.OrderBy(y =>
                  {
                      if (!orders.Any(order => order.Name == y.AwardProduct))
                          return int.MaxValue;
                      return AppData.Products[y.AwardProduct];
                  }).First();
                  return new AwardMapping { AwardQty = AwardItem.AwardQty, AwardName = AwardItem.AwardProduct };
              }
              if (x.Key.AwardType == "Max")
              {
                  var AwardItem = x.OrderByDescending(y =>
                  {
                      if (!orders.Any(order => order.Name == y.AwardProduct))
                          return int.MinValue;
                      return AppData.Products[y.AwardProduct];
                  }).First();
                  return new AwardMapping { AwardQty = AwardItem.AwardQty, AwardName = AwardItem.AwardProduct };
              }
              if (x.Key.AwardType == "Random")
              {
                  var AwardItem = x.OrderBy(y =>
                  {
                      if (!orders.Any(order => order.Name == y.AwardProduct))
                          return int.MaxValue;
                      return Guid.NewGuid().GetHashCode();
                  }).First();
                  return new AwardMapping { AwardQty = AwardItem.AwardQty, AwardName = AwardItem.AwardProduct };
              }

              else
                  return null;
          }).Where(x => x != null).ToList();

            var addOrder = allocatingAwards.Select(x =>
            {
                var addFreeCount = x.AwardQty * freeCount;
                return new Item("(贈送)" + x.AwardName, "0", addFreeCount.ToString());

            }).ToList();

            orders.AddRange(addOrder);


        }
    }
}
