using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Resources;
using Graphics;
using SharpDX.Direct2D1.Effects;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using static Entities.PhysicalEntity;
using static Graphics.Animation;
using System;


namespace UI
{
    public class UIComponent
    {

        public AnimationManager aManager;
        public StaticSpriteFactory.StaticSprites sprite;

        public Vector2 Position; //needs reload
        public Vector2 adjPosition; //needs reload
        public Vector2 Scale; //needs reload
        public Vector2 adjScale;
        public float Rotation; //needs reload
        public Rectangle Bounds; //needs reload
        public Vector2 Origin;

        public bool stickToCamera;
        public bool stickToZoom;

        public Vector2 ZoomOrigin;

        public UIComponent()
        {
            aManager = new AnimationManager();

            Position = Vector2.Zero;
            Scale = new Vector2(1, 1);
            Rotation = 0f;
            Bounds = new Rectangle(0, 0, 0, 0);
            Origin = Vector2.Zero;

            stickToCamera = true;
            ZoomOrigin = Vector2.Zero;
        }


        public virtual void Update()
        {
            adjPosition = Position;
            adjScale = Scale;

            if (stickToCamera)
            {
                adjPosition += Graphics.Graphics.camera.Position;
            }
        }

        public virtual void Draw()
        {

            this.aManager.GetCurrent().Draw(adjPosition, Color.White, 0f, Vector2.Zero, Vector2.One, 0f);
        }
    }
}
