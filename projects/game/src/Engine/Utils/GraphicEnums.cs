using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public enum SpriteSheets
    {
        //--------
        //GRAPHICS
        //parallax
        GRAPHICS_PARALLAX_0,

        //static
        GRAPHICS_STATIC,
        
        //sun
        GRAPHICS_SUN,

        //rain
        GRAPHICS_CLOUDS,

        //--------
        //ENTITIES
        //livingentities
        ENTITIES_PLAYER,
        ENTITIES_MOB0,

        //physicalentities
        ENTITIES_STATIC,

        //platforms
        ENTITIES_PLATFORMS,

        //equipment
        ENTITIES_WEAPONS,

        //--
        //UI
        UI_CURSOR,
        UI_FRAMES,
        UI_GAME_ICON,
        UI_ICONS,
        UI_HUD
    }

    public enum StaticSprites
    {
        //--------
        //GRAPHICS
        //parallax
        GRAPHICS_PARALLAX_0_0,
        GRAPHICS_PARALLAX_0_1,
        GRAPHICS_PARALLAX_0_2,
        GRAPHICS_PARALLAX_0_3,
        GRAPHICS_PARALLAX_0_N,

        //static
        GRAPHICS_STATIC_DRAGON,

        //sun
        GRAPHICS_SUN,

        //rain
        GRAPHICS_CLOUD_0,

        //--------
        //ENTITIES
        //livingentities
        ENTITIES_PLAYER,
        ENTITIES_MOB0,

        //physicalentities
        ENTITIES_STATIC_BALL,
        ENTITIES_STATIC_CIRCLE,

        //crates
        ENTITIES_STATIC_CRATE_0,
        ENTITIES_STATIC_CRATE_1,

        //equipment
        ENTITIES_WEAPONS_SWORD0,
        ENTITIES_WEAPONS_SWORD1,

        //--
        //UI
        //MISC
        UI_CURSOR,
        UI_GAME_ICON
    }


    public enum FlatBodyPreset
    {
        CRATE_0,
        CRATE_1,
        CIRCLE,
        HUMANOID,
    }

    public enum Models
    {
        PLAYER,
        CRATE_0, CRATE_1, BALL, COIN,
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
