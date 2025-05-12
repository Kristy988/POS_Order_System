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
        public static event EventHandler<(FlowLayoutPanel, string)> showPanelHander;

        public static void NotifyShowPanel((FlowLayoutPanel, string) response)
        {
            showPanelHander?.Invoke(null, response);
        }

    }
}
