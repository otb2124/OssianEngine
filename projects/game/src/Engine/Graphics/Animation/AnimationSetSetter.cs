using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Graphics
{
    public static class AnimationSetSetter
    {

        public static AnimationSet GetAnimationSetBySpriteSheet(SpriteSheets spriteSheet)
        {
            return AnimationSets.FirstOrDefault(set => set.SpriteSheet == spriteSheet);
        }


        public static AnimationSet[] AnimationSets = new AnimationSet[]
        {
            new AnimationSet(
                Utils.SpriteSheets.ENTITIES_PLAYER,
                new List<Animation>
                            {
                                //idle
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.LEFT),
                                    new AnimationFramesData(9, new Vector2(0, 0), new Vector2(64, 128), 0.1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.RIGHT),
                                    new AnimationFramesData(9, new Vector2(0, 0), new Vector2(64, 128), 0.1f)),

                                //moving
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.LEFT),
                                    new AnimationFramesData(9, new Vector2(0, 128), new Vector2(64, 128), 0.1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.RIGHT),
                                    new AnimationFramesData(9, new Vector2(0, 128), new Vector2(64, 128), 0.1f)),

                                //jumping
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*2), new Vector2(64, 128), 0.04f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*2), new Vector2(64, 128), 0.04f)),

                                //fallen
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*7), new Vector2(64, 128), 0.04f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*7), new Vector2(64, 128), 0.04f)),

                                //roll
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.LEFT),
                                    new AnimationFramesData(9, new Vector2(0, 128*6), new Vector2(64, 128), 0.15f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.RIGHT),
                                    new AnimationFramesData(9, new Vector2(0, 128*6), new Vector2(64, 128), 0.15f)),

                                //sprinting
                                new Animation(new AnimationKey(AnimationStates.SPRINTING, Directions.LEFT),
                                    new AnimationFramesData(9, new Vector2(0, 128*6), new Vector2(64, 128), 0.15f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.SPRINTING, Directions.RIGHT),
                                    new AnimationFramesData(9, new Vector2(0, 128*6), new Vector2(64, 128), 0.15f)),


                                //weapon out
                                new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_IDLE, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_IDLE, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //weapon out
                                new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_MOVING, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_MOVING, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),


                                //blocking sw
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SWORD, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*11), new Vector2(64, 128), 0.15f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SWORD, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*11), new Vector2(64, 128), 0.15f)),

                                //attacking sw l
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw ll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                 
                                //attacking sw lll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw hh
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_HEAVY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_HEAVY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw llh
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),





                                //blocking kn
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_KNIFE, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*11), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_KNIFE, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*11), new Vector2(64, 128), 0.15f)),

                                //attacking kn l
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw ll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                 
                                //attacking sw lll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),





                                //blocking bh
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_BARE_HANDS, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 128*11), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_BARE_HANDS, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 128*11), new Vector2(64, 128), 0.15f)),

                                //attacking kn l
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw ll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                 
                                //attacking sw lll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 128*8), new Vector2(64, 128), 0.15f)),
                            }
                         )
        };

    }

}
