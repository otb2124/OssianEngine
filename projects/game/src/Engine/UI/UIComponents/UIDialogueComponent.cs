using Myra.Graphics2D.UI;
using Resources;

namespace UI
{
    public class UIDialogueComponent : UIComponent
    {
        public UIDialogueComponent()
        {
            SetTemplate(UITemplates.DIALOGUE);
        }

        public override void Init()
        {
            base.Init();
        }
    }
}