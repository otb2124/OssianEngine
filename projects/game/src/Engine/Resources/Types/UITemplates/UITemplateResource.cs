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
        PANEL,
        INGAME,
        INVENTORY,
        EQUIPMENT,
        SKILLS,
        QUESTBOOK,
        STATS,
        SETTINGS,
        QUIT,
        HUD,
        TOOLTIP,
        DIALOGUE,
    };


    public class UITemplateResource
    {
        public string Path;
        public Project Project;

        public UITemplateResource(UITemplates type)
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
            var xml = File.ReadAllText(ResourceLoader.GLOBAL_RES_PATH + "ui/templates/" + Path + ".xml");
            Project = Project.LoadFromXml(xml);
        }


        public static Dictionary<UITemplates, string> PathMap = new Dictionary<UITemplates, string>()
        {
                { UITemplates.NONE,          "none" },
                { UITemplates.INGAME,        "ingame" },
                { UITemplates.INVENTORY,     "inventory" },
                { UITemplates.EQUIPMENT,     "equipment" },
                { UITemplates.SKILLS,        "skills" },
                { UITemplates.QUESTBOOK,     "questbook" },
                { UITemplates.STATS,         "stats" },
                { UITemplates.SETTINGS,      "settings" },
                { UITemplates.QUIT,          "quit" },
                { UITemplates.HUD,           "hud" },
                { UITemplates.PANEL,         "panel"},
                { UITemplates.TOOLTIP,       "tooltip"},
                { UITemplates.DIALOGUE,      "dialogue"}
        };
    }
}
