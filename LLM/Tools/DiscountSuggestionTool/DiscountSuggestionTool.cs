using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.LLM.Tools.DiscountSuggestionTool
{
    internal class DiscountSuggestionTool : ATools
    {
        public override AIResponse.Args Apply(AIResponse.Args args)
        {
            Console.WriteLine($"推薦您使用：{args.discountName}");
            Console.WriteLine($"推薦原因：{args.reason}");
            return args;
        }
    }
}
