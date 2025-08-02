using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS點餐機.LLM.AIRequest;

namespace POS點餐機.LLM.Tools.DiscountSuggestionTool
{
    internal class DiscountDeclaration : AFunctiondeclaration
    {
        public override string name => "Tools.DiscountSuggestionTool.DiscountSuggestionTool";
        public override string description => "該函數根據菜單、折扣與使用者點餐的內容，分析最合適的方案，建議使用者使用該折扣點餐，並詳細說明原因";

        public override AParameters parameters => new DiscountParameter();
    }
}
