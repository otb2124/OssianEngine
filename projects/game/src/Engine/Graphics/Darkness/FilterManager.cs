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

        List<FilterLayer> layers;

        public FilterManager()
        {
            layers = new List<FilterLayer>();
        }

        public void Init()
        {
            layers.Clear();

            foreach (FilterLayer layer in Entities.Entities.entityMapManager.GetCurrentMap().FilterLayers)
            {
                layers.Add(layer);
            }
        }

        public void Update()
        {
            foreach (FilterLayer layer in layers)
            {
                layer.Update();
            }
        }

        public void Draw()
        {
            foreach (FilterLayer layer in layers)
            {
                layer.Draw();
            }
        }
    }
}
