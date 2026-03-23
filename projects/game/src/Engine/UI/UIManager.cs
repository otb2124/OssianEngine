using Myra;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace UI
{
    public class UIManager
    {

        public UIDesktop UIDesktop;
        public Dictionary<string, Action> Actions = new Dictionary<string, Action>();

        public UIManager() { }

        public void Init(Game game)
        {
            UIDesktop = new UIDesktop();
            UIDesktop.Init(game);
            RegisterInitialActions();
        }

        public void RegisterAction(string key, Action action)
        {
            Actions[key] = action;
        }

        public void ExecuteAction(string key)
        {
            if (Actions.TryGetValue(key, out var action))
                action?.Invoke();
            else
                System.Diagnostics.Debug.WriteLine($"UIManager: action '{key}' not found");
        }

        public void RegisterInitialActions()
        {
            RegisterAction("ingame.continue", () => UIIngameMenuComponent.ToggleIngameMenu());
            RegisterAction("ingame.inventory", () => UIIngameMenuComponent.OnIngameMenuOptionButtonPressed(typeof(UIInventoryComponent)));
            RegisterAction("ingame.skills", () => UIIngameMenuComponent.OnIngameMenuOptionButtonPressed(typeof(UISkillsComponent)));
            RegisterAction("ingame.questbook", () => UIIngameMenuComponent.OnIngameMenuOptionButtonPressed(typeof(UIQuestbookComponent)));
            RegisterAction("ingame.stats", () => UIIngameMenuComponent.OnIngameMenuOptionButtonPressed(typeof(UIStatsComponent)));
            RegisterAction("ingame.settings", () => UIIngameMenuComponent.OnIngameMenuOptionButtonPressed(typeof(UISettingsComponent)));
            RegisterAction("ingame.quit", () => UIIngameMenuComponent.OnIngameMenuOptionButtonPressed(typeof(UIQuitComponent)));
            RegisterAction("ingame.quit.exit", () => System.Environment.Exit(0));
        }

        public void Update()
        {
            if (Entities.Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.TOGGLEMENUPRESSED])
            {
                UIIngameMenuComponent.OnIngameMenuButtonPressed();
            }
        }

        public void Draw()
        {
            UIDesktop.Draw();
        }
    }
}