using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS點餐機.Models
{
    public class RenderDataModel
    {
        public string SuggestionDiscount = "";
        public string SuggestionReason = "";
        public FlowLayoutPanel OrderDetail;
        public string CheckoutPrice;
        public RenderDataModel(FlowLayoutPanel OrderDetail, string CheckoutPrice)
        {
            this.OrderDetail = OrderDetail;
            this.CheckoutPrice = CheckoutPrice;
        }
    }
}
