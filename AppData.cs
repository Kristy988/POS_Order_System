using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機
{
    internal class AppData
    {
        public static MenuSpec.Menu[] Menus;
        public static MenuSpec.Discount[] Discounts;
        public static Dictionary<String, int> Products;

        
        static AppData()
        {
            string menuPath = ConfigurationManager.AppSettings["MenuPath"];
            string menuContent = File.ReadAllText(menuPath);
            //物件轉json字串 => 序列化 Serialize
            //json字串 轉物件 => 反序列化 DeSerialize
            MenuSpec menuSpec = JsonConvert.DeserializeObject<MenuSpec>(menuContent);

            Menus = menuSpec.Menus; 
            Discounts = menuSpec.Discounts;

            Products = Menus.SelectMany(x => x.Foods.Select(y => new
            {
                ProductName = y.Name,
                Price = int.Parse(y.Price)
            })).ToDictionary(x=> x.ProductName,x=>x.Price );

            
        }
         
    }
}
