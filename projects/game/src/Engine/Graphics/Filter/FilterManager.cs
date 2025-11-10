using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class FilterManager
    {

        List<FilterLayer> MapLayers;
        List<FilterLayer> DayTimeLayers;

        public FilterManager()
        {
            MapLayers = new List<FilterLayer>();
            DayTimeLayers = new List<FilterLayer>();
        }

        public void Init()
        {
            DayTimeLayers.Add(new FilterLayer(Color.Black, 0.95f, 0f, 0.95f, StaticSprites.LIGHT_DARKNESS_FULL));
            DayTimeLayers.Add(new FilterLayer(Color.Black, 0.95f, 0f, 1f, StaticSprites.LIGHT_DARKNESS_VIGNETTE));
        }

        public void UpdateLayers()
        {
            MapLayers.Clear();

            foreach (FilterLayer layer in Entities.Entities.EntityMapManager.GetCurrentMap().FilterLayers)
            {
                MapLayers.Add(layer);
            }
        }

        public void Update()
        {
            foreach (FilterLayer dayTimeLayer in DayTimeLayers)
            {
                dayTimeLayer.Update();
            }
        }

        public void Draw()
        {
            foreach (FilterLayer layer in MapLayers)
            {
                layer.Draw();
            }

            foreach (FilterLayer dayTimeLayer in DayTimeLayers)
            {
                dayTimeLayer.Draw();
            }
        }
    }
}
