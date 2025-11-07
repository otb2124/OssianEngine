using Microsoft.Xna.Framework;
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

        public VFX(SpriteSheets sheet, Animation anim)
        {
            AnimationSet = new AnimationSet(sheet, new List<Animation>() { anim });
        }


        public static Dictionary<VFXs, VFX> VFXMap = new()
        {
            { VFXs.EXPLOSION, new VFX(SpriteSheets.ENITIES_FIREBALL, new Animation(AnimationKey.IdleKey, new AnimationFramesData(3, new Vector2(0, 2), new Vector2(64, 64), 1f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None))) }
        };
    }
}
