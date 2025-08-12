using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public static class FilterLayerMapSetter
    {
        public static List<FilterLayer> FillFilterLayerMap(int mapId)
        {
            List<FilterLayer> layers = new List<FilterLayer>();

            switch (mapId)
            {
                case 0:
                    layers = new List<FilterLayer>()
                    {
                        { new FilterLayer(Color.Black, 0.95f, 0f, 0.95f, Utils.StaticSprites.LIGHT_DARKNESS_FULL) },
                        { new FilterLayer(Color.Black, 0.95f, 0f, 1f, Utils.StaticSprites.LIGHT_DARKNESS_VIGNETTE) }
                    };
                    break;
                case 1:
                    layers = new List<FilterLayer>()
                    {
                        
                    };
                    break;
            }


            return layers;
        }
    }
}
