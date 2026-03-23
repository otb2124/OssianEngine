using Myra.Graphics2D.UI;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;


namespace UI
{
    public class UIQuitComponent : UIComponent
    {

        public UIQuitComponent()
        {
            SetTemplate(UITemplates.QUIT);
        }


        public override void Init()
        {
            var btnConfirm = UI.UIManager.UIDesktop.FindById("btnQuitConfirm") as TextButton;
            var btnCancel = UI.UIManager.UIDesktop.FindById("btnQuitCancel") as TextButton;

            btnConfirm.TouchUp += (s, e) => UI.UIManager.ExecuteAction("ingame.quit.exit");
            btnCancel.TouchUp += (s, e) => UI.UIManager.ExecuteAction("ingame.quit");
        }
    }
}
