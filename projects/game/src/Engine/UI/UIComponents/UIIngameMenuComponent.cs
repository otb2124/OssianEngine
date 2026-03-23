using Myra.Graphics2D.UI;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIIngameMenuComponent : UIComponent
    {

        public UIIngameMenuComponent()
        { 
            SetTemplate(UITemplates.INGAME);
        }


        public override void Init()
        {
            var btnPlay = UI.UIManager.UIDesktop.FindById("btnPlay") as TextButton;
            var btnQuit = UI.UIManager.UIDesktop.FindById("btnQuit") as TextButton;

            //btnPlay.Click += (s, e) => GameStateManager.SetState(GameState.Playing);
            btnQuit.Click += (s, e) => Console.WriteLine("quit");

            base.Init();
        }
    }
}
