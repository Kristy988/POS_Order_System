using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS點餐機.LLM.AIRequest;

namespace POS點餐機.LLM.Tools.DiscountSuggestionTool
{
    internal class DiscountParameter : AParameters
    {
        public override object properties => new
        {
            strategyName = new PropertyDetail()
            {
                type = "string",
                description = "該參數代表折扣使用的策略"
            },
            discountName = new PropertyDetail()
            {
                type = "string",
                description = "該參數代表折扣的名稱"
            },
            reason = new PropertyDetail()
            {
                type = "string",
                description = "該參數代表使用該折扣的原因"
            }

        };

        public override string[] required => new string[] { "strategyName", "discountName", "reason" };
    }
}
