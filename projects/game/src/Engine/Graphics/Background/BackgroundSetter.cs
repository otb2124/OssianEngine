using Microsoft.Xna.Framework;
using Resources;
using System.Collections.Generic;
using Utils;

namespace Graphics
{
    public static class BackgroundSetter
    {



        public static List<BackgroundEntity> SetEntities(int Id)
        {

            List<BackgroundEntity> backgrounds = new List<BackgroundEntity>();

            switch (Id)
            {
                case 0:
                    backgrounds.Add(new BackgroundEntity(StaticSprites.GRAPHICS_MOON, new Vector2(0, 200), 1) { isStickToCamera = true, isStickToZoom = true });
                    break;
                case 1:
                    backgrounds.Add(new BackgroundEntity(StaticSprites.GRAPHICS_SUN, new Vector2(0, 150), 1) { isStickToCamera = true, isStickToZoom = true });
                    break;
            }

            return backgrounds;
        }


        public static ParallaxBackground SetParallax(int id)
        {

            ParallaxBackground parallax = new ParallaxBackground(ParallaxBackground.ParallaxBackgrounds.GREEN);
                        

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



        public static List<DynamicBackgroundEvent> SetDynamicBackgroundEvents(int id)
        {
            List<DynamicBackgroundEvent> list = new List<DynamicBackgroundEvent>();

            switch (id)
            {
                case 0:
                    break;
                case 1:
                    list.Add(new DynamicBackgroundEvent(DynamicBackgroundEvent.DynamicBackgroundEvents.CLOUDY_SKY));
                    break;
            }

            return list;
        }

    }
}
