using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.LLM
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            AIAgent aIAgent = new AIAgent("作為一個AI助理管家，你會需要根據你現有的工具協助使用者解決問題，請注意你只能回答有關你工具內容的問題並幫他們執行，以外的內容都不允許回答。");
            Console.WriteLine("我是AI助理，請問需要什麼幫助嗎?");


            while (true)
            {
                string userInput = Console.ReadLine();
                aIAgent.AddPrompt("user", userInput);
                AIResult aIResult = await aIAgent.GetResult();

                if (!aIResult.CanExcuteTool)
                {
                    Console.WriteLine(aIResult.ResponseText);
                }
                else
                {
                    aIResult.RunTool();
                }

            }

            Console.ReadKey();
        }
    }
}
