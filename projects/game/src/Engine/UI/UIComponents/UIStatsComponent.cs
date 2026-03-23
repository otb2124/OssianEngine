using Myra.Graphics2D.UI;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIStatsComponent : UIComponent
    {

        public UIStatsComponent()
        {
            SetTemplate(UITemplates.STATS);
        }


        public override void Init()
        {


            //var btnClose = UI.UIManager.UIDesktop.FindById("btnCloseQuestBook") as TextButton;
            //btnClose.TouchUp += (s, e) => Visible = false;

            base.Init();
        }
    }
}
