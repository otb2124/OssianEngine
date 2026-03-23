using Microsoft.Xna.Framework.Audio;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{

    public enum UITemplates
    {
        NONE,
        INGAME,
    };


    public class UITemplate
    {
        public string Path;
        public Project Project;

        public UITemplate(UITemplates type)
        {
            Path = GetPath(type);
            Load();
        }

        public string GetPath(UITemplates key)
        {
            return PathMap[key];
        }

        public void Load()
        {
            var xml = File.ReadAllText(ResourceLoader.GLOBAL_RES_PATH + "uitemplates/" + Path + ".xml");
            Project = Project.LoadFromXml(xml);
        }


        public static Dictionary<UITemplates, string> PathMap = new Dictionary<UITemplates, string>()
        {
                { UITemplates.NONE,          "none" },
                { UITemplates.INGAME,        "ingame" },
        };
    }
}
