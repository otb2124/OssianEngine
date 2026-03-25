using Myra;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;
using System;
using Resources;
using System.Collections.Generic;
using AssetManagementBase;
using Myra.Graphics2D.TextureAtlases;
using SharpDX.Direct2D1.Effects;

namespace UI
{
    public class UIDesktop
    {
        public Desktop Desktop;
        public Panel Root;
        public List<UIComponent> Components;

        public UIDragDropService DragDropService;

        public static float UIScale = 1f;

        public UIDesktop() { }

        public void Init(Game game)
        {
            MyraEnvironment.Game = game;
            Desktop = new Desktop();
            Root = new Panel();
            Desktop.Root = Root;

            var viewport = Graphics.Graphics.GraphicsDeviceManager.GraphicsDevice.Viewport;
            var scaleX = (float)viewport.Width / Graphics.Graphics.ScreenResolution.X;
            var scaleY = (float)viewport.Height / Graphics.Graphics.ScreenResolution.Y;
            UIScale = Math.Min(scaleX, scaleY);


            UIStylesheet.Apply();

            Components = new List<UIComponent>();

            DragDropService = new UIDragDropService();

            //TODO: change to custom cursor
            game.IsMouseVisible = true;
        }

        public void ScaleWidgets(Widget widget)
        {
            if (widget == null) return;

            if (widget.Width.HasValue)
                widget.Width = (int)(widget.Width.Value * UIScale);

            if (widget.Height.HasValue)
                widget.Height = (int)(widget.Height.Value * UIScale);

            widget.Left = (int)(widget.Left * UIScale);
            widget.Top = (int)(widget.Top * UIScale);

            if (widget is Container container)
            {
                foreach (var child in container.Widgets)
                    ScaleWidgets(child);
            }
        }

        public bool HasComponent(Type type)
        {
            return Components.Find(c => c.GetType() == type) != null;
        }

        public void ToggleComponent(Type type)
        {
            if (!HasComponent(type))
                AddComponent((UIComponent)Activator.CreateInstance(type));
            else
                RemoveComponent(type);
        }

        public void AddComponent(UIComponent component)
        {
            component.ReloadTemplate();
            Components.Add(component);
            Root.Widgets.Add(component.Template.Project.Root);
            ScaleWidgets(component.Template.Project.Root);
            component.Init();
        }

        public void RemoveComponent(UIComponent component)
        {
            Root.Widgets.Remove(component.Template.Project.Root);
            Components.Remove(component);
        }

        // remove by type
        public void RemoveComponent(Type type)
        {
            var component = Components.Find(c => c.GetType() == type);
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


        public void SetButtonImage(string id, StaticSprite sprite)
        {
            var btn = FindById(id) as ImageButton;
            btn.Image = new TextureRegion(ResourceLoader.spriteSheets[sprite.SpriteSheet].Texture, sprite.SrcRect);
        }


        public static Point GetAbsolutePosition(Widget widget)
        {
            if (widget == null) return Point.Zero;

            float x = widget.Left;
            float y = widget.Top;

            var current = widget.Parent;
            while (current != null)
            {
                x += current.Left;
                y += current.Top;

                // If parent has scroll offset (rare in your case)
                if (current is ScrollViewer scrollViewer)
                {
                    x -= scrollViewer.ScrollPosition.X;
                    y -= scrollViewer.ScrollPosition.Y;
                }

                current = current.Parent;
            }

            // Apply global UI scale if you use it
            //x *= UIDesktop.UIScale;
            //y *= UIDesktop.UIScale;

            return new Point((int)x, (int)y + Graphics.Graphics.Screen.Height/2);
        }

        public static Rectangle GetAbsoluteBounds(Widget widget)
        {
            if (widget == null)
                return Rectangle.Empty;

            float x = widget.Left;
            float y = widget.Top;
            float width = widget.Width ?? 0;
            float height = widget.Height ?? 0;

            var current = widget.Parent;

            while (current != null)
            {
                x += current.Left;
                y += current.Top;

                // Handle ScrollViewer offset if present
                if (current is ScrollViewer scrollViewer)
                {
                    x -= scrollViewer.ScrollPosition.X;
                    y -= scrollViewer.ScrollPosition.Y;
                }

                current = current.Parent;
            }

            // Apply global UI scale
            //x *= UIDesktop.UIScale;
            //y *= UIDesktop.UIScale;
            //width *= UIDesktop.UIScale;
            //height *= UIDesktop.UIScale;

            return new Rectangle(
                (int)x,
                (int)y + Graphics.Graphics.Screen.Height/2,
                (int)width,
                (int)height
            );
        }
    }
}