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
        AStrategy AStrategy;
        public StrategyContext(MenuSpec.Discount discountType, List<Item> orders)
        {
            Type type = Type.GetType(discountType.Strategy); //找到類別
            AStrategy = (AStrategy)Activator.CreateInstance(type, new object[] { discountType, orders });

        }
        public void ContextInterface()
        {
            AStrategy.DiscountSrategy();
        }
    }
}
