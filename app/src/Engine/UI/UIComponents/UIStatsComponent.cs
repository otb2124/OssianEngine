using Myra.Graphics2D.UI;
using Resources;

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
            var btnClose = UI.UIManager.UIDesktop.FindById("btnCloseStats") as TextButton;
            if (btnClose != null)
                btnClose.TouchUp += (s, e) => UI.UIManager.ExecuteAction("ingame.stats");

            base.Init();
        }

        // call this to update all values from your player data
        public void Refresh(/* PlayerStats stats */)
        {
            SetLabel("lblName", "Orest");
            SetLabel("lblClass", "Warrior");
            SetLabel("lblLevel", "12");
            SetLabel("lblExp", "4200 / 6000");
            SetLabel("lblStr", "42");
            SetLabel("lblDex", "28");
            SetLabel("lblInt", "15");
            SetLabel("lblWil", "20");
            SetLabel("lblVit", "60");
            SetLabel("lblEnd", "45");
            SetLabel("lblAgi", "33");
            SetLabel("lblCha", "10");
            SetLabel("lblAtk", "78");
            SetLabel("lblDef", "34");
            SetLabel("lblCrit", "12%");
            SetLabel("lblGold", "1,240");
            SetLabel("lblCarry", "42 / 80");

            var xpBar = UI.UIManager.UIDesktop.FindById("xpBar") as HorizontalProgressBar;
            if (xpBar != null) xpBar.Value = 70;
        }

        private void SetLabel(string id, string text)
        {
            var lbl = UI.UIManager.UIDesktop.FindById(id) as Label;
            if (lbl != null) lbl.Text = text;
        }
    }
}