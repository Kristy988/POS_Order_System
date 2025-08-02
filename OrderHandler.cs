using POS點餐機.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS點餐機
{
    internal class OrderHandler
    {
        public static event EventHandler<RenderDataModel> showPanelHander;

        public static void NotifyShowPanel(RenderDataModel response)
        {
            showPanelHander?.Invoke(null, response);
        }

    }
}
