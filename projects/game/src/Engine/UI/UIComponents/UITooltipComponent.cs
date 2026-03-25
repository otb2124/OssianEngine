using Myra.Graphics2D.UI;
using Resources;

namespace UI
{
    public class UITooltipComponent : UIComponent
    {
        public UITooltipComponent()
        {
            SetTemplate(UITemplates.TOOLTIP);
        }

        public override void Init()
        {
            base.Init();
        }

        public void SetLabel(string id, string text)
        {
            var lbl = UI.UIManager.UIDesktop.FindById(id) as Label;
            if (lbl != null) lbl.Text = text;
        }
    }
}