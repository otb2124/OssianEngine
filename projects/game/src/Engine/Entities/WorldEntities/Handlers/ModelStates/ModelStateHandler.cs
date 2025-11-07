using Physics;
using Resources;
using System;
using System.Net;
using Utils;

namespace Entities {


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
        INWATER_MOVING,

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
        INWATER_WALKING,
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


    public static class ModelStateHandler
    {

        public static void Update(StatsEntity Entity)
        {
            ModelStates state = Entity.Model.ModelState;
            int directionXFactor = Entity.Model.Direction == Directions.RIGHT ? 1 : -1;

            if (state == ModelStates.IDLE || state == ModelStates.WEAPON_OUT_IDLE)
            {
                if(Entity.StatsManager.StatsBattleHitSpendHandler != null)
                {
                    Entity.StatsManager.StatsBattleHitSpendHandler.StatsPerAttackHitSpent = false;
                }
            }

            if (state == ModelStates.MOVING || state == ModelStates.WEAPON_OUT_MOVING || state == ModelStates.INWATER_MOVING)
            {
                Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * directionXFactor, 0));
            }

            if (state == ModelStates.JUMPING)
            {
                Entity.Model.Body.Jump(Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).CurrentValue);
                Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).StaminaDependencySec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.StatsManager.AllowJumpDescendingLock = true;
                Entity.Model.Body.IsFrozen = false;
            }

            if (state == ModelStates.JUMPING_AND_MOVING)
            {
                Entity.Model.Body.Jump(Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).CurrentValue);
                Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).StaminaDependencySec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * directionXFactor, 0));
                Entity.StatsManager.AllowJumpDescendingLock = true;
                Entity.Model.Body.IsFrozen = false;
            }

            if (state == ModelStates.FLYING)
            {
                Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;

                if(!Entity.StatsManager.FlyingUpwards)
                {
                    Entity.Model.Body.Jump(Entity.StatsManager.GetStat(EntityStats.FLY_SPEED).CurrentValue);
                }
                else
                {
                    Entity.Model.Body.Jump(-Entity.StatsManager.GetStat(EntityStats.FLY_SPEED).CurrentValue);
                }
                
                Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).StaminaDependencySec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.StatsManager.AllowJumpDescendingLock = true;
                Entity.Model.Body.IsFrozen = false;
            }

            if (state == ModelStates.FLYING_AND_MOVING)
            {
                Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;

                if (Entity.StatsManager.FlyingUpwards)
                {
                    Entity.Model.Body.Jump(Entity.StatsManager.GetStat(EntityStats.FLY_SPEED).CurrentValue);
                }
                else
                {
                    Entity.Model.Body.Jump(-Entity.StatsManager.GetStat(EntityStats.FLY_SPEED).CurrentValue);
                }

                Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).StaminaDependencySec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.StatsManager.AllowJumpDescendingLock = true;
                Entity.Model.Body.IsFrozen = false;

                Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * directionXFactor, 0));
            }

            if (state == ModelStates.SPRINTING)
            {
                if (Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue - Entity.StatsManager.GetStat(EntityStats.SPRINT_SPEED_MULTIPLIER).StaminaDependencySec / (float)Graphics.Graphics.UpdatesPerSecond > 0)
                {
                    Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * Entity.StatsManager.GetStat(EntityStats.SPRINT_SPEED_MULTIPLIER).CurrentValue * directionXFactor, 0));
                    Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.SPRINT_SPEED_MULTIPLIER).StaminaDependencySec / (float)Graphics.Graphics.UpdatesPerSecond;
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
                if (Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue - Entity.StatsManager.GetStat(EntityStats.ROLL_SPEED_MULTIPLIER).StaminaDependencySec / (float)Graphics.Graphics.UpdatesPerSecond > 0)
                {
                    Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.ROLL_SPEED_MULTIPLIER).StaminaDependencySec / (float)Graphics.Graphics.UpdatesPerSecond;
                    Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * Entity.StatsManager.GetStat(EntityStats.ROLL_SPEED_MULTIPLIER).CurrentValue * directionXFactor, 0));
                }
                else
                {
                    Entity.Model.ModelState = ModelStates.IDLE;
                }
            }

            if (state == ModelStates.JUMPING_DESCENDING)
            {
                Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;
                Entity.Model.Body.linearVelocity -= new PhysicalVector(0, Entity.StatsManager.DescendingMultiplier * 200);
            }

            if (state == ModelStates.JUMPING_DESCENDING_AND_MOVING)
            {
                Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;
                Entity.Model.Body.linearVelocity -= new PhysicalVector(0, Entity.StatsManager.DescendingMultiplier * 200);
                Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * directionXFactor, 0));
            }

            if (state == ModelStates.HANGING_ON_LEDGE)
            {
                Entity.Model.Body.linearVelocity = PhysicalVector.Zero;
                Entity.Model.Body.IsFrozen = true;
            }
            else
            {
                Entity.Model.Body.IsFrozen = false;
            }

            if (state == ModelStates.DOUBLE_JUMPING)
            {
                Entity.Model.Body.Jump(Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).CurrentValue * 1.5f);
                Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).StaminaDependencySec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.StatsManager.AllowJumpDescendingLock = true;
                Entity.Model.Body.IsFrozen = false;
            }

            if (state == ModelStates.DOUBLE_JUMPING_AND_MOVING)
            {
                Entity.Model.Body.Jump(Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).CurrentValue * 1.5f);
                Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).StaminaDependencySec / (float)Graphics.Graphics.UpdatesPerSecond;
                Entity.StatsManager.AllowJumpDescendingLock = true;
                Entity.Model.Body.IsFrozen = false;
                Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * directionXFactor, 0));
            }

            if (Entity is EquipmentEntity eqEnt)
            {
                if (state == ModelStates.ATTACKING_LIGHT || state == ModelStates.ATTACKING_HEAVY)
                {
                    if (!eqEnt.StatsManager.StatsBattleHitSpendHandler.StatsPerAttackHitSpent)
                    {
                        eqEnt.StatsManager.SpendStatsForBattleHit(eqEnt);
                    }

                }

                if (state == ModelStates.BLOCKING)
                {
                    if (!eqEnt.StatsManager.StatsBattleHitSpendHandler.StatsPerAttackHitSpent)
                    {

                    }
                    
                    //StatsManager.Stamina -= StatsManager.StaminaRollCostSec / 60;
                }
            }
            
        }


        
    }
}
