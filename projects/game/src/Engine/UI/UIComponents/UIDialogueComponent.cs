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
            var textbox = UI.UIManager.UIDesktop.FindById("txtDescription") as TextBox;
            textbox.Readonly = true;
            base.Init();
        }
    }
}