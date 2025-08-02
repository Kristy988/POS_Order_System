using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.LLM.Tools
{
    internal abstract class ATools
    {
        public abstract AIResponse.Args Apply(AIResponse.Args args);

    }
}
