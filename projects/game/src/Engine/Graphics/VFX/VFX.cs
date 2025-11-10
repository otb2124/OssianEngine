using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{

    public enum VFXs
    {
        EXPLOSION,
        WATER_MOVE_EFFECT,
    };

    public class VFX
    {

        public VFXs Type;
        public AnimationSet AnimationSet;

        public Vector2 Position;
        public Vector2 Size;

        public bool WasPlayed = false;

        public VFX(VFXs type, Vector2 pos, Vector2 size, AnimationSet animSet)
        {
            Type = type;
            Position = pos;
            Size = size;
            AnimationSet = animSet;
        }

        public void Update()
        {
            AnimationSet.Update();

            if(AnimationSet.IsCurrentAnimationLastFrame())
            {
                WasPlayed = true;
            }
        }

        public void Draw()
        {
            AnimationSet.DrawCurrent(Position, Color.White, 0f, Vector2.Zero, Vector2.One, 0f, false);
        }


        public static Dictionary<VFXs, SpriteSheets> VFXSpriteSheetMap = new()
        {
            { VFXs.EXPLOSION, SpriteSheets.VFX_EXPLOSION }
        };
    }
}