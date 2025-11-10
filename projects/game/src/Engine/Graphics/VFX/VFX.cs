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

        public VFX(SpriteSheets sheet, Animation anim)
        {
            AnimationSet = new AnimationSet(sheet, new List<Animation>() { anim });
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


        public static Dictionary<VFXs, VFX> VFXMap = new()
        {
            { VFXs.EXPLOSION, new VFX(SpriteSheets.ENITIES_FIREBALL, new Animation(AnimationKey.IdleKey, new AnimationFramesData(6, new Vector2(0, 0), new Vector2(64, 64), 1f, SpriteEffects.None))) }
        };
    }
}