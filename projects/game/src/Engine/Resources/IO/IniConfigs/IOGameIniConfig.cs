using App;
using Microsoft.Xna.Framework;
using System;

namespace Resources
{
    public class IOGameIniConfig : IOIniConfig
    {
        protected override string FileName => "Game.ini";


        public Point Resolution
        {
            get => GetPoint("Resolution");
            set => SetPoint("Resolution", new Point(value.X, value.Y));
        }

        public Point WindowPosition
        {
            get => GetPoint("WindowPosition");
            set => SetPoint("WindowPosition", new Point(value.X, value.Y));
        }

        public float BufferRatio
        {
            get => GetFloat("BufferRatio");
            set => SetValue("BufferRatio", value.ToString());
        }

        public bool Fullscreen
        {
            get => GetBool("Fullscreen");
            set => SetValue("Fullscreen", value.ToString().ToLower());
        }

        public bool VSync
        {
            get => GetBool("VSync");
            set => SetValue("VSync", value.ToString().ToLower());
        }

        public bool Borderless
        {
            get => GetBool("Borderless");
            set => SetValue("Borderless", value.ToString().ToLower());
        }

        public int GraphicsFrameRate
        {
            get => GetInt("GraphicsFrameRate");
            set => SetValue("GraphicsFrameRate", value.ToString());
        }

        public int LogicUpdateRate
        {
            get => GetInt("LogicUpdateRate");
            set => SetValue("LogicUpdateRate", value.ToString());
        }

        public bool MouseVisible
        {
            get => GetBool("MouseVisible");
            set => SetValue("MouseVisible", value.ToString().ToLower());
        }

        public bool ConsoleVisible
        {
            get => GetBool("ConsoleVisible");
            set => SetValue("ConsoleVisible", value.ToString().ToLower());
        }

        protected override void InitializeDefaults()
        {
            Lines.Add(new IOIniLine("Resolution", "1280,720", "resolution"));
            Lines.Add(new IOIniLine("WindowPosition", "400,40", "window pos"));
            Lines.Add(new IOIniLine("BufferRatio", "0.85", "widnow ratio"));
            Lines.Add(new IOIniLine("Fullscreen", "false"));
            Lines.Add(new IOIniLine("VSync", "true"));
            Lines.Add(new IOIniLine("Borderless", "false"));
            Lines.Add(new IOIniLine("GraphicsFrameRate", "120", "fps"));
            Lines.Add(new IOIniLine("LogicUpdateRate", "60", "ups"));
            Lines.Add(new IOIniLine("MouseVisible", "false"));
            Lines.Add(new IOIniLine("ConsoleVisible", "false"));
        }

        public override void Apply()
        {
            Graphics.Graphics.PreferredBackBufferSize = Resolution;
            Graphics.Graphics.WindowPosition = WindowPosition;
            Graphics.Graphics.BufferRatio = BufferRatio;
            Graphics.Graphics.IsFullscreen = Fullscreen;
            Graphics.Graphics.SynchronizeWithVerticalRetrace = VSync;
            Graphics.Graphics.IsBorderLess = Borderless;
            Graphics.Graphics.GraphicsFrameRate = GraphicsFrameRate;
            Graphics.Graphics.LogicUpdateRate = LogicUpdateRate;
            Graphics.Graphics.IsMouseVisible = MouseVisible;
            Graphics.Graphics.ConsoleVisible = ConsoleVisible;
        }
    }
}