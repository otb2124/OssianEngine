using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Utils;
using static Graphics.BackgroundManager;

namespace Graphics
{
    public static class BackgroundSetter
    {



        public static List<BackgroundEntity> SetBackgrounds(int Id)
        {

            List<BackgroundEntity> backgrounds = new List<BackgroundEntity>();

            switch (Id)
            {
                case 0:
                    backgrounds.Add(new BackgroundEntity(StaticSprites.GRAPHICS_MOON, new Vector2(0, 200), BackgroundEntity.BGEntityDynamics.STATIC) { isStickToCamera = true, isStickToZoom = true });
                    break;
                case 1:
                    backgrounds.Add(new BackgroundEntity(StaticSprites.GRAPHICS_SUN, new Vector2(0, 200), BackgroundEntity.BGEntityDynamics.STATIC) { isStickToCamera = true, isStickToZoom = true });
                    break;
            }

            return backgrounds;
        }


        public static ParallaxBackground SetParallax(int id)
        {

            ParallaxBackground parallax = new ParallaxBackground(ParallaxBackground.ParallaxBackgrounds.SEASIDE_EVENING);
                        

            switch (id)
            {
                case 0:

                    parallax = new ParallaxBackground(ParallaxBackground.ParallaxBackgrounds.GREEN);
                    break;

                case 1:
                    parallax = new ParallaxBackground(ParallaxBackground.ParallaxBackgrounds.SEASIDE_EVENING);
                    break;
            }

            return parallax;
        }


        public static BackgroundState SetBackgroundState(int Id)
        {

            BackgroundState state = BackgroundState.NONE;

            switch (Id)
            {
                case 0:
                    state = BackgroundState.NONE;
                    break;
            }

            return state;
        }
    }
}
