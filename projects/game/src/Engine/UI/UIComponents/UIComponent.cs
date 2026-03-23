using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIComponent
    {

        public readonly string EMPTY_TEMPLATE_STRING = "<Label Text=\"NO TEMPLATE\"/>";
        public UITemplate Template;

        public UIComponent() 
        {

        }

        public void SetTemplate(UITemplates template)
        {
            Template = ResourceLoader.uiTemplates[template];
        }

        public virtual void Init()
        {

        }
    }
}
