using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Graphics
{
    public class FilterLayer
    {

        public Color DarknessCurrentColor = Color.Black;

        public float DarknessCurrentAlpha;

        public float DarknessMaxAlpha = 0.95f;
        public float DarknessMinAlpha = 0f;

        public AnimationManager aManager;

        public FilterLayer(Color color, float MaxAlpha = 1f, float MinAlpha = 0f, float CurrentAlpha = 1f, StaticSprites sprites = StaticSprites.LIGHT_DARKNESS_FULL)
        {
            DarknessMaxAlpha = MaxAlpha;
            DarknessMinAlpha = MinAlpha;
            if (CurrentAlpha != 1f)
            {
                DarknessCurrentAlpha = CurrentAlpha;
            }
            else
            {
                DarknessCurrentAlpha = DarknessMaxAlpha;
            }
            aManager = new AnimationManager();
            aManager.AddStaticAnimation(StaticSpriteFactory.spriteMappings[sprites]);
        }

        public void Update()
        {
            //
        }

        public void Draw()
        {
            Vector2 scale = new Vector2(Graphics.ScreenResolution.X / aManager.GetCurrent().sourceRectangles[0].Width, Graphics.ScreenResolution.Y / aManager.GetCurrent().sourceRectangles[0].Height);
            aManager.GetCurrent().Draw(new Vector2(Graphics.camera.Position.X - Graphics.ScreenResolution.X / 2, Graphics.camera.Position.Y - Graphics.ScreenResolution.Y / 2), DarknessCurrentColor * DarknessCurrentAlpha, 0f, Vector2.Zero, scale, 0f);
        }
    }
}
