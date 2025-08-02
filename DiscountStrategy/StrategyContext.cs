using Newtonsoft.Json;
using POS點餐機.DiscountFolder;
using POS點餐機.LLM;
using POS點餐機.Models;
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
        OrderRequestModel orderRequestModel;
        public StrategyContext(OrderRequestModel orderRequestModel)
        {
            this.orderRequestModel = orderRequestModel;

        }
        public async Task<AIResponse.Args> ContextInterface()
        {

            if (!orderRequestModel.EnableAISuggestion)
            {
                Type type = Type.GetType(orderRequestModel.DiscountType.Strategy); //找到類別
                AStrategy = (AStrategy)Activator.CreateInstance(type, new object[] { orderRequestModel.DiscountType, orderRequestModel.Orders });
                AStrategy.DiscountSrategy();
                return null;
            }
            else
            {

                AIResponse.Args agentResponse = await RunAiAgent(orderRequestModel);
                if (agentResponse == null)
                {
                    return null;
                }
                Type type = Type.GetType(agentResponse.strategyName); //找到類別
                MenuSpec.Discount agentSuggestion = AppData.Discounts.First(discountType => discountType.Name == agentResponse.discountName);
                AStrategy = (AStrategy)Activator.CreateInstance(type, new object[] { agentSuggestion, orderRequestModel.Orders });
                AStrategy.DiscountSrategy();
                return agentResponse;
            }
        }

        public static async Task<AIResponse.Args> RunAiAgent(OrderRequestModel orderRequestModel)
        {

            AIAgent aIAgent = new AIAgent("作為一個AI智慧推薦折扣，你會需要根據你現有的工具協助使用者解決問題，請注意你只能回答有關你工具內容的問題並幫他們執行，以外的內容都不允許回答。");
            aIAgent.AddPrompt("model", "以下是菜單和擁有的折扣:" + JsonConvert.SerializeObject(AppData.MenuContent));
            aIAgent.AddPrompt("user", string.Join(",", orderRequestModel.Orders.Select(x => $"{x.Name} {x.Count}份")));
            AIResult aIResult = await aIAgent.GetResult();

            if (!aIResult.CanExcuteTool)
            {
                Console.WriteLine(aIResult.ResponseText);
                return null;
            }
            else
            {
                return aIResult.RunTool();
            }
        }
    }
}
