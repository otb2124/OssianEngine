using Microsoft.Xna.Framework.Audio;
using System;
using System.IO;

namespace Resources
{
    public enum Sounds
    {
        NONE,
        BODY_ARMOR_1,
        BODY_ARMOR_2,
        BODY_ARMOR_3,
        BODY_ARMOR_4,
        BODY_HAUBERK_1,
        BODY_HAUBERK_2,
        BODY_HAUBERK_3,
        BODY_HAUBERK_4,
        BODY_LOBE_1,
        BODY_LOBE_2,
        BODY_LOBE_3,
        BODY_LOBE_4,
        BOW_SHOT1,
        BOW_SHOT2,
        BOW_SHOT3,
        BOW_STANCE1,
        BREATH,
        DAMAGE1,
        DAMAGE2,
        DAMAGE3,
        DOWNS_KNEE,
        FOOT_SOIL_R1,
        FOOT_SOIL_R2,
        FOOT_SOIL_R3,
        FOOT_SOIL_R4,
        FOOT_STONE_W1,
        FOOT_STONE_W2,
        FOOT_STONE_W3,
        HUMANOID_FOOTSTEP0,
        HUMANOID_FOOTSTEP1,
        HUMANOID_FOOTSTEP2,
        HUMANOID_HURT,
        IRON_CUT_IRON,
        IRON_CUT_IRON2,
        IRON_CUT_IRON3,
        IRON_CUT_MEAT,
        IRON_CUT_MEAT2,
        MAGIC_FIRE,
        MAGIC_FORCE23,
        SWING_KATANA,
        SWING_SWORD,
        SWING_SWORD2,
        SWING_SWORD_CHARGE,
        TORCH
    }

    public enum EntitySounds
    {
        STEP,
        RECEIVEDAMAGE,
        JUMP,
        WEAPON_SWING,
    }

    public class SoundResource
    {

        public SoundEffect Effect;
        public string SoundPath;

        public SoundResource(Sounds key)
        {
            SoundPath = GetSoundPath(key);
            Load();
        }

        public string GetSoundPath(Sounds key)
        {
            switch (key)
            {
                case Sounds.NONE:
                    return "sfx/none";
                case Sounds.BODY_ARMOR_1:
                    return "sfx/Body-armor-1";
                case Sounds.BODY_ARMOR_2:
                    return "sfx/Body-armor-2";
                case Sounds.BODY_ARMOR_3:
                    return "sfx/Body-armor-3";
                case Sounds.BODY_ARMOR_4:
                    return "sfx/Body-armor-4";
                case Sounds.BODY_HAUBERK_1:
                    return "sfx/Body-hauberk-1";
                case Sounds.BODY_HAUBERK_2:
                    return "sfx/Body-hauberk-2";
                case Sounds.BODY_HAUBERK_3:
                    return "sfx/Body-hauberk-3";
                case Sounds.BODY_HAUBERK_4:
                    return "sfx/Body-hauberk-4";
                case Sounds.BODY_LOBE_1:
                    return "sfx/Body-lobe-1";
                case Sounds.BODY_LOBE_2:
                    return "sfx/Body-lobe-2";
                case Sounds.BODY_LOBE_3:
                    return "sfx/Body-lobe-3";
                case Sounds.BODY_LOBE_4:
                    return "sfx/Body-lobe-4";
                case Sounds.BOW_SHOT1:
                    return "sfx/bow-shot1";
                case Sounds.BOW_SHOT2:
                    return "sfx/bow-shot2";
                case Sounds.BOW_SHOT3:
                    return "sfx/bow-shot3";
                case Sounds.BOW_STANCE1:
                    return "sfx/bow-stance1";
                case Sounds.BREATH:
                    return "sfx/breath";
                case Sounds.DAMAGE1:
                    return "sfx/damage1";
                case Sounds.DAMAGE2:
                    return "sfx/damage2";
                case Sounds.DAMAGE3:
                    return "sfx/damage3";
                case Sounds.DOWNS_KNEE:
                    return "sfx/downs-knee";
                case Sounds.FOOT_SOIL_R1:
                    return "sfx/foot-soil-r1";
                case Sounds.FOOT_SOIL_R2:
                    return "sfx/foot-soil-r2";
                case Sounds.FOOT_SOIL_R3:
                    return "sfx/foot-soil-r3";
                case Sounds.FOOT_SOIL_R4:
                    return "sfx/foot-soil-r4";
                case Sounds.FOOT_STONE_W1:
                    return "sfx/foot-stone-w1";
                case Sounds.FOOT_STONE_W2:
                    return "sfx/foot-stone-w2";
                case Sounds.FOOT_STONE_W3:
                    return "sfx/foot-stone-w3";
                case Sounds.HUMANOID_FOOTSTEP0:
                    return "sfx/humanoid_footstep0";
                case Sounds.HUMANOID_FOOTSTEP1:
                    return "sfx/humanoid_footstep1";
                case Sounds.HUMANOID_FOOTSTEP2:
                    return "sfx/humanoid_footstep2";
                case Sounds.HUMANOID_HURT:
                    return "sfx/humanoid_hurt";
                case Sounds.IRON_CUT_IRON:
                    return "sfx/iron-cut-iron";
                case Sounds.IRON_CUT_IRON2:
                    return "sfx/iron-cut-iron2";
                case Sounds.IRON_CUT_IRON3:
                    return "sfx/iron-cut-iron3";
                case Sounds.IRON_CUT_MEAT:
                    return "sfx/iron-cut-meat";
                case Sounds.IRON_CUT_MEAT2:
                    return "sfx/iron-cut-meat2";
                case Sounds.MAGIC_FIRE:
                    return "sfx/magic-fire";
                case Sounds.MAGIC_FORCE23:
                    return "sfx/magic-force23";
                case Sounds.SWING_KATANA:
                    return "sfx/swing-katana";
                case Sounds.SWING_SWORD:
                    return "sfx/swing-sword";
                case Sounds.SWING_SWORD2:
                    return "sfx/swing-sword2";
                case Sounds.SWING_SWORD_CHARGE:
                    return "sfx/swing-sword-charge";
                case Sounds.TORCH:
                    return "sfx/torch";
                default:
                    return "sfx/humanoid-hurt";
            }
        }

        public void Load()
        {
            string soundsDirectory = Path.Combine("Content", "res", "sounds");
            string path = Path.Combine("res", "sounds", SoundPath);

            Console.WriteLine(path);

            Effect = Graphics.Graphics.contentManager.Load<SoundEffect>(path);
        }
    }
}
