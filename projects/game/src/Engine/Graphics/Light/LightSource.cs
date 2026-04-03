using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class LightSource
    {
        public int Id;

        public Vector2 Position;
        public LightSourceData Data;
        public Texture2D texture;

        public LightSource()
        {
            texture = CreateCircleTexture(64);
        }

        public LightSource(Vector2 position, float radius, Color color)
        {
            Position = position;
            Data = new LightSourceData(LightSourceData.LightSourceForms.CIRCULAR, new Vector2(radius, radius), Vector2.Zero, color, 0f);
            texture = CreateCircleTexture(64);

            Id = Graphics.LightManager.GenerateId();
        }

        public LightSource(Vector2 position, LightSourceData data)
        {
            Position = position;
            Data = data;
            texture = CreateCircleTexture(64);

            Id = Graphics.LightManager.GenerateId();
        }

        public LightSource(Vector2 position, Vector2 size, Color color)
        {
            Position = position;
            Data = new LightSourceData(LightSourceData.LightSourceForms.RECTANGULAR, size, Vector2.Zero, color, 0f);
            texture = new Texture2D(Graphics.GraphicsDeviceManager.GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.White });

            Id = Graphics.LightManager.GenerateId();
        }

        private Texture2D CreateCircleTexture(int diameter)
        {
            Texture2D texture = new Texture2D(Graphics.GraphicsDeviceManager.GraphicsDevice, diameter, diameter);
            Color[] data = new Color[diameter * diameter];
            float radius = diameter / 2f;
            Vector2 center = new Vector2(radius, radius);

            for (int x = 0; x < diameter; x++)
            {
                for (int y = 0; y < diameter; y++)
                {
                    Vector2 pos = new Vector2(x, y);
                    float distance = Vector2.Distance(center, pos);
                    if (distance < radius)
                    {
                        float alpha = 1f - (distance / radius); // Fade to edge
                        data[x + y * diameter] = new Color(1f, 1f, 1f, alpha);
                    }
                    else
                    {
                        data[x + y * diameter] = Color.Transparent;
                    }
                }
            }

            texture.SetData(data);
            return texture;
        }

        public virtual void Update() { }

        public virtual void Update(Vector2 position, LightSourceData data) 
        {
            Position = position;
            Data = data;
        }

        public virtual void Update(Vector2 position) 
        {
            Position = position;
        }

        public virtual void Draw()
        {
            if (Data == null) return;

            // How many pixels per world unit at current zoom
            float visibleWorldHeight = (float)(2.0 * Math.Tan(MathHelper.PiOver2 * 0.5f) * Graphics.Camera.Z);
            float worldToScreen = Graphics.Screen.Height / visibleWorldHeight;

            Vector2 screenPos = Graphics.Camera.WorldToScreen(new Vector2(Position.X, Position.Y));

            if (Data.Form == LightSourceData.LightSourceForms.CIRCULAR)
            {
                float screenRadius = Data.Size.X * worldToScreen;
                Vector2 scale = new Vector2(screenRadius * 2f / texture.Width);

                Graphics.Sprites.Draw(
                    texture,
                    null,
                    new Vector2(texture.Width / 2f, texture.Height / 2f),
                    screenPos,
                    0f,
                    scale,
                    Data.Color
                );
            }
            else
            {
                Vector2 screenSize = Data.Size * worldToScreen;

                Graphics.Sprites.Draw(
                    texture,
                    null,
                    new Vector2(0.5f, 0.5f),
                    screenPos,
                    0f,
                    screenSize,
                    Data.Color
                );
            }
        }


        public class LightSourceData
        {
            public enum LightSourceForms
            {
                CIRCULAR,
                RECTANGULAR
            }

            public LightSourceForms Form;
            public Vector2 Size;
            public Color Color;
            public Vector2 Origin;
            public float Rotation;

            public LightSourceData(LightSourceForms form, Vector2 size, Vector2 origin, Color color, float rotation)
            {
                Form = form;
                Size = size;
                Origin = origin;
                Color = color;
                Rotation = rotation;
            }
        }
    }
}
