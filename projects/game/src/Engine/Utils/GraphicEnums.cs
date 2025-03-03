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

        //equipment
        ENTITIES_WEAPONS,

        //--
        //UI
        UI_CURSOR,
        UI_GAME_ICON
    }

    public enum StaticSprites
    {
        //--------
        //GRAPHICS
        //parallax
        GRAPHICS_PARALLAX_0_0,
        GRAPHICS_PARALLAX_0_1,

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
        ENTITIES_STATIC_CRATE_0,
        ENTITIES_STATIC_CRATE_1,
        ENTITIES_STATIC_PLATFORM,

        //equipment
        ENTITIES_WEAPONS_SWORD0,
        ENTITIES_WEAPONS_SWORD1,

        //--
        //UI
        UI_CURSOR,
        UI_GAME_ICON
    }


    public enum FlatBodyPreset
    {
        PLATFORM,
        CRATE_0,
        CRATE_1,
        CIRCLE,
        HUMANOID,
    }

    public enum Models
    {
        PLAYER,
        CRATE_0,
        CRATE_1,
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
