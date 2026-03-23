using Entities;
using Myra.Graphics2D.UI;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIHUDComponent : UIComponent
    {

        public UIHUDComponent()
        {
            SetTemplate(UITemplates.HUD);
        }


        public override void Init()
        {
            var healthBar = UI.UIManager.UIDesktop.FindById("healthBar") as HorizontalProgressBar;
            var lblScore = UI.UIManager.UIDesktop.FindById("lblScore") as Label;
            var bossPanel = UI.UIManager.UIDesktop.FindById("bossPanel") as VerticalStackPanel;

            healthBar.Value = 100;
            lblScore.Text = $"Score: {100}";
            bossPanel.Visible = false;

            base.Init();
        }
    }
}
