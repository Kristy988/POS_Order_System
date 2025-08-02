using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機
{
    internal class Item
    {
        public String Name { get; set; }
        public String Price { get; set; }
        public String Count { get; set; }
        public String SubTotal
        {
            get
            {
                return (int.Parse(Price) * int.Parse(Count)).ToString();
            }
        }

        public Item(string name, string price, string count)
        {
            Name = name;
            Price = price;
            Count = count;

        }
    }
}
