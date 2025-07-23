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
        public Vector2 Position;
        public LightSourceData Data;
        public Texture2D texture;

        public LightSource(Vector2 position, float radius, Color color)
        {
            Position = position;
            Data = new LightSourceData(LightSourceData.LightSourceForms.CIRCULAR, new Vector2(radius, radius), Vector2.Zero, color, 50f, 0f);
            texture = CreateCircleTexture(64);
        }

        public LightSource(Vector2 position, LightSourceData data)
        {
            Position = position;
            Data = data;
            texture = CreateCircleTexture(64);
        }

        public LightSource(Vector2 position, Vector2 size, Color color)
        {
            Position = position;
            Data = new LightSourceData(LightSourceData.LightSourceForms.RECTANGULAR, size, Vector2.Zero, color, 50f, 0f);
            texture = new Texture2D(Graphics.graphicsDeviceManager.GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.White });
        }

        private Texture2D CreateCircleTexture(int diameter)
        {
            Texture2D texture = new Texture2D(Graphics.graphicsDeviceManager.GraphicsDevice, diameter, diameter);
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
            if (Data.Form == LightSourceData.LightSourceForms.CIRCULAR)
            {
                Graphics.sprites.Draw(
                    texture,
                    null,
                    new Vector2(texture.Width / 2f, texture.Height / 2f),
                new Vector2(Position.X, Position.Y),
                0f,
                    new Vector2(Data.Size.X * 2f / texture.Width),
                    Data.Color
                );
            }
            else
            {
                Graphics.sprites.Draw(
                    texture,
                    null,
                    new Vector2(0.5f, 0.5f),
                new Vector2(Position.X, Position.Y),
                0f,
                    Data.Size,
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
            public float Power;
            public Vector2 Origin;
            public float Rotation;

            public LightSourceData(LightSourceForms form, Vector2 size, Vector2 origin, Color color, float power, float rotation)
            {
                Form = form;
                Size = size;
                Origin = origin;
                Color = color;
                Power = power;
                Rotation = rotation;
            }
        }
    }
}
