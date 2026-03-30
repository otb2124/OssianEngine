using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System.Collections.Generic;

namespace Graphics
{
    public class FilterManager
    {
        // Map-specific layers (e.g. fog, color tints set per-map) — still drawn as sprites.
        List<FilterLayer> MapLayers;

        // Day/night layers — these now drive LightMask.AmbientColor instead of drawing sprites.
        // [0] = full-screen darkness    → feeds the mask ambient
        // [1] = vignette                → still drawn as a sprite (screen-edge effect only)
        List<FilterLayer> DayTimeLayers;

        public FilterManager()
        {
            MapLayers = new List<FilterLayer>();
            DayTimeLayers = new List<FilterLayer>();
        }

        public void Init()
        {
            // Index 0: full-screen darkness — drives LightMask.AmbientColor
            DayTimeLayers.Add(new FilterLayer(Color.Black, 0.95f, 0f, 0.95f, StaticSprites.LIGHT_DARKNESS_FULL));

            // Index 1: vignette — still drawn as a sprite over the composited image
            DayTimeLayers.Add(new FilterLayer(Color.Black, 0.95f, 0f, 1f, StaticSprites.LIGHT_DARKNESS_VIGNETTE));
        }

        public void UpdateLayers()
        {
            MapLayers.Clear();
            foreach (FilterLayer layer in Entities.Entities.EntityMapManager.GetCurrentMapLayer().FilterLayers)
            {
                MapLayers.Add(layer);
            }
        }

        public void Update()
        {
            foreach (FilterLayer layer in DayTimeLayers)
            {
                layer.Update();
            }
        }

        /// <summary>
        /// Combines all day/night darkness layers into a single ambient color for the
        /// light mask. Each layer lerps White→its color by its current alpha, then
        /// all layers are multiplied together so they stack correctly.
        ///
        /// Example:
        ///   Darkness layer at alpha 0.8 (nearly night) → dark gray ambient
        ///   Multiplied with a faint blue tint layer     → dark blue-gray ambient
        /// </summary>
        public Color GetDayTimeAmbient()
        {
            // Start fully lit; each darkness layer dims it down.
            // Skip the last layer (index 1) — that's the vignette, which stays a sprite.
            Color ambient = Color.White;

            for (int i = 0; i < DayTimeLayers.Count - 1; i++)
            {
                Color layerContrib = DayTimeLayers[i].GetAmbientContribution();

                // Multiply channels together (same math as the GPU multiply blend).
                ambient = new Color(
                    (ambient.R / 255f) * (layerContrib.R / 255f),
                    (ambient.G / 255f) * (layerContrib.G / 255f),
                    (ambient.B / 255f) * (layerContrib.B / 255f),
                    1f);
            }

            return ambient;
        }

        /// <summary>
        /// Draw sprite overlays: map-specific layers + vignette only.
        /// The full-screen darkness layer is handled by the light mask now.
        /// </summary>
        public void Draw()
        {
            // Map-specific layers (fog, tints, etc.) still draw as sprites.
            foreach (FilterLayer layer in MapLayers)
            {
                layer.Draw();
            }

            // Only the vignette (last DayTimeLayer) draws as a sprite.
            // All other DayTimeLayers feed the mask ambient via GetDayTimeAmbient().
            if (DayTimeLayers.Count > 0)
            {
                DayTimeLayers[DayTimeLayers.Count - 1].Draw();
            }
        }
    }
}