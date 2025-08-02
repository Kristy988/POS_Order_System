using POS點餐機.DiscountFolder;
using POS點餐機.DiscountStrategy;
using POS點餐機.LLM;
using POS點餐機.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機
{
    internal class Discount
    {
        public static async Task DiscountOrder(OrderRequestModel orderRequestModel)
        {
            orderRequestModel.Orders.RemoveAll(x => x.Name.Contains("贈送") || x.Name.Contains("折扣"));

            StrategyContext strategyContext = new StrategyContext(orderRequestModel);
            AIResponse.Args agentResponse = await strategyContext.ContextInterface();
            ShowPanel.Show(orderRequestModel.Orders, agentResponse);

        }


    }
}