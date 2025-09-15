using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Resources;
using Graphics;
using Utils;
using System.Diagnostics;
using static Resources.StaticSpriteFactory;
using System;


namespace UI
{
    public class UIComponent
    {

        public enum UIComponentTypes
        {
            //COMMON
            //FRAME
            FRAME, FRAMEPART,
            //TEXT
            TEXT, TEXT_FRAME,
            //ICON
            ICON,
            //BUTTON
            BUTTON, BUTTON_FRAME, BUTTON_TEXT_FRAME, BUTTON_ICON, BUTTON_ICON_FRAME,
            //BARS
            STAT_BAR,
            //MISC

            //MENU
            MENU_INGAME, MENU_INGAME_INVENTORY, MENU_INGAME_SKILLS, MENU_INGAME_QUESTBOOK, MENU_INGAME_STATISTICS,
            //WINDOWS
            WARNING_WINDOW,
            //HUD
            HUD, CURSOR, PLAYER_INDICATORS,
            //INVENTORY
            INVENTORY, INVENTORY_SLOT, INVENTORY_SLOTBOARD,
            INVENTORY_TO_EQUIPMENT,
            //EQUIPMENT
            EQUIPMENT, EQUIPMENT_SLOT,
        }

        public AnimationManager aManager;
        public StaticSprites sprite;
        public SpriteData spriteData;

        public Vector2 Position;
        public float Rotation;
        public Vector2 Origin;
        public Vector2 Scale;

        public Vector2 adjPosition;
        public float adjRotation;
        public Vector2 adjOrigin;
        public Vector2 adjScale;

        //states
        public bool IsStickToCameraState;
        public bool IsStickToZoomState;
        public bool IsStickToCursorState;
        public bool IsAppliedHalfScreenOriginState;

        //flags
        public bool WasRefreshedFlag;

        public UIComponent[] children;

        public string text;
        public Font font;

        public Color? Tint { get; set; } = null;

        public UIComponentTypes type;

        public int Id { get; set; }

        public UIComponent(int id)
        {
            Id = id;
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

            if (IsAppliedHalfScreenOriginState)
            {
                adjPosition -= new Vector2(Graphics.Graphics.screen.Width / 2, Graphics.Graphics.screen.Height / 2);
            }

            float zoomFactor = 1f;
            if (IsStickToZoomState)
            {
                float currentZoom = (float)Graphics.Graphics.camera.Z;
                float baseZoom = (float)Graphics.Graphics.camera.GetZFromHeight(Graphics.Graphics.screen.Height);
                zoomFactor = currentZoom / baseZoom;
                adjScale *= zoomFactor;
            }

            if (IsStickToCursorState)
            {
                Vector2 mouseWorldPos = Inputs.Inputs.mouse.GetMouseWorldPosition();
                adjPosition = mouseWorldPos;
            }
            else if (IsStickToCameraState)
            {
                adjPosition += Graphics.Graphics.camera.Position;
                if (IsStickToZoomState)
                {
                    adjPosition = Graphics.Graphics.camera.Position + (adjPosition - Graphics.Graphics.camera.Position) * zoomFactor;
                }
            }
            else if (IsStickToZoomState)
            {
                adjPosition = Graphics.Graphics.camera.Position + (adjPosition - Graphics.Graphics.camera.Position) * zoomFactor;
            }


            WasRefreshedFlag = false;
        }


        public virtual void Draw()
        {
            if (aManager != null)
            {
                Color color = Tint ?? Color.White;
                aManager.GetCurrent().Draw(adjPosition, color, adjRotation, adjOrigin, adjScale, 0f);
            }

            if(text != null)
            {
                Color color = Tint ?? Color.White;
                font.Draw(text, adjPosition, 0f, adjOrigin, adjScale, color);
            }
        }


        public virtual void Refresh() 
        {
            WasRefreshedFlag = true;
        }

        public virtual void DrawDebug()
        {
            
        }
    }
}
