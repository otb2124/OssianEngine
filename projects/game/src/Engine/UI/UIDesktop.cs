using Myra;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;
using System;
using Resources;
using System.Collections.Generic;

namespace UI
{
    public class UIDesktop
    {
        public Desktop Desktop;
        public Panel Root;
        public List<UIComponent> Components;

        public UIDesktop() { }

        public void Init(Game game)
        {
            MyraEnvironment.Game = game;
            Desktop = new Desktop();
            Root = new Panel();
            Desktop.Root = Root;

            Components = new List<UIComponent>();

            //TODO: change to custom cursor
            game.IsMouseVisible = true;
        }


        public bool HasComponent<T>() where T : UIComponent
        {
            var component = Components.Find(c => c is T);
            if (component != null) return true;

            return false;
        }

        public void AddComponent(UIComponent component)
        {
            Components.Add(component);
            Root.Widgets.Add(component.Template.Project.Root);
            component.Init();
        }

        public void RemoveComponent(UIComponent component)
        {
            Root.Widgets.Remove(component.Template.Project.Root);
            Components.Remove(component);
        }

        // remove by type
        public void RemoveComponent<T>() where T : UIComponent
        {
            var component = Components.Find(c => c is T);
            if (component == null) return;
            Root.Widgets.Remove(component.Template.Project.Root);
            Components.Remove(component);
        }

        // remove all
        public void ClearComponents()
        {
            Root.Widgets.Clear();
            Components.Clear();
        }

        public Widget FindById(string id)
        {
            return Desktop.Root?.FindWidgetById(id);
        }

        public void Draw()
        {
            Desktop?.Render();
        }
    }
}