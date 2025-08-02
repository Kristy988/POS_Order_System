using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.Models
{
    internal class OrderRequestModel
    {
        public MenuSpec.Discount DiscountType { get; set; }
        public List<Item> Orders { get; set; }
        public Item OrderItem { get; set; }
        public bool EnableAISuggestion { get; set; }


        public OrderRequestModel(MenuSpec.Discount DiscountType, Item OrderItem)
        {
            EnableAISuggestion = false;
            this.DiscountType = DiscountType;
            this.OrderItem = OrderItem;
        }
        public OrderRequestModel(bool EnableAISuggestion)
        {
            this.EnableAISuggestion = EnableAISuggestion;
        }
        public OrderRequestModel(MenuSpec.Discount DiscountType)
        {
            this.DiscountType = DiscountType;
        }
    }
}
