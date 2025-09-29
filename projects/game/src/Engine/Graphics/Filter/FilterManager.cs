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

        List<FilterLayer> mapLayers;
        List<FilterLayer> dayTimeLayers;

        public FilterManager()
        {
            mapLayers = new List<FilterLayer>();
            dayTimeLayers = new List<FilterLayer>();
        }

        public void Init()
        {
            dayTimeLayers.Add(new FilterLayer(Color.Black, 0.95f, 0f, 0.95f, Utils.StaticSprites.LIGHT_DARKNESS_FULL));
            dayTimeLayers.Add(new FilterLayer(Color.Black, 0.95f, 0f, 1f, Utils.StaticSprites.LIGHT_DARKNESS_VIGNETTE));
        }

        public void UpdateLayers()
        {
            mapLayers.Clear();

            foreach (FilterLayer layer in Entities.Entities.EntityMapManager.GetCurrentMap().FilterLayers)
            {
                mapLayers.Add(layer);
            }
        }

        public void Update()
        {
            foreach (FilterLayer dayTimeLayer in dayTimeLayers)
            {
                dayTimeLayer.Update();
            }
        }

        public void Draw()
        {
            foreach (FilterLayer layer in mapLayers)
            {
                layer.Draw();
            }

            foreach (FilterLayer dayTimeLayer in dayTimeLayers)
            {
                dayTimeLayer.Draw();
            }
        }
    }
}
