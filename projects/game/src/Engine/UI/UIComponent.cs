using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Resources;


namespace UI
{
    public class UIComponent
    {
        public Vector2 Position; //needs reload
        public Vector2 adjPosition; //needs reload
        public Vector2 Scale; //needs reload
        public Vector2 adjScale;
        public float Rotation; //needs reload
        public Rectangle Bounds; //needs reload
        public Vector2 Origin;

        public Sprite Sprite;

        public bool stickToCamera;
        public bool stickToZoom;

        public Vector2 ZoomOrigin;

        public UIComponent()
        {
            Position = Vector2.Zero;
            Scale = new Vector2(1, 1);
            Rotation = 0f;
            Bounds = new Rectangle(0, 0, 0, 0);
            Origin = Vector2.Zero;

            Sprite = null;

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
            Sprite.Draw(
                Vector2.Zero,
                Color.White,
                0f,
                Vector2.Zero,
                Vector2.One,
                SpriteEffects.FlipVertically, 0f);
        }
    }
}
