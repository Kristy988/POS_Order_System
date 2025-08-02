using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.LLM
{
    internal class AIRequest
    {
        public List<Content> contents { get; set; } = new List<Content>();
        public List<Tool> tools { get; set; } = new List<Tool> { new Tool() };

        public class Content
        {

            public string role { get; set; }
            public List<Part> parts { get; set; } = new List<Part>();

            public Content(string role, String partsText)
            {
                this.role = role;
                parts.Add(new Part(partsText));
            }
        }

        public class Part
        {
            public string text { get; set; }
            public Part(string text)
            {
                this.text = text;
            }
        }
        public void AddTools(List<AFunctiondeclaration> toolName)
        {
            tools[0].functionDeclarations.AddRange(toolName);
        }
        public class Tool
        {
            public List<AFunctiondeclaration> functionDeclarations { get; set; } = new List<AFunctiondeclaration>();
        }

        public abstract class AFunctiondeclaration
        {
            public abstract string name { get; }
            public abstract string description { get; }
            public abstract AParameters parameters { get; }

        }

        public abstract class AParameters
        {
            public string type { get; set; } = "object";
            public abstract object properties { get; }
            public abstract string[] required { get; }
        }

        public class PropertyDetail
        {
            public string type { get; set; }
            public string[] @enum { get; set; }
            public string description { get; set; }
            public ItemType items { get; set; }
        }

        public class ItemType
        {
            public string type { get; set; }
        }
    }
}
