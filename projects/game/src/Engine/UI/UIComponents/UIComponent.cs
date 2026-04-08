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
        public UITemplateResource Template;

        public UIComponent() { }

        public virtual void Init() { }

        public void SetTemplate(UITemplates template)
        {
            Template = ResourceLoader.UITemplateResources[template];
        }

        public void ReloadTemplate()
        {
            Template.Load();
        }

        public virtual void Update() { }
    }
}
