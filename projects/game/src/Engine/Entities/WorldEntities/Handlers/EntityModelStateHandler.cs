using Physics;
using System;
using System.Net;
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
                Entity.StatsManager.statsPerAttackHitSpent = false;
            }

            if (state == ModelStates.MOVING || state == ModelStates.WEAPON_OUT_MOVING)
            {
                Entity.Model.Body.Move(new FlatVector(Entity.StatsManager.speed * directionXFactor, 0));
            }

            if (state == ModelStates.JUMPING)
            {
                Entity.Model.Body.Jump(Entity.StatsManager.jumpSpeed);
                Entity.StatsManager.IndicatorStats.Stamina -= Entity.StatsManager.staminaJumpCostSec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.StatsManager.AllowJumpDescendingLock = true;
                Entity.Model.Body.IsFrozen = false;
            }

            if (state == ModelStates.JUMPING_AND_MOVING)
            {
                Entity.Model.Body.Jump(Entity.StatsManager.jumpSpeed);
                Entity.StatsManager.IndicatorStats.Stamina -= Entity.StatsManager.staminaJumpCostSec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.Model.Body.Move(new FlatVector(Entity.StatsManager.speed * directionXFactor, 0));
                Entity.StatsManager.AllowJumpDescendingLock = true;
                Entity.Model.Body.IsFrozen = false;
            }

            if (state == ModelStates.FLYING)
            {
                Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;

                if(Entity.StatsManager.FlyingUpwards)
                {
                    Entity.Model.Body.Jump(Entity.StatsManager.flySpeed);
                }
                else
                {
                    Entity.Model.Body.Jump(-Entity.StatsManager.flySpeed);
                }
                
                Entity.StatsManager.IndicatorStats.Stamina -= Entity.StatsManager.staminaJumpCostSec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.StatsManager.AllowJumpDescendingLock = true;
                Entity.Model.Body.IsFrozen = false;
            }

            if (state == ModelStates.FLYING_AND_MOVING)
            {
                Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;

                if (Entity.StatsManager.FlyingUpwards)
                {
                    Entity.Model.Body.Jump(Entity.StatsManager.flySpeed);
                }
                else
                {
                    Entity.Model.Body.Jump(-Entity.StatsManager.flySpeed);
                }

                Entity.StatsManager.IndicatorStats.Stamina -= Entity.StatsManager.staminaJumpCostSec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.StatsManager.AllowJumpDescendingLock = true;
                Entity.Model.Body.IsFrozen = false;

                Entity.Model.Body.Move(new FlatVector(Entity.StatsManager.speed * directionXFactor, 0));
            }

            if (state == ModelStates.SPRINTING)
            {
                if (Entity.StatsManager.IndicatorStats.Stamina - Entity.StatsManager.staminaSprintCostSec / (float)Graphics.Graphics.UpdatesPerSecond > 0)
                {
                    Entity.Model.Body.Move(new FlatVector(Entity.StatsManager.speed * Entity.StatsManager.sprintMultiplier * directionXFactor, 0));
                    Entity.StatsManager.IndicatorStats.Stamina -= Entity.StatsManager.staminaSprintCostSec / (float)Graphics.Graphics.UpdatesPerSecond;
                    Entity.StatsManager.OnUsingStamina = true;
                }
                else
                {
                    Entity.Model.ModelState = ModelStates.IDLE;
                }
            }

            if (state == ModelStates.BLOCKING)
            {
                Entity.StatsManager.OnUsingStamina = true;
            }

            if (state == ModelStates.ROLLING)
            {
                if (Entity.StatsManager.IndicatorStats.Stamina - Entity.StatsManager.staminaRollCostSec / (float)Graphics.Graphics.UpdatesPerSecond > 0)
                {
                    Entity.StatsManager.IndicatorStats.Stamina -= Entity.StatsManager.staminaRollCostSec / (float)Graphics.Graphics.UpdatesPerSecond;
                    Entity.Model.Body.Move(new FlatVector(Entity.StatsManager.speed * Entity.StatsManager.rollMultiplier * directionXFactor, 0));
                }
                else
                {
                    Entity.Model.ModelState = ModelStates.IDLE;
                }
            }

            if (state == ModelStates.JUMPING_DESCENDING)
            {
                Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;
                Entity.Model.Body.linearVelocity -= new FlatVector(0, Entity.StatsManager.DescendingMultiplier * 200);
            }

            if (state == ModelStates.JUMPING_DESCENDING_AND_MOVING)
            {
                Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;
                Entity.Model.Body.linearVelocity -= new FlatVector(0, Entity.StatsManager.DescendingMultiplier * 200);
                Entity.Model.Body.Move(new FlatVector(Entity.StatsManager.speed * directionXFactor, 0));
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
                if (state == ModelStates.ATTACKING_LIGHT || state == ModelStates.ATTACKING_HEAVY)
                {
                    if (!eqEnt.StatsManager.statsPerAttackHitSpent)
                    {
                        eqEnt.StatsManager.SpendStatsForBattleHit(eqEnt);
                    }

                }

                if (state == ModelStates.BLOCKING)
                {
                    if (!eqEnt.StatsManager.statsPerAttackHitSpent)
                    {

                    }
                    
                    //StatsManager.Stamina -= StatsManager.staminaRollCostSec / 60;
                }
            }
            
        }


        public static void UpdatePlayerModelState(Player player)
        {
            // WEAPON TOGGLE
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.TOGGLEWEAPONPRESSED])
            {
                player.EquipmentManager.ToggleWeaponInOut(player.BattleBodyManager);
            }

            // ATTACK
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.ATTACKLIGHTPRESSED] && !UI.UI.PreventButtonPressedOverlap
                && !KeyHandlerUtil.isPlayerMoving() 
                && player.Model.ModelState != ModelStates.JUMPING_DESCENDING 
                && player.Model.ModelState != ModelStates.JUMPING_DESCENDING_AND_MOVING
                && player.Model.ModelState != ModelStates.JUMPING
                && player.Model.ModelState != ModelStates.JUMPING_AND_MOVING
                && player.Model.ModelState != ModelStates.OVERALL_DESCENDING
                && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
            {
                if (player.StatsManager.IndicatorStats.Stamina - BattleStatsCalculator.GetFinalStaminaPerHitCostForBattleEntity(player) > 0 &&
                    player.StatsManager.IndicatorStats.Mana - BattleStatsCalculator.GetFinalManaPerHitCostForBattleEntity(player) > 0)
                {
                    player.Model.ModelState = ModelStates.ATTACKING_LIGHT;
                }
            }
            else if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.ATTACKHEAVYPRESSED] && !UI.UI.PreventButtonPressedOverlap
                && !KeyHandlerUtil.isPlayerMoving()
                && player.Model.ModelState != ModelStates.JUMPING_DESCENDING
                && player.Model.ModelState != ModelStates.JUMPING_DESCENDING_AND_MOVING
                && player.Model.ModelState != ModelStates.JUMPING
                && player.Model.ModelState != ModelStates.JUMPING_AND_MOVING
                && player.Model.ModelState != ModelStates.OVERALL_DESCENDING
                && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
            {
                if (player.StatsManager.IndicatorStats.Stamina - player.StatsManager.staminaAttackHitCostMultiplier > 0)
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
                if (player.StatsManager.IndicatorStats.Stamina > 0)
                {
                    if (player.StatsManager.IndicatorStats.Stamina - player.StatsManager.staminaRollCostSec / (float)Graphics.Graphics.UpdatesPerSecond > 0)
                    {
                        player.Model.ModelState = ModelStates.BLOCKING;
                    }
                }
            }
            //BLOCK RESET
            else if (player.Model.ModelState == ModelStates.BLOCKING
                && !Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.BLOCKPRESSED])
            {
                player.Model.ModelState = player.EquipmentManager.WeaponInOutToggler.IsWeaponOut ? ModelStates.WEAPON_OUT_IDLE : ModelStates.IDLE;
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
                    if (player.StatsManager.IndicatorStats.Stamina - player.StatsManager.staminaJumpCostSec > 0 && (player.StatsManager.IsGrounded || player.Model.ModelState == ModelStates.HANGING_ON_LEDGE))
                    {
                        player.Model.ModelState = ModelStates.JUMPING_AND_MOVING;
                    }
                }

                // JUMPING_DESCENDING
                if ((player.Model.ModelState == ModelStates.JUMPING ||
                     player.Model.ModelState == ModelStates.JUMPING_AND_MOVING ||
                     player.Model.ModelState == ModelStates.JUMPING_DESCENDING) &&
                     !player.StatsManager.IsGrounded &&
                     player.StatsManager.AllowJumpDescending)
                {
                    player.Model.ModelState = ModelStates.JUMPING_DESCENDING_AND_MOVING;
                }

                if (player.StatsManager.IsTouchingCeiling || (!player.StatsManager.IsGrounded && !player.StatsManager.AllowJumpDescending && player.Model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING))
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
                    player.Model.ModelState != ModelStates.OVERALL_DESCENDING && player.StatsManager.IsGrounded)
                    {
                        if (player.StatsManager.IndicatorStats.Stamina - player.StatsManager.staminaSprintCostSec / (float)Graphics.Graphics.UpdatesPerSecond > 0 &&
                            !player.StatsManager.OnStaminaRegen)
                        {
                            player.Model.ModelState = ModelStates.SPRINTING;
                        }
                    }

                    //ROLL
                    else if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.BLOCKPRESSED] &&
                    player.Model.ModelState != ModelStates.OVERALL_DESCENDING)
                    {
                        if (player.StatsManager.IndicatorStats.Stamina - player.StatsManager.staminaRollCostSec / (float)Graphics.Graphics.UpdatesPerSecond > 0 &&
                            !player.StatsManager.OnStaminaRegen)
                        {
                            player.Model.ModelState = ModelStates.ROLLING;
                        }
                    }

                    //MOVE
                    else
                    {
                        if(player.StatsManager.IsGrounded && !player.StatsManager.AllowJumpDescending && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
                        {
                            player.Model.ModelState = player.EquipmentManager.WeaponInOutToggler.IsWeaponOut ? ModelStates.WEAPON_OUT_MOVING : ModelStates.MOVING;
                        }
                        else if(!player.StatsManager.IsGrounded && !player.StatsManager.AllowJumpDescending && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
                        {
                            player.Model.ModelState = ModelStates.OVERALL_DESCENDING;
                        }
                    }
                }

                if (player.StatsManager.IsGrounded && !player.StatsManager.AllowJumpDescending && (player.Model.ModelState == ModelStates.JUMPING_DESCENDING || player.Model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING))
                {
                    player.Model.ModelState = player.EquipmentManager.WeaponInOutToggler.IsWeaponOut ? ModelStates.WEAPON_OUT_IDLE : ModelStates.IDLE;
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
                    !player.StatsManager.IsGrounded &&
                    player.StatsManager.AllowJumpDescending)
                {
                    player.Model.ModelState = ModelStates.JUMPING_DESCENDING;
                }

                    if (player.StatsManager.IsGrounded && !player.StatsManager.AllowJumpDescending && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
                    {
                        player.Model.ModelState = player.EquipmentManager.WeaponInOutToggler.IsWeaponOut ? ModelStates.WEAPON_OUT_IDLE : ModelStates.IDLE;
                    }
                    else if (player.StatsManager.IsTouchingCeiling || (!player.StatsManager.IsGrounded && !player.StatsManager.AllowJumpDescending && player.Model.ModelState != ModelStates.JUMPING_AND_MOVING && player.Model.ModelState != ModelStates.JUMPING && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE))
                    {
                        player.Model.ModelState = ModelStates.OVERALL_DESCENDING;
                    }

                    // JUMP
                    if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED])
                    {
                        if (player.StatsManager.IndicatorStats.Stamina - player.StatsManager.staminaJumpCostSec > 0)
                        {
                            player.Model.ModelState = ModelStates.JUMPING;
                        }
                    }

                //Console.WriteLine(player.Model.ModelState);
            }
        }
    }
}
