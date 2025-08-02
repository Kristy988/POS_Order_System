using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機
{
    internal class MenuSpec
    {
        public Menu[] Menus { get; set; }
        public Discount[] Discounts { get; set; }

        public class Menu
        {
            public string Title { get; set; }
            public Food[] Foods { get; set; }
        }

        public class Food
        {
            public string Name { get; set; }
            public string Price { get; set; }
        }

        public class Discount
        {
            public string Name { get; set; }
            public string Strategy { get; set; }
            public Condition[] Conditions { get; set; }
            public Award[] Awards { get; set; }
        }

        public class Condition
        {
            public string Item { get; set; }
            public int Count { get; set; }
            public int MinPrice { get; set; }
        }

        public class Award
        {
            public string Item { get; set; }
            public int SetPrice { get; set; }
            public string AwardType { get; set; }
            public int Count { get; set; }
            public int DisPrice { get; set; }
            public float Off { get; set; }
        }

    }
}
