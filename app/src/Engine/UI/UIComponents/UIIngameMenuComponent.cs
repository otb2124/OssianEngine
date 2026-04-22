using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using Resources;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace UI
{
    public class UIIngameMenuComponent : UIComponent
    {

        public UIIngameMenuComponent()
        { 
            SetTemplate(UITemplates.INGAME);
        }

        public static readonly Type[] IngameMenuOptions =
        {
            typeof(UIEquipmentComponent), typeof(UIInventoryComponent),
            typeof(UISkillsComponent),
            typeof(UIQuestbookComponent),
            typeof(UIStatsComponent),
            typeof(UISettingsComponent),
            typeof(UIQuitComponent),
        };


        public override void Init()
        {
            UI.UIManager.UIDesktop.SetButtonImage("btnContinue",  new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64*0, 0, 64, 64)));
            UI.UIManager.UIDesktop.SetButtonImage("btnInventory", new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64*1, 0, 64, 64)));
            UI.UIManager.UIDesktop.SetButtonImage("btnSkills",    new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64*2, 0, 64, 64)));
            UI.UIManager.UIDesktop.SetButtonImage("btnQuestbook", new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64*3, 0, 64, 64)));
            UI.UIManager.UIDesktop.SetButtonImage("btnStats",     new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64*4, 0, 64, 64)));
            UI.UIManager.UIDesktop.SetButtonImage("btnSettings",  new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64*5, 0, 64, 64)));
            UI.UIManager.UIDesktop.SetButtonImage("btnQuit",      new StaticSprite(SpriteSheets.UI_ICONS, new Rectangle(64*6, 0, 64, 64)));

            var btnContinue = UI.UIManager.UIDesktop.FindById("btnContinue") as ImageButton;
            var btnInventory = UI.UIManager.UIDesktop.FindById("btnInventory") as ImageButton;
            var btnSkills = UI.UIManager.UIDesktop.FindById("btnSkills") as ImageButton;
            var btnQuestbook = UI.UIManager.UIDesktop.FindById("btnQuestbook") as ImageButton;
            var btnStats = UI.UIManager.UIDesktop.FindById("btnStats") as ImageButton;
            var btnSettings = UI.UIManager.UIDesktop.FindById("btnSettings") as ImageButton;
            var btnQuit = UI.UIManager.UIDesktop.FindById("btnQuit") as ImageButton;

            btnContinue.Click += (s, e) =>  UI.UIManager.ExecuteAction("ingame.continue");
            btnInventory.Click += (s, e) => UI.UIManager.ExecuteAction("ingame.inventory");
            btnSkills.Click += (s, e) =>    UI.UIManager.ExecuteAction("ingame.skills");
            btnQuestbook.Click += (s, e) => UI.UIManager.ExecuteAction("ingame.questbook");
            btnStats.Click += (s, e) =>     UI.UIManager.ExecuteAction("ingame.stats");
            btnSettings.Click += (s, e) =>  UI.UIManager.ExecuteAction("ingame.settings");
            btnQuit.Click += (s, e) =>      UI.UIManager.ExecuteAction("ingame.quit");

            base.Init();
        }

        public static bool AnyOptionOpen()
        {
            foreach (var type in IngameMenuOptions)
                if (UI.UIManager.UIDesktop.HasComponent(type)) return true;
            return false;
        }

        public static void RemoveAllOptions()
        {
            foreach (Type type in IngameMenuOptions)
                UI.UIManager.UIDesktop.RemoveComponent(type);
        }

        public static void ToggleIngameMenu()
        {
            UI.UIManager.UIDesktop.ToggleComponent(typeof(UIIngameMenuComponent));
            RemoveAllOptions();
        }

        public static void OnIngameMenuButtonPressed()
        {
            if (AnyOptionOpen())
                RemoveAllOptions();
            else
                ToggleIngameMenu();
        }

        public static void OnIngameMenuOptionButtonPressed(params Type[] types)
        {
            if (types == null || types.Length == 0)
                return;

            if (types.Any(t => UI.UIManager.UIDesktop.HasComponent(t)))
            {
                RemoveAllOptions();
                return;
            }

            RemoveAllOptions();
            foreach (Type item in types)
            {
                UI.UIManager.UIDesktop.AddComponent(
                    (UIComponent)Activator.CreateInstance(item)
                );
            }
            
        }
    }
}
