using POS點餐機.DiscountFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountStrategy
{
    internal class StrategyContext
    {
        ADiscount ADiscount;
        Type type;
        ADiscount getDiscount;
        public StrategyContext(String discountTypeName, List<Item> orders)
        {
            type = Type.GetType(discountTypeName); //找到類別
            ADiscount = (ADiscount)Activator.CreateInstance(type, new object[] { orders });

        }
        public void ContextInterface()
        {
            ADiscount.DiscountOrder();
        }
    }
}
