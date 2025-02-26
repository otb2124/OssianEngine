using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using System;
using SharpDX.Direct2D1.Effects;
using Graphics;
using System.Reflection.Metadata;
using Resources;


namespace UI
{
    public class UIComponent
    {
        public Vector2 Position; //needs reload
        public Vector2 Scale; //needs reload
        public float Rotation; //needs reload
        public Rectangle Bounds; //needs reload
        public Vector2 Origin;

        public Sprite Sprite;

        public bool stickToCamera;
        public bool stickToZoom;

        public UIComponent()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
            Bounds = new Rectangle(0, 0, 0, 0);
            Origin = Vector2.Zero;

            Sprite = null;
        }


        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            Graphics.Graphics.sprites.Draw(
                Sprite.texture,
                Vector2.Zero,
                new Rectangle(0, 0, Sprite.texture.Width, Sprite.texture.Height),
                Color.White,
                0f,
                Vector2.Zero,
                Vector2.One,
                SpriteEffects.FlipVertically, 0f);
        }
    }
}
