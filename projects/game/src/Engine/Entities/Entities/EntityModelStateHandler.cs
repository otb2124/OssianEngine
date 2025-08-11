using Physics;
using System;
using Utils;

namespace Entities {
    public static class EntityModelStateHandler
    {

        public static void Update(StatsEntity Entity)
        {
            ModelStates state = Entity.Model.ModelState;
            int directionXFactor = Entity.Model.Direction == Directions.RIGHT ? 1 : -1;

            if (state == ModelStates.IDLE || state == ModelStates.WEAPON_OUT_IDLE)
            {
                Entity.Stats.staminaPerAttackHitSpent = false;
            }

            if (state == ModelStates.MOVING || state == ModelStates.WEAPON_OUT_MOVING)
            {
                Entity.Model.Body.Move(new FlatVector(Entity.Stats.speed * directionXFactor, 0));
            }

            if (state == ModelStates.JUMPING)
            {
                Entity.Model.Body.Jump(Entity.Stats.jumpSpeed);
                Entity.Stats.stamina -= Entity.Stats.staminaJumpCostSec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.Stats.AllowJumpDescendingLock = true;
                Entity.Model.Body.IsFrozen = false;
            }

            if (state == ModelStates.JUMPING_AND_MOVING)
            {
                Entity.Model.Body.Jump(Entity.Stats.jumpSpeed);
                Entity.Stats.stamina -= Entity.Stats.staminaJumpCostSec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.Model.Body.Move(new FlatVector(Entity.Stats.speed * directionXFactor, 0));
                Entity.Stats.AllowJumpDescendingLock = true;
                Entity.Model.Body.IsFrozen = false;
            }

            if (state == ModelStates.SPRINTING)
            {
                Entity.Model.Body.Move(new FlatVector(Entity.Stats.speed * Entity.Stats.sprintMultiplier * directionXFactor, 0));
                Entity.Stats.stamina -= Entity.Stats.staminaSprintCostSec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.Stats.OnUsingStamina = true;
            }

            if (state == ModelStates.BLOCKING)
            {
                Entity.Stats.OnUsingStamina = true;
            }

            if (state == ModelStates.ROLLING)
            {
                Entity.Stats.stamina -= Entity.Stats.staminaRollCostSec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.Model.Body.Move(new FlatVector(Entity.Stats.speed * Entity.Stats.rollMultiplier * directionXFactor, 0));
            }

            if (state == ModelStates.JUMPING_DESCENDING)
            {
                Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;
                Entity.Model.Body.linearVelocity -= new FlatVector(0, Entity.Stats.DescendingMultiplier * 200);
            }

            if (state == ModelStates.JUMPING_DESCENDING_AND_MOVING)
            {
                Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;
                Entity.Model.Body.linearVelocity -= new FlatVector(0, Entity.Stats.DescendingMultiplier * 200);
                Entity.Model.Body.Move(new FlatVector(Entity.Stats.speed * directionXFactor, 0));
            }

            if (state == ModelStates.HANGING_ON_LEDGE)
            {
                Entity.Model.Body.linearVelocity = FlatVector.Zero;
                Entity.Model.Body.IsFrozen = true;
            }
            else
            {
                Entity.Model.Body.IsFrozen = false;
            }

            if (Entity is EquipmentEntity eqEnt)
            {
                if (state == ModelStates.ATTACKING_LIGHT)
                {
                    if (!eqEnt.Stats.staminaPerAttackHitSpent)
                    {
                        eqEnt.Stats.stamina -= eqEnt.Stats.staminaAttackHitCost;
                        eqEnt.Stats.staminaPerAttackHitSpent = true;
                    }

                }

                if (state == ModelStates.ATTACKING_HEAVY)
                {
                    if (!eqEnt.Stats.staminaPerAttackHitSpent)
                    {
                        eqEnt.Stats.stamina -= eqEnt.Stats.staminaAttackHitCost;
                        eqEnt.Stats.staminaPerAttackHitSpent = true;
                    }
                }

                if (state == ModelStates.BLOCKING)
                {
                    if (!eqEnt.Stats.staminaPerAttackHitSpent)
                    {

                    }
                    
                    //Stats.stamina -= Stats.staminaRollCostSec / 60;
                }
            }
            
        }


        public static void UpdatePlayerModelState(Player player)
        {
            // WEAPON TOGGLE
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.TOGGLEWEAPONPRESSED])
            {
                player.EquipmentManager.IsWeaponOut = !player.EquipmentManager.IsWeaponOut;
            }

            // ATTACK
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.ATTACKLIGHTPRESSED] 
                && !KeyHandlerUtil.isPlayerMoving() 
                && player.Model.ModelState != ModelStates.JUMPING_DESCENDING 
                && player.Model.ModelState != ModelStates.JUMPING_DESCENDING_AND_MOVING
                && player.Model.ModelState != ModelStates.JUMPING
                && player.Model.ModelState != ModelStates.JUMPING_AND_MOVING
                && player.Model.ModelState != ModelStates.OVERALL_DESCENDING
                && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
            {
                if (player.Stats.stamina - player.Stats.staminaAttackHitCost > 0)
                {
                    player.Model.ModelState = ModelStates.ATTACKING_LIGHT;
                }
            }
            else if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.ATTACKHEAVYPRESSED]
                && !KeyHandlerUtil.isPlayerMoving()
                && player.Model.ModelState != ModelStates.JUMPING_DESCENDING
                && player.Model.ModelState != ModelStates.JUMPING_DESCENDING_AND_MOVING
                && player.Model.ModelState != ModelStates.JUMPING
                && player.Model.ModelState != ModelStates.JUMPING_AND_MOVING
                && player.Model.ModelState != ModelStates.OVERALL_DESCENDING
                && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
            {
                if (player.Stats.stamina - player.Stats.staminaAttackHitCost > 0)
                {
                    player.Model.ModelState = ModelStates.ATTACKING_HEAVY;
                }
            }
            //BLOCK
            else if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.BLOCKPRESSED]
                && !KeyHandlerUtil.isPlayerMoving()
                && player.Model.ModelState != ModelStates.JUMPING_DESCENDING
                && player.Model.ModelState != ModelStates.JUMPING_DESCENDING_AND_MOVING
                && player.Model.ModelState != ModelStates.JUMPING
                && player.Model.ModelState != ModelStates.JUMPING_AND_MOVING
                && player.Model.ModelState != ModelStates.OVERALL_DESCENDING
                && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
            {
                if (player.Stats.stamina > 0)
                {
                    if (player.Stats.stamina - player.Stats.staminaRollCostSec / (float)Graphics.Graphics.UpdatesPerSecond > 0)
                    {
                        player.Model.ModelState = ModelStates.BLOCKING;
                    }
                }
            }
            //BLOCK RESET
            else if (player.Model.ModelState == ModelStates.BLOCKING
                && !Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.BLOCKPRESSED])
            {
                player.Model.ModelState = player.EquipmentManager.IsWeaponOut ? ModelStates.WEAPON_OUT_IDLE : ModelStates.IDLE;
            }


            // Handle movement and other states
            if (KeyHandlerUtil.isPlayerMoving() &&
                player.Model.ModelState != ModelStates.ATTACKING_LIGHT &&
                player.Model.ModelState != ModelStates.ATTACKING_HEAVY &&
                player.Model.ModelState != ModelStates.FALLEN)
            {
                // DIRECTION
                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
                {
                    player.Model.Direction = Directions.RIGHT;
                }
                else if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
                {
                    player.Model.Direction = Directions.LEFT;
                }

                // JUMP
                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED])
                {
                    if (player.Stats.stamina - player.Stats.staminaJumpCostSec > 0 && (player.Stats.IsGrounded || player.Model.ModelState == ModelStates.HANGING_ON_LEDGE))
                    {
                        player.Model.ModelState = ModelStates.JUMPING_AND_MOVING;
                    }
                }

                // JUMPING_DESCENDING
                if ((player.Model.ModelState == ModelStates.JUMPING ||
                     player.Model.ModelState == ModelStates.JUMPING_AND_MOVING ||
                     player.Model.ModelState == ModelStates.JUMPING_DESCENDING) &&
                     !player.Stats.IsGrounded &&
                     player.Stats.AllowJumpDescending)
                {
                    player.Model.ModelState = ModelStates.JUMPING_DESCENDING_AND_MOVING;
                }

                if (player.Stats.IsTouchingCeiling || (!player.Stats.IsGrounded && !player.Stats.AllowJumpDescending && player.Model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING))
                {
                    player.Model.ModelState = ModelStates.OVERALL_DESCENDING;
                }


                // SPRINT BLOCK ROLL
                if (player.Model.ModelState != ModelStates.JUMPING &&
                    player.Model.ModelState != ModelStates.JUMPING_AND_MOVING &&
                    player.Model.ModelState != ModelStates.JUMPING_DESCENDING &&
                    player.Model.ModelState != ModelStates.JUMPING_DESCENDING_AND_MOVING)
                {

                    // SPRINT
                    if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.SPRINTPRESSED] &&
                    player.Model.ModelState != ModelStates.OVERALL_DESCENDING && player.Stats.IsGrounded)
                    {
                        if (player.Stats.stamina - player.Stats.staminaSprintCostSec / (float)Graphics.Graphics.UpdatesPerSecond > 0 &&
                            !player.Stats.OnStaminaRegen)
                        {
                            player.Model.ModelState = ModelStates.SPRINTING;
                        }
                    }

                    //ROLL
                    else if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.BLOCKPRESSED] &&
                    player.Model.ModelState != ModelStates.OVERALL_DESCENDING)
                    {
                        if (player.Stats.stamina > 0)
                        {
                            if (player.Stats.stamina - player.Stats.staminaRollCostSec / (float)Graphics.Graphics.UpdatesPerSecond > 0)
                            {
                                player.Model.ModelState = ModelStates.ROLLING;
                            }
                        }
                    }

                    //MOVE
                    else
                    {
                        if(player.Stats.IsGrounded && !player.Stats.AllowJumpDescending && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
                        {
                            player.Model.ModelState = player.EquipmentManager.IsWeaponOut ? ModelStates.WEAPON_OUT_MOVING : ModelStates.MOVING;
                        }
                        else if(!player.Stats.IsGrounded && !player.Stats.AllowJumpDescending && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
                        {
                            player.Model.ModelState = ModelStates.OVERALL_DESCENDING;
                        }
                    }
                }

                if (player.Stats.IsGrounded && !player.Stats.AllowJumpDescending && (player.Model.ModelState == ModelStates.JUMPING_DESCENDING || player.Model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING))
                {
                    player.Model.ModelState = player.EquipmentManager.IsWeaponOut ? ModelStates.WEAPON_OUT_IDLE : ModelStates.IDLE;
                }
            }
            // NO MOVEMENT
            else if (player.Model.ModelState != ModelStates.ATTACKING_LIGHT &&
                     player.Model.ModelState != ModelStates.ATTACKING_HEAVY &&
                     player.Model.ModelState != ModelStates.BLOCKING)
            {
                // Handle descending when not moving
                if ((player.Model.ModelState == ModelStates.JUMPING ||
                     player.Model.ModelState == ModelStates.JUMPING_AND_MOVING) &&
                    !player.Stats.IsGrounded &&
                    player.Stats.AllowJumpDescending)
                {
                    player.Model.ModelState = ModelStates.JUMPING_DESCENDING;
                }

                    if (player.Stats.IsGrounded && !player.Stats.AllowJumpDescending && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
                    {
                        player.Model.ModelState = player.EquipmentManager.IsWeaponOut ? ModelStates.WEAPON_OUT_IDLE : ModelStates.IDLE;
                    }
                    else if (player.Stats.IsTouchingCeiling || (!player.Stats.IsGrounded && !player.Stats.AllowJumpDescending && player.Model.ModelState != ModelStates.JUMPING_AND_MOVING && player.Model.ModelState != ModelStates.JUMPING && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE))
                    {
                        player.Model.ModelState = ModelStates.OVERALL_DESCENDING;
                    }

                    // JUMP
                    if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED])
                    {
                        if (player.Stats.stamina - player.Stats.staminaJumpCostSec > 0)
                        {
                            player.Model.ModelState = ModelStates.JUMPING;
                        }
                    }

                //Console.WriteLine(player.Model.ModelState);
            }
        }
    }
}
