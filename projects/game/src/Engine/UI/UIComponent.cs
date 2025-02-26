using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using System;
using SharpDX.Direct2D1.Effects;
using Graphics;


namespace UI
{
    public class UIComponent
    {
        public Vector2 Position; //needs reload
        public Vector2 Scale; //needs reload
        public float Rotation; //needs reload
        public Rectangle Bounds; //needs reload
        public Vector2 Origin; //needs reload

        public Vector2 adjPosition;
        public Vector2 adjScale;
        public float adjRotation;
        public Rectangle adjBounds;
        public Vector2 adjOrigin;


        public Texture2D Texture;
        public Color Color;
        public SpriteFont Font;
        public string Text;
        public float LayerDepth;

        public bool IsHovered;
        public bool IsClicked;
        public Action OnClick;
        public Action OnHover;

        public bool IsVisible;
        public bool IsEnabled;
        public float Opacity;

        public enum UIComponentType { EXISTS, MOUSE_CURSOR, TEXT, FRAME_PART }
        public UIComponentType type;

        // Constructor
        public UIComponent()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
            Bounds = new Rectangle(0, 0, 0, 0);
            Origin = Vector2.Zero;

            Texture = null;
            Color = Color.White;
            Font = null;
            Text = string.Empty;
            LayerDepth = 0f;

            IsHovered = false;
            IsClicked = false;
            OnClick = null;
            OnHover = null;

            IsVisible = true;
            IsEnabled = true;
            Opacity = 1f;

            type = UIComponentType.EXISTS;
        }




        public void Update()
        {

        }

        public void Draw()
        {
            Draw();
        }
    }
}
