using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Resources;
using Graphics;
using SharpDX.Direct2D1.Effects;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using static Entities.PhysicalEntity;
using static Graphics.Animation;
using System;
using System.Diagnostics;


namespace UI
{
    public class UIComponent
    {

        public AnimationManager aManager;
        public StaticSpriteFactory.StaticSprites sprite;

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

        public UIComponent()
        {
            aManager = new AnimationManager();

            Position = Vector2.Zero;
            Rotation = 0f;
            Origin = Vector2.Zero;
            Scale = new Vector2(1, 1);
        }


        public virtual void Update()
        {
            adjPosition = Position;
            adjRotation = Rotation;
            adjOrigin = Origin;
            adjScale = Scale;

            //important
            if (stickToCursor && stickToZoom)
            {
                stickToCamera = false;
            }

            if (stickToCamera)
            {
                adjPosition += Graphics.Graphics.camera.Position;
            }

            if (stickToCursor)
            {
                adjPosition += new Vector2(Inputs.Inputs.mouse.GetMouseWorldPosition().X, Inputs.Inputs.mouse.GetMouseWorldPosition().Y);
                // worldPos + cameraPos = screenPos
            }

            if (stickToZoom)
            {
                float currentZoom = (float)Graphics.Graphics.camera.Z;
                float baseZoom = (float)Graphics.Graphics.camera.BaseZ;
                adjScale *= currentZoom / baseZoom;
            }
        }


        public virtual void Draw()
        {

            this.aManager.GetCurrent().Draw(adjPosition, Color.White, adjRotation, adjOrigin, adjScale, 0f);
        }
    }
}
