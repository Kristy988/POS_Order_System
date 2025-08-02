using POS點餐機.LLM.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS點餐機.LLM.AIRequest;

namespace POS點餐機.LLM
{
    internal class AIResult
    {
        AIResponse res { get; set; }
        public bool CanExcuteTool = true;
        public string ResponseText;
        public AIResult(AIResponse res)
        {
            this.res = res;
            if (res.candidates[0].content.parts[0].text != null)
            {
                CanExcuteTool = false;
                ResponseText = res.candidates[0].content.parts[0].text;

            }

        }

        public AIResponse.Args RunTool()
        {
            if (CanExcuteTool)
            {
                Type type = Type.GetType("POS點餐機.LLM." + res.candidates[0].content.parts[0].functionCall.name); //反射找到類別
                ATools theTool = (ATools)Activator.CreateInstance(type);
                return theTool.Apply(res.candidates[0].content.parts[0].functionCall.args);
            }
            return null;
        }
    }
}
