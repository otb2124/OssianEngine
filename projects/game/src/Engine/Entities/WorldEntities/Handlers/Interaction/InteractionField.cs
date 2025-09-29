using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InteractionField
    {
        public static Vector2 DEFAULT_INTERACTIONFIELD_SIZE = new Vector2(30, 30);

        public Hitbox Hitbox;
        public Vector2 Size;

        public InteractionField(Vector2 size)
        {
            Size = size;
            Hitbox = new Hitbox();
        }

        public InteractionField()
        {
            Size = DEFAULT_INTERACTIONFIELD_SIZE;
            Hitbox = new Hitbox();
        }

        public void Update(Vector2 pos, Vector2 modelSize, float rot)
        {
            Hitbox.Update(pos, new Vector2(modelSize.X + Size.X, modelSize.Y + Size.Y), rot);
        }

        public void Draw()
        {
            Hitbox.Draw(Color.Red);
        }
    }
}
