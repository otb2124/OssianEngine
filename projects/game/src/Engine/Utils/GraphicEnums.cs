using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public enum SpriteSheets
    {
        NONE,
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
        ENTITIES_HUMAN_M,
        ENTITIES_HUMAN_M_ARMOR_CHESTPLATE_0,
        ENTITIES_HUMAN_M_ARMOR_HELMET_0,
        ENTITIES_HUMAN_M_ARMOR_BOOTS_0,
        ENTITIES_HUMAN_M_ARMOR_GLOVES_0,

        ENTITIES_SLIME, ENTITIES_BAT,

        ENITIES_FIREBALL, ENITIES_ARROW,

        //physicalentities
        ENTITIES_STATIC,

        //platforms
        ENTITIES_PLATFORMS,
        ENTITIES_TILES,
        ENTITIES_LEDGES,

        //equipment
        ENTITIES_WEAPONS_TERRABLADE,
        ENTITIES_WEAPONS_TORCH,

        //particles
        ENTITIES_PARTICLES,

        //light
        LIGHT_DARKNESS_FULL,
        LIGHT_DARKNESS_MIN,

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
        NONE,
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
        ENTITIES_HUMAN_M,
        ENTITIES_HUMAN_M_ARMOR_CHESTPLATE_0,
        ENTITIES_HUMAN_M_ARMOR_HELMET_0,
        ENTITIES_HUMAN_M_ARMOR_BOOTS_0,
        ENTITIES_HUMAN_M_ARMOR_GLOVES_0,

        ENTITIES_SLIME, ENTITIES_BAT,

        ENTITIES_FIREBALL, ENTITIES_ARROW,

        //physicalentities
        ENTITIES_STATIC_BALL,

        //ledges
        ENTITIES_LEDGE,

        //crates
        ENTITIES_STATIC_CRATE_0,
        ENTITIES_STATIC_CRATE_1,

        //equipment
        ENTITIES_WEAPONS_TERRABLADE,
        ENTITIES_WEAPONS_TORCH,

        //light
        LIGHT_DARKNESS_FULL,
        LIGHT_DARKNESS_VIGNETTE,

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
        LEDGE,
        PROJECTILE
    }

    

    public enum ModelStates
    {
        IDLE,
        MOVING,
        JUMPING,
        JUMPING_AND_MOVING,
        DOUBLE_JUMPING,
        DOUBLE_JUMPING_AND_MOVING,
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
        JUMPING_DESCENDING,
        JUMPING_DESCENDING_AND_MOVING,
        DESCENDING,
        HANGING_ON_LEDGE,

        FLYING,
        FLYING_AND_MOVING,
    }

    public enum AnimationStates
    {
        IDLE,
        MOVING,
        JUMPING,
        SPRINTING,
        WEAPON_OUT_IDLE,
        WEAPON_OUT_MOVING,
        ROLL,
        FALLEN,
        RECEIVING_DAMAGE,
        JUMPING_DESCENDING,
        DESCENDING,
        HANGING_ALT,
        HANGING,
        FLYING,
        FLYING_AND_MOVING,

        BLOCKING_SWORD,

        ATTACKING_SWORD_LIGHT,
        ATTACKING_SWORD_LIGHT_LIGHT,
        ATTACKING_SWORD_LIGHT_LIGHT_LIGHT,
        ATTACKING_SWORD_HEAVY,
        ATTACKING_SWORD_HEAVY_HEAVY,
        ATTACKING_SWORD_LIGHT_LIGHT_HEAVY,

        BLOCKING_KNIFE,

        ATTACKING_KNIFE_LIGHT,
        ATTACKING_KNIFE_LIGHT_LIGHT,
        ATTACKING_KNIFE_LIGHT_LIGHT_LIGHT,
        ATTACKING_KNIFE_HEAVY,
        ATTACKING_KNIFE_LIGHT_HEAVY,
        ATTACKING_KNIFE_LIGHT_HEAVY_HEAVY,

        BLOCKING_BARE_HANDS,

        ATTACKING_BARE_HANDS_LIGHT,
        ATTACKING_BARE_HANDS_LIGHT_LIGHT,
        ATTACKING_BARE_HANDS_LIGHT_LIGHT_LIGHT,
        ATTACKING_BARE_HANDS_HEAVY,
        ATTACKING_BARE_HANDS_LIGHT_HEAVY,
        ATTACKING_BARE_HANDS_LIGHT_HEAVY_HEAVY,

        BLOCKING_SLIME_BODY,

        ATTACKING_SLIME_BODY_LIGHT,
        ATTACKING_SLIME_BODY_LIGHT_LIGHT,
        ATTACKING_SLIME_BODY_LIGHT_LIGHT_LIGHT,
        ATTACKING_SLIME_BODY_HEAVY,
        ATTACKING_SLIME_BODY_HEAVY_HEAVY,

        BLOCKING_BAT_BODY,

        ATTACKING_BAT_BODY_LIGHT,
        ATTACKING_BAT_BODY_LIGHT_LIGHT,
        ATTACKING_BAT_BODY_LIGHT_LIGHT_LIGHT,
        ATTACKING_BAT_BODY_HEAVY,
        ATTACKING_BAT_BODY_HEAVY_HEAVY,
    }


    public enum Directions
    {
        LEFT,
        RIGHT
    }
}
