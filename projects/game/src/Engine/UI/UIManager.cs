using Myra;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using SharpDX.Direct2D1;

namespace UI
{
    public class UIManager
    {

        public UIDesktop UIDesktop;
        public Dictionary<string, Action<object[]>> Actions { get; } = new Dictionary<string, Action<object[]>>();

        public UIManager() { }

        public void Init(Game game)
        {
            UIDesktop = new UIDesktop();
            UIDesktop.Init(game);
            RegisterInitialActions();
        }

        public void RegisterAction(string key, Action<object[]> action)
        {
            Actions[key] = action;
        }

        public void ExecuteAction(string key, params object[] parameters)
        {
            if (Actions.TryGetValue(key, out var action))
            {
                try
                {
                    action?.Invoke(parameters ?? Array.Empty<object>());
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"UIManager: Error executing action '{key}': {ex.Message}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"UIManager: action '{key}' not found");
            }
        }

        public void RegisterInitialActions()
        {
            RegisterAction("ingame.continue", _ => UIIngameMenuComponent.ToggleIngameMenu());
            RegisterAction("ingame.inventory", _ => UIIngameMenuComponent.OnIngameMenuOptionButtonPressed(typeof(UIEquipmentComponent), typeof(UIInventoryComponent)));
            RegisterAction("ingame.skills", _ => UIIngameMenuComponent.OnIngameMenuOptionButtonPressed(typeof(UISkillsComponent)));
            RegisterAction("ingame.questbook", _ => UIIngameMenuComponent.OnIngameMenuOptionButtonPressed(typeof(UIQuestbookComponent)));
            RegisterAction("ingame.stats", _ => UIIngameMenuComponent.OnIngameMenuOptionButtonPressed(typeof(UIStatsComponent)));
            RegisterAction("ingame.settings", _ => UIIngameMenuComponent.OnIngameMenuOptionButtonPressed(typeof(UISettingsComponent)));
            RegisterAction("ingame.quit", _ => UIIngameMenuComponent.OnIngameMenuOptionButtonPressed(typeof(UIQuitComponent)));
            RegisterAction("ingame.quit.exit", _ => System.Environment.Exit(0));
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