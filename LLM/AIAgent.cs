using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static POS點餐機.LLM.AIRequest;

namespace POS點餐機.LLM
{
    internal class AIAgent
    {
        AIRequest aIRequest { get; set; } = new AIRequest();
        public AIAgent(string description)
        {
            AddPrompt("model", description);

            //用LINQ反找 types內的30個類別，只找出Declaration 的類別，並想辦法創建(new)出來，然後添加到tools.functionDeclarations 裡面
            var types = Assembly.GetExecutingAssembly().DefinedTypes;
            var declarations = types.Where(t => t.BaseType == typeof(AFunctiondeclaration))
                                    .Select(x => (AFunctiondeclaration)Activator.CreateInstance(x))
                                    .ToList();
            aIRequest.AddTools(declarations);
        }


        public void AddPrompt(string roleName, string roleInput)
        {
            //使用者輸入
            aIRequest.contents.Add(new Content(roleName, roleInput));

        }

        public async Task<AIResult> GetResult()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent");
            request.Headers.Add("x-goog-api-key", ConfigurationManager.AppSettings["apiKey"]);

            //model回答
            string reqString = JsonConvert.SerializeObject(aIRequest);
            var content = new StringContent(reqString);
            request.Content = content;
            var response = await client.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            AIResponse res = JsonConvert.DeserializeObject<AIResponse>(responseText);
            AIResult aiResult = new AIResult(res);
            string meg = aiResult.CanExcuteTool ? "好的，已經按照您的要求完成指示，請問接下來還有甚麼需要幫忙的嗎?" : res.candidates[0].content.parts[0].text;
            AddPrompt("model", meg);

            return aiResult;
        }
    }
}
