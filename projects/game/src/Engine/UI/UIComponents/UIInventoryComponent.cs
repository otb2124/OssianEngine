using Myra.Graphics2D.UI;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIInventoryComponent : UIComponent
    {

        public UIInventoryComponent()
        {
            SetTemplate(UITemplates.INVENTORY);
        }


        public override void Init()
        {


            //var btnClose = UI.UIManager.UIDesktop.FindById("btnCloseQuestBook") as TextButton;
            //btnClose.TouchUp += (s, e) => Visible = false;

            base.Init();
        }
    }
}
