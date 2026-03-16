using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Graphics
{
    public static class AnimationSetSetter
    {

        public static AnimationSet GetAnimationSetBySpriteSheet(SpriteSheets spriteSheet)
        {
            return AnimationSets.FirstOrDefault(set => set.SpriteSheet == spriteSheet);
        }

        public static AnimationSet CreateAnimationSetBySpriteSheet(SpriteSheets spriteSheet)
        {
            AnimationSet found = GetAnimationSetBySpriteSheet(spriteSheet);

            // Deep-copy each Animation so instances don't share frame state with the template
            List<Animation> copies = new List<Animation>();
            foreach (Animation anim in found.Anims)
            {
                copies.Add(new Animation(anim.AnimationKey, anim.AnimationFramesData));
            }

            return new AnimationSet(found.SpriteSheet, copies);
        }


        public static AnimationSet[] AnimationSets = new AnimationSet[]
        {
            new AnimationSet(
                SpriteSheets.ENTITIES_HUMAN_M,
                new List<Animation>
                            {
                                //idle
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*0), new Vector2(64, 64), 0.5f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*0), new Vector2(64, 64), 0.5f)),


                                 //weapon out idle
                                new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_IDLE, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*1), new Vector2(64, 64), 0.5f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_IDLE, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*1), new Vector2(64, 64), 0.5f)),

                                //moving
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*2), new Vector2(64, 64), 0.1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*2), new Vector2(64, 64), 0.1f)),

                                //weapon out moving
                                new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_MOVING, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*3), new Vector2(64, 64), 0.1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_MOVING, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*3), new Vector2(64, 64), 0.1f)),

                                //sprinting
                                new Animation(new AnimationKey(AnimationStates.SPRINTING, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*4), new Vector2(64, 64), 0.1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.SPRINTING, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*4), new Vector2(64, 64), 0.1f)),

                                //roll
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 64*5), new Vector2(64, 64), 0.1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 64*5), new Vector2(64, 64), 0.1f)),

                                //jumping
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*6), new Vector2(64, 64), 0.5f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*6), new Vector2(64, 64), 0.5f)),

                                //jumping desc
                                new Animation(new AnimationKey(AnimationStates.JUMPING_DESCENDING, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 64*7), new Vector2(64, 64), 0.5f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.JUMPING_DESCENDING, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 64*7), new Vector2(64, 64), 0.5f)),

                                //desc
                                new Animation(new AnimationKey(AnimationStates.DESCENDING, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 64*8), new Vector2(64, 64), 0.5f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.DESCENDING, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 64*8), new Vector2(64, 64), 0.5f)),

                                //fallen
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 64*9), new Vector2(64, 64), 0.5f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 64*9), new Vector2(64, 64), 0.5f)),

                                //hang
                                new Animation(new AnimationKey(AnimationStates.HANGING, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 64*10), new Vector2(64, 64), 0.5f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.HANGING, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 64*10), new Vector2(64, 64), 0.5f)),

                                //hang alt
                                new Animation(new AnimationKey(AnimationStates.HANGING_ALT, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 64*11), new Vector2(64, 64), 0.5f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.HANGING_ALT, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 64*11), new Vector2(64, 64), 0.5f)),

                                //receiving dmg
                                new Animation(new AnimationKey(AnimationStates.RECEIVING_DAMAGE, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 64*12), new Vector2(64, 64), 0.5f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.RECEIVING_DAMAGE, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 64*12), new Vector2(64, 64), 0.5f)),

                                //die
                                //new Animation(new AnimationKey(AnimationStates.RECEIVING_DAMAGE, Directions.LEFT),
                                //    new AnimationFramesData(2, new Vector2(0, 64*13), new Vector2(64, 64), 0.5f, SpriteEffects.FlipHorizontally)),
                                //new Animation(new AnimationKey(AnimationStates.RECEIVING_DAMAGE, Directions.RIGHT),
                                //    new AnimationFramesData(2, new Vector2(0, 64*13), new Vector2(64, 64), 0.5f)),


                                //blocking bh b
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_BARE_HANDS, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 64*14), new Vector2(64, 64), 0.5f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_BARE_HANDS, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 64*14), new Vector2(64, 64), 0.5f)),

                                //attacking bh l
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f)),

                                //attacking bh ll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f)),
                                 
                                //attacking bh lll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f)),

                                //attacking bh h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f)),

                                //attacking bh lh
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f)),

                                //attacking bh lhh
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BARE_HANDS_LIGHT_HEAVY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f)),




                                //blocking sw
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SWORD, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 64*14), new Vector2(64, 64), 0.5f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SWORD, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 64*14), new Vector2(64, 64), 0.5f)),

                                //attacking sw l
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*16), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*16), new Vector2(64, 64), 1f)),

                                //attacking sw ll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*16), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*16), new Vector2(64, 64), 1f)),
                                 
                                //attacking sw lll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*16), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*16), new Vector2(64, 64), 1f)),

                                //attacking sw h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*16), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*16), new Vector2(64, 64), 1f)),

                                //attacking sw hh
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_HEAVY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*16), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_HEAVY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*16), new Vector2(64, 64), 1f)),

                                //attacking sw llh
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*16), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SWORD_LIGHT_LIGHT_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*16), new Vector2(64, 64), 1f)),





                                //blocking knife
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_KNIFE, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 64*14), new Vector2(64, 64), 0.5f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_KNIFE, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 64*14), new Vector2(64, 64), 0.5f)),

                                //attacking knife l
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f)),

                                //attacking knife ll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f)),
                                 
                                //attacking knife lll
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f)),

                                //attacking knife h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f)),

                                //attacking knife lh
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f)),

                                //attacking knife lhh
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_KNIFE_LIGHT_HEAVY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*15), new Vector2(64, 64), 1f)),

                                //attacking spell l
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SPELL_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*17), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SPELL_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*17), new Vector2(64, 64), 1f)),

                                //attacking spell h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SPELL_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*17), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SPELL_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*17), new Vector2(64, 64), 1f)),


                                //attacking bow l
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BOW_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*18), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BOW_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*18), new Vector2(64, 64), 1f)),

                                //attacking bow h
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BOW_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 64*18), new Vector2(64, 64), 1f, SpriteEffects.FlipHorizontally)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_BOW_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 64*18), new Vector2(64, 64), 1f)),
                            }
                         ),



                        new AnimationSet
                        (
                            SpriteSheets.ENTITIES_SLIME,
                            new List<Animation>
                            {
                                //idle
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 0), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 0), new Vector2(64, 64), 0.5f)),

                                //moving
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //jumping
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                
                                //roll
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //fallen
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SLIME_BODY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SLIME_BODY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                            }
                        ),


                        new AnimationSet
                        (
                            SpriteSheets.ENTITIES_BAT,
                            new List<Animation>
                            {
                                //idle
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 0), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 0), new Vector2(64, 64), 0.5f)),

                                //moving
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.LEFT),
                                    new AnimationFramesData(2, new Vector2(0, 64), new Vector2(0, -16), new Vector2(64, 64), Vector2.Zero, 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.RIGHT),
                                    new AnimationFramesData(2, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //jumping
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.JUMPING, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                
                                //fly
                                new Animation(new AnimationKey(AnimationStates.FLYING, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.FLYING, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //fly move
                                new Animation(new AnimationKey(AnimationStates.FLYING_AND_MOVING, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.FLYING_AND_MOVING, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //roll
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //fallen
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.FALLEN, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SLIME_BODY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.BLOCKING_SLIME_BODY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT_LIGHT, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_LIGHT_LIGHT_LIGHT, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),

                                //attacking
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY_HEAVY, Directions.LEFT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                                new Animation(new AnimationKey(AnimationStates.ATTACKING_SLIME_BODY_HEAVY_HEAVY, Directions.RIGHT),
                                    new AnimationFramesData(3, new Vector2(0, 64), new Vector2(64, 64), 0.5f)),
                            }
                        ),


                        new AnimationSet
                        (
                            SpriteSheets.ENITIES_FIREBALL,
                            new List<Animation>
                            {
                                //idle
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 0), new Vector2(32, 32), 0.05f)),
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 0), new Vector2(32, 32), 0.05f)),

                                //idle
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 0), new Vector2(32, 32), 0.05f)),
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 0), new Vector2(32, 32), 0.05f)),

                                //idle
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 0), new Vector2(32, 32), 0.05f)),
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.RIGHT),
                                    new AnimationFramesData(4, new Vector2(0, 0), new Vector2(32, 32), 0.05f)),
                            }
                        ),

                        new AnimationSet
                        (
                            SpriteSheets.ENITIES_ARROW,
                            new List<Animation>
                            {
                                //idle
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 0), new Vector2(32, 32), 0.05f)),
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 0), new Vector2(32, 32), 0.05f)),

                                //idle
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 0), new Vector2(32, 32), 0.05f)),
                                new Animation(new AnimationKey(AnimationStates.MOVING, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 0), new Vector2(32, 32), 0.05f)),

                                //idle
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.LEFT),
                                    new AnimationFramesData(1, new Vector2(0, 0), new Vector2(32, 32), 0.05f)),
                                new Animation(new AnimationKey(AnimationStates.ROLL, Directions.RIGHT),
                                    new AnimationFramesData(1, new Vector2(0, 0), new Vector2(32, 32), 0.05f)),
                            }
                        ),


                        //vfx
                        new AnimationSet
                        (
                            SpriteSheets.VFX_EXPLOSION,
                            new List<Animation>
                            {
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.LEFT),
                                    new AnimationFramesData(6, new Vector2(0, 0), new Vector2(32, 32), 0.05f)),
                            }
                        ),

                        new AnimationSet
                        (
                            SpriteSheets.VFX_WATER_STEP,
                            new List<Animation>
                            {
                                new Animation(new AnimationKey(AnimationStates.IDLE, Directions.LEFT),
                                    new AnimationFramesData(4, new Vector2(0, 0), new Vector2(32, 32), 0.075f)),
                            }
                        ),
        };


    }

}
