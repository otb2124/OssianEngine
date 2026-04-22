using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
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
            return SoundPathMap[key];
        }

        public void Load()
        {
            string soundsDirectory = Path.Combine("Content", "res", "Sounds");
            string path = Path.Combine("res", "Sounds", SoundPath);
            Effect = Graphics.Graphics.ContentManager.Load<SoundEffect>(path);
        }

        public static Dictionary<Sounds, string> SoundPathMap = new Dictionary<Sounds, string>()
        {
                { Sounds.NONE,                "sfx/none" },
                { Sounds.BODY_ARMOR_1,        "sfx/Body-armor-1" },
                { Sounds.BODY_ARMOR_2,        "sfx/Body-armor-2" },
                { Sounds.BODY_ARMOR_3,        "sfx/Body-armor-3" },
                { Sounds.BODY_ARMOR_4,        "sfx/Body-armor-4" },
                { Sounds.BODY_HAUBERK_1,      "sfx/Body-hauberk-1" },
                { Sounds.BODY_HAUBERK_2,      "sfx/Body-hauberk-2" },
                { Sounds.BODY_HAUBERK_3,      "sfx/Body-hauberk-3" },
                { Sounds.BODY_HAUBERK_4,      "sfx/Body-hauberk-4" },
                { Sounds.BODY_LOBE_1,         "sfx/Body-lobe-1" },
                { Sounds.BODY_LOBE_2,         "sfx/Body-lobe-2" },
                { Sounds.BODY_LOBE_3,         "sfx/Body-lobe-3" },
                { Sounds.BODY_LOBE_4,         "sfx/Body-lobe-4" },
                { Sounds.BOW_SHOT1,           "sfx/bow-shot1" },
                { Sounds.BOW_SHOT2,           "sfx/bow-shot2" },
                { Sounds.BOW_SHOT3,           "sfx/bow-shot3" },
                { Sounds.BOW_STANCE1,         "sfx/bow-stance1" },
                { Sounds.BREATH,              "sfx/breath" },
                { Sounds.DAMAGE1,             "sfx/damage1" },
                { Sounds.DAMAGE2,             "sfx/damage2" },
                { Sounds.DAMAGE3,             "sfx/damage3" },
                { Sounds.DOWNS_KNEE,          "sfx/downs-knee" },
                { Sounds.FOOT_SOIL_R1,        "sfx/foot-soil-r1" },
                { Sounds.FOOT_SOIL_R2,        "sfx/foot-soil-r2" },
                { Sounds.FOOT_SOIL_R3,        "sfx/foot-soil-r3" },
                { Sounds.FOOT_SOIL_R4,        "sfx/foot-soil-r4" },
                { Sounds.FOOT_STONE_W1,       "sfx/foot-stone-w1" },
                { Sounds.FOOT_STONE_W2,       "sfx/foot-stone-w2" },
                { Sounds.FOOT_STONE_W3,       "sfx/foot-stone-w3" },
                { Sounds.HUMANOID_FOOTSTEP0,  "sfx/humanoid_footstep0" },
                { Sounds.HUMANOID_FOOTSTEP1,  "sfx/humanoid_footstep1" },
                { Sounds.HUMANOID_FOOTSTEP2,  "sfx/humanoid_footstep2" },
                { Sounds.HUMANOID_HURT,       "sfx/humanoid_hurt" },
                { Sounds.IRON_CUT_IRON,       "sfx/iron-cut-iron" },
                { Sounds.IRON_CUT_IRON2,      "sfx/iron-cut-iron2" },
                { Sounds.IRON_CUT_IRON3,      "sfx/iron-cut-iron3" },
                { Sounds.IRON_CUT_MEAT,       "sfx/iron-cut-meat" },
                { Sounds.IRON_CUT_MEAT2,      "sfx/iron-cut-meat2" },
                { Sounds.MAGIC_FIRE,          "sfx/magic-fire" },
                { Sounds.MAGIC_FORCE23,       "sfx/magic-force23" },
                { Sounds.SWING_KATANA,        "sfx/swing-katana" },
                { Sounds.SWING_SWORD,         "sfx/swing-sword" },
                { Sounds.SWING_SWORD2,        "sfx/swing-sword2" },
                { Sounds.SWING_SWORD_CHARGE,  "sfx/swing-sword-charge" },
                { Sounds.TORCH,               "sfx/torch" }
        };
    }
}
