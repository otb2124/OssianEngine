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
        GRAPHICS_BG0_CANVAS,
        GRAPHICS_BG0_B0,
        GRAPHICS_BG0_B1,
        GRAPHICS_BG0_B2,
        GRAPHICS_BG0_F0,

        GRAPHICS_BG1_CANVAS,
        GRAPHICS_BG1_B0,
        GRAPHICS_BG1_B1,
        GRAPHICS_BG1_B2,
        GRAPHICS_BG1_B3,
        GRAPHICS_BG1_B4,
        GRAPHICS_BG1_B5,
        GRAPHICS_BG1_F0,

        //static
        GRAPHICS_STATIC,
        
        //sun
        GRAPHICS_SUN,
        GRAPHICS_MOON,

        //rain
        GRAPHICS_CLOUDS,

        //--------
        //ENTITIES
        //livingentities
        ENTITIES_PLAYER,
        ENTITIES_BANDIT,
        ENTITIES_SLIME,

        //physicalentities
        ENTITIES_STATIC,

        //platforms
        ENTITIES_PLATFORMS,
        ENTITIES_TILES,

        //equipment
        ENTITIES_WEAPONS,

        //particles
        ENTITIES_PARTICLES,

        //--
        //UI
        UI_CURSOR,
        UI_FRAMES,
        UI_GAME_ICON,
        UI_ICONS,
        UI_HUD,
        UI_ITEMS,
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
        GRAPHICS_MOON,

        //rain
        GRAPHICS_CLOUD_0,

        //--------
        //ENTITIES
        //livingentities
        ENTITIES_PLAYER,
        ENTITIES_BANDIT,
        ENTITIES_SLIME,

        //physicalentities
        ENTITIES_STATIC_BALL,

        //crates
        ENTITIES_STATIC_CRATE_0,
        ENTITIES_STATIC_CRATE_1,

        //equipment
        ENTITIES_WEAPONS_TERRABLADE,
        ENTITIES_WEAPONS_IRON_SWORD,

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
        COIN,
        ITEM_DROP,
        HUMANOID,
        ANIMAL,
    }

    public enum Models
    {
        PLAYER,
        CRATE_0, CRATE_1, BALL,
        ITEM_DROP,
        BANDIT,
        SLIME,
    }

    public enum ModelStates
    {
        IDLE,
        MOVING,
        JUMPING,
        JUMPING_AND_MOVING,
        BLOCKING,
        ATTACKING_LIGHT,
        ATTACKING_HEAVY,
        SPRINTING,
        WEAPON_OUT_IDLE,
        WEAPON_OUT_MOVING,
        ROLLING,
        FALLEN,
        FALLING,
        RECEIVING_DAMAGE, 
    }

    public enum AnimationStates
    {
        IDLE,
        MOVING,
        JUMPING,
        SPRINTING,
        WEAPON_OUT_IDLE,
        BATTLE_MOVING,
        ROLL,
        FALLEN,
        RECEIVING_DAMAGE,

        ATTACKING_SWORD_LIGHT,
        ATTACKING_SWORD_LIGHT_LIGHT,
        ATTACKING_SWORD_LIGHT_LIGHT_LIGHT,
        ATTACKING_SWORD_HEAVY,
        ATTACKING_SWORD_HEAVY_HEAVY,
        ATTACKING_SWORD_LIGHT_LIGHT_HEAVY,

        ATTACKING_KNIFE_LIGHT,
        ATTACKING_KNIFE_LIGHT_LIGHT,
        ATTACKING_KNIFE_LIGHT_LIGHT_LIGHT,
        ATTACKING_KNIFE_HEAVY,
        ATTACKING_KNIFE_HEAVY_HEAVY,
        ATTACKING_KNIFE_LIGHT_HEAVY,
        ATTACKING_KNIFE_LIGHT_HEAVY_HEAVY,
    }


    public enum Directions
    {
        LEFT,
        RIGHT
    }
}
