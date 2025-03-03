using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public enum SpriteSheets
    {
        DECOR,
        HERO,
        UI,
        BACKGROUND,
        BG_CLOUDS,
        BG_SUN,
        MOB,
        DRAGON,
        WEAPONS
    }

    public enum StaticSprites
    {
        PLATFORM,
        CRATE,
        CRATE_SMALL,
        CIRCLE,
        HERO,
        CURSOR,
        BACKGROUND,
        BG_CLOUD_0,
        BG_SUN,
        MOB,
        DRAGON,
        SWORD,
        SWORD_0,
    }


    public enum FlatBodyPreset
    {
        PLATFORM,
        BLOCK,
        BOX,
        CIRCLE,
        HUMANOID,
    }

    public enum Models
    {
        HERO,
        CRATE_BIG,
        CRATE_SMALL,
        BALL,
        PLATFORM,
        MOB
    }

    public enum ModelStates
    {
        IDLE,
        MOVING,
        ATTACKING,
    }

    public enum AnimationStates
    {
        IDLE,
        MOVING,
        ATTACKING
    }


    public enum Directions
    {
        LEFT,
        RIGHT
    }
}
