using Entities;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
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
        CLIMBING_LADDER,
        DYING,

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

        ATTACKING_SPELL_LIGHT,
        ATTACKING_SPELL_HEAVY,

        ATTACKING_BOW_LIGHT,
        ATTACKING_BOW_HEAVY,

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

        public static void Handle(StatsEntity Entity)
        {
            ModelStates state = Entity.Model.ModelState;
            int directionXFactor = Entity.Model.Direction == Directions.RIGHT ? 1 : -1;
             
            StateActions[state](Entity, directionXFactor);
        }

        public static void Idle(StatsEntity Entity)
        {
            if (Entity.StatsManager.StatsBattleHitSpendHandler != null)
            {
                Entity.StatsManager.StatsBattleHitSpendHandler.StatsPerAttackHitSpent = false;
            }
        }

        public static void Move(StatsEntity Entity, int directionXFactor)
        {
            Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * directionXFactor, 0));
        }

        public static void Jump(StatsEntity Entity)
        {
            Entity.Model.Body.Jump(Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).CurrentValue);
            Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).StaminaDependencySec / (float)Graphics.Graphics.GraphicsFrameRate;
            Entity.StatsManager.GetStatAbility<DescencionAbility>().AllowJumpDescendingLock = true;
            Entity.Model.Body.IsFrozen = false;
        }

        public static void JumpAndMove(StatsEntity Entity, int directionXFactor)
        {
            Entity.Model.Body.Jump(Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).CurrentValue);
            Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).StaminaDependencySec / (float)Graphics.Graphics.GraphicsFrameRate;
            Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * directionXFactor, 0));
            Entity.StatsManager.GetStatAbility<DescencionAbility>().AllowJumpDescendingLock = true;
            Entity.Model.Body.IsFrozen = false;
        }

        public static void DoubleJump(StatsEntity Entity)
        {
            Entity.Model.Body.Jump(Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).CurrentValue * 1.5f);
            Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).StaminaDependencySec / (float)Graphics.Graphics.GraphicsFrameRate;
            Entity.StatsManager.GetStatAbility<DescencionAbility>().AllowJumpDescendingLock = true;
            Entity.Model.Body.IsFrozen = false;
        }

        public static void DoubleJumpAndMove(StatsEntity Entity, int directionXFactor)
        {
            Entity.Model.Body.Jump(Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).CurrentValue * 1.5f);
            Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).StaminaDependencySec / (float)Graphics.Graphics.GraphicsFrameRate;
            Entity.StatsManager.GetStatAbility<DescencionAbility>().AllowJumpDescendingLock = true;
            Entity.Model.Body.IsFrozen = false;
            Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * directionXFactor, 0));
        }

        public static void Block(StatsEntity Entity)
        {
            Entity.StatsManager.GetStatAbility<StaminaRegenerationAbility>().OnUsingStamina = true;
        }

        public static void AttackLight(StatsEntity Entity)
        {
            if(Entity is EquipmentEntity eqEnt)
            {
                if (!eqEnt.StatsManager.StatsBattleHitSpendHandler.StatsPerAttackHitSpent)
                {
                    eqEnt.StatsManager.SpendStatsForBattleHit(eqEnt);
                }
            }
        }

        public static void AttackHeavy(StatsEntity Entity)
        {
            if (Entity is EquipmentEntity eqEnt)
            {
                if (!eqEnt.StatsManager.StatsBattleHitSpendHandler.StatsPerAttackHitSpent)
                {
                    eqEnt.StatsManager.SpendStatsForBattleHit(eqEnt);
                }
            }
        }

        public static void Sprint(StatsEntity Entity, int directionXFactor)
        {
            if (Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue - Entity.StatsManager.GetStat(EntityStats.SPRINT_SPEED_MULTIPLIER).StaminaDependencySec / (float)Graphics.Graphics.GraphicsFrameRate > 0)
            {
                Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * Entity.StatsManager.GetStat(EntityStats.SPRINT_SPEED_MULTIPLIER).CurrentValue * directionXFactor, 0));
                Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.SPRINT_SPEED_MULTIPLIER).StaminaDependencySec / (float)Graphics.Graphics.GraphicsFrameRate;
                Entity.StatsManager.GetStatAbility<StaminaRegenerationAbility>().OnUsingStamina = true;
            }
            else
            {
                Entity.Model.ModelState = ModelStates.IDLE;
            }
        }

        public static void WeaponOutIdle(StatsEntity Entity)
        {
            if (Entity.StatsManager.StatsBattleHitSpendHandler != null)
            {
                Entity.StatsManager.StatsBattleHitSpendHandler.StatsPerAttackHitSpent = false;
            }
        }

        public static void WeaponOutMove(StatsEntity Entity, int directionXFactor)
        {
            Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * directionXFactor, 0));
        }

        public static void Roll(StatsEntity Entity, int directionXFactor)
        {
            Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.ROLL_SPEED_MULTIPLIER).StaminaDependencySec / (float)Graphics.Graphics.GraphicsFrameRate;
            Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * Entity.StatsManager.GetStat(EntityStats.ROLL_SPEED_MULTIPLIER).CurrentValue * directionXFactor, 0));
        }


        public static void Fallen(StatsEntity Entity)
        {

        }

        public static void Falling(StatsEntity Entity)
        {

        }

        public static void ReceiveDamage(StatsEntity Entity)
        {

        }

        public static void JumpDescend(StatsEntity Entity)
        {
            Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;
            Entity.Model.Body.linearVelocity -= new PhysicalVector(0, Entity.StatsManager.GetStatAbility<DescencionAbility>().DescendingMultiplier * 200);
        }

        public static void JumpDescendAndMove(StatsEntity Entity, int directionXFactor)
        {
            Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;
            Entity.Model.Body.linearVelocity -= new PhysicalVector(0, Entity.StatsManager.GetStatAbility<DescencionAbility>().DescendingMultiplier * 200);
            Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * directionXFactor, 0));
        }

        public static void Descend(StatsEntity Entity)
        {

        }

        public static void HangOnLedge(StatsEntity Entity)
        {
            Entity.Model.Body.linearVelocity = PhysicalVector.Zero;
            Entity.Model.Body.IsFrozen = true;
        }

        public static void InwaterMove(StatsEntity Entity, int directionXFactor)
        {
            Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * directionXFactor, 0));
        }

        public static void ClimbLadder(StatsEntity Entity)
        {

        }

        public static void Fly(StatsEntity Entity)
        {
            Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;

            if (!Entity.StatsManager.GetStatAbility<FlyAbility>().FlyingUpwards)
            {
                Entity.Model.Body.Jump(Entity.StatsManager.GetStat(EntityStats.FLY_SPEED).CurrentValue);
            }
            else
            {
                Entity.Model.Body.Jump(-Entity.StatsManager.GetStat(EntityStats.FLY_SPEED).CurrentValue);
            }

            Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).StaminaDependencySec / (float)Graphics.Graphics.GraphicsFrameRate;
            Entity.StatsManager.GetStatAbility<DescencionAbility>().AllowJumpDescendingLock = true;
            Entity.Model.Body.IsFrozen = false;
        }

        public static void FlyAndMove(StatsEntity Entity, int directionXFactor)
        {
            Entity.Model.Body.linearVelocity *= (float)Graphics.Graphics.CurrentLogicTime / (float)Graphics.Graphics.TimeScale;

            if (Entity.StatsManager.GetStatAbility<FlyAbility>().FlyingUpwards)
            {
                Entity.Model.Body.Jump(Entity.StatsManager.GetStat(EntityStats.FLY_SPEED).CurrentValue);
            }
            else
            {
                Entity.Model.Body.Jump(-Entity.StatsManager.GetStat(EntityStats.FLY_SPEED).CurrentValue);
            }

            Entity.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue -= Entity.StatsManager.GetStat(EntityStats.JUMP_SPEED).StaminaDependencySec / (float)Graphics.Graphics.GraphicsFrameRate;
            Entity.StatsManager.GetStatAbility<DescencionAbility>().AllowJumpDescendingLock = true;
            Entity.Model.Body.IsFrozen = false;

            Entity.Model.Body.Move(new PhysicalVector(Entity.StatsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * directionXFactor, 0));
        }

        public static void Die(StatsEntity Entity)
        {
            if (Entity.DropInventory != null)
            {
                if (!Entity.DropInventory.IsEmpty())
                {
                    List<Item> droppedItems = Entity.DropInventory.TryDrop();

                    foreach (Item item in droppedItems)
                    {
                        InteractiveItemEntity itemEnt = EntityHelper.CreateItemDrop(item, Entity.Model.Body.Position.ToVector2());
                        Entities.EntityMapManager.GetCurrentMapLayer().Entities.Add(itemEnt);
                        Graphics.Graphics.LightManager.AddEntityEmissionLightSource(itemEnt);
                    }
                }
            }

            Entities.EntityManager.RemoveEntity(Entity);
        }


        public static Dictionary<ModelStates, Action<StatsEntity, int>> StateActions = new()
        {
            { ModelStates.IDLE,                  (e, d) => Idle(e) },
            { ModelStates.WEAPON_OUT_IDLE,       (e, d) => WeaponOutIdle(e) },
            { ModelStates.MOVING,                (e, d) => Move(e, d) },
            { ModelStates.WEAPON_OUT_MOVING,     (e, d) => WeaponOutMove(e, d) },
            { ModelStates.INWATER_MOVING,        (e, d) => InwaterMove(e, d) },
            { ModelStates.JUMPING,               (e, d) => Jump(e) },
            { ModelStates.JUMPING_AND_MOVING,    (e, d) => JumpAndMove(e, d) },
            { ModelStates.FLYING,                (e, d) => Fly(e) },
            { ModelStates.FLYING_AND_MOVING,     (e, d) => FlyAndMove(e, d) },
            { ModelStates.SPRINTING,             (e, d) => Sprint(e, d) },
            { ModelStates.BLOCKING,              (e, d) => Block(e) },
            { ModelStates.ROLLING,               (e, d) => Roll(e, d) },
            { ModelStates.JUMPING_DESCENDING,    (e, d) => JumpDescend(e) },
            { ModelStates.JUMPING_DESCENDING_AND_MOVING, (e, d) => JumpDescendAndMove(e, d) },
            { ModelStates.HANGING_ON_LEDGE,      (e, d) => HangOnLedge(e) },
            { ModelStates.DOUBLE_JUMPING,        (e, d) => DoubleJump(e) },
            { ModelStates.DOUBLE_JUMPING_AND_MOVING, (e, d) => DoubleJumpAndMove(e, d) },
            { ModelStates.ATTACKING_LIGHT,       (e, d) => AttackLight(e) },
            { ModelStates.ATTACKING_HEAVY,       (e, d) => AttackHeavy(e) },
            

            { ModelStates.FALLEN,                (e, d) => Fallen(e) },
            { ModelStates.FALLING,               (e, d) => Falling(e) },
            { ModelStates.RECEIVING_DAMAGE,      (e, d) => ReceiveDamage(e) },
            { ModelStates.DESCENDING,            (e, d) => Descend(e) },
            { ModelStates.CLIMBING_LADDER,       (e, d) => ClimbLadder(e) },
            { ModelStates.DYING,                 (e, d) => Die(e) },
        };
        
    }
}
