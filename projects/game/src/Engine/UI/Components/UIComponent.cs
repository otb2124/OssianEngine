using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Resources;
using Graphics;
using Utils;
using System.Diagnostics;


namespace UI
{
    public class UIComponent
    {

        public enum ComponentTypes
        {
            CURSOR,
            FRAME,
        }

        public AnimationManager aManager;
        public StaticSprites sprite;

        public Vector2 Position;
        public float Rotation;
        public Vector2 Origin;
        public Vector2 Scale;

        public Vector2 adjPosition;
        public float adjRotation;
        public Vector2 adjOrigin;
        public Vector2 adjScale;

        public bool stickToCamera;
        public bool stickToZoom;
        public bool stickToCursor;
        public bool applyHalfScreenOrigin;

        public UIComponent[] children;

        public Color? Tint { get; set; } = null;

        public ComponentTypes type;

        public UIComponent()
        {
            Position = Vector2.Zero;
            Rotation = 0f;
            Origin = Vector2.Zero;
            Scale = new Vector2(1, 1);
        }

        public virtual void Init()
        {
            aManager = new AnimationManager();
            aManager.AddStaticAnimation(this.sprite);
        }


        public virtual void Update()
        {
            adjPosition = Position;
            adjRotation = Rotation;
            adjOrigin = Origin;
            adjScale = Scale;

            if (applyHalfScreenOrigin)
            {
                adjPosition -= new Vector2(Graphics.Graphics.screen.Width / 2, Graphics.Graphics.screen.Height / 2);
            }

            float zoomFactor = 1f;
            if (stickToZoom)
            {
                float currentZoom = (float)Graphics.Graphics.camera.Z;
                float baseZoom = (float)Graphics.Graphics.camera.GetZFromHeight(Graphics.Graphics.screen.Height);
                zoomFactor = currentZoom / baseZoom;
                adjScale *= zoomFactor;
            }

            if (stickToCursor)
            {
                Vector2 mouseWorldPos = Inputs.Inputs.mouse.GetMouseWorldPosition();
                adjPosition = mouseWorldPos;
            }
            else if (stickToCamera)
            {
                adjPosition += Graphics.Graphics.camera.Position;
                if (stickToZoom)
                {
                    adjPosition = Graphics.Graphics.camera.Position + (adjPosition - Graphics.Graphics.camera.Position) * zoomFactor;
                }
            }
            else if (stickToZoom)
            {
                adjPosition = Graphics.Graphics.camera.Position + (adjPosition - Graphics.Graphics.camera.Position) * zoomFactor;
            }
        }


        public virtual void Draw()
        {
            if (aManager != null)
            {
                Color color = Tint ?? Color.White;
                aManager.GetCurrent().Draw(adjPosition, color, adjRotation, adjOrigin, adjScale, 0f);
            }
                   
        }
    }
}
