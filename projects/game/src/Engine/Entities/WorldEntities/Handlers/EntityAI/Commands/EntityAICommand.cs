using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Entities.BattleMovesetFactory;

namespace Entities
{
    public class EntityAICommand
    {
        public AIEntity Entity;

        public float InitialDuration;
        public float CurrentDuration;
        public Action<EntityAICommand> CommandAction;

        public bool IsDurationInfinite = false;
        public bool RepeatAfterRestart = false;
        public int RepeatAfterCommandsCount = 0;

        public float CommandTime = 0;

        public bool ComplexCommandSet = false;

        public EntityAICommand(Action<EntityAICommand> commandAction, float duration)
        {
            InitialDuration = duration;
            CurrentDuration = duration;
            CommandAction = commandAction;
        }

        public EntityAICommand(Action<EntityAICommand> commandAction, float duration, bool repeatAfterRestart)
        {
            InitialDuration = duration;
            CurrentDuration = duration;
            CommandAction = commandAction;
            RepeatAfterRestart = repeatAfterRestart;
        }

        public EntityAICommand(Action<EntityAICommand> commandAction, float duration, int repeatAfterCommandsCount)
        {
            InitialDuration = duration;
            CurrentDuration = duration;
            CommandAction = commandAction;
            RepeatAfterCommandsCount = repeatAfterCommandsCount;
        }

        public EntityAICommand(Action<EntityAICommand> commandAction)
        {
            IsDurationInfinite = true;
            CommandAction = commandAction;

            CurrentDuration = 0f;
        }

        public void ReInit()
        {
            CurrentDuration = InitialDuration;
            CommandTime = 0f;
        }

        public void StandStill()
        {
            Entity.EntityControlHandler.ResetAllStates();
        }

        public void Move()
        {
            Move(Entity.Model.Direction);
        }

        public void Move(Directions direction)
        {
            if(direction == Directions.LEFT)
            {
                Entity.EntityControlHandler.SetState(Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED, true);
            }
            else
            {
                Entity.EntityControlHandler.SetState(Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED, true);
            }
        }

        public void MoveUntillUngrounded()
        {
            MoveUntillUngrounded(Entity.Model.Direction);
        }

        public void MoveUntillUngrounded(Directions direction)
        {
            if (EntityAIHelper.HasGroundForward(Entity))
            {
                Move(direction);
            }
            else
            {
                Entity.Model.SwapDirection();
            }
        }

        public void MoveUntillUngroundedAndStandStill(Directions direction)
        {
            if (EntityAIHelper.HasGroundForward(Entity))
            {
                Move(direction);
            }
            else
            {
                StandStill();
            }
        }

        public void Jump()
        {
            Entity.EntityControlHandler.SetState(Inputs.KeyHandler.KeyStates.JUMPPRESSED, true);
        }

        public void JumpAndMove(Directions direction)
        {
            Move(direction);
            Jump();
        }

        public void Fly()
        {
            Entity.Model.ModelState = ModelStates.FLYING;
        }

        public void FlyAndMove(Directions direction)
        {
            Entity.Model.Direction = direction;
            Entity.Model.ModelState = ModelStates.FLYING_AND_MOVING;
        }

        public void Sprint()
        {
            Sprint(Entity.Model.Direction);
        }

        public void Sprint(Directions direction)
        {
            Entity.EntityControlHandler.SetState(Inputs.KeyHandler.KeyStates.SPRINTPRESSED, true);
            Move(direction);
        }

        public void PerformAttack(AttackTypes type)
        {
            IsDurationInfinite = false;
            RepeatAfterRestart = true;

            ModelStates state = SwitchAttackTypeToModelState(type);

            if(Entity is EquipmentEntity eqEnt)
            {
                if(!eqEnt.EquipmentManager.WeaponInOutToggler.IsWeaponOut)
                {
                    eqEnt.EquipmentManager.ToggleWeaponInOut(eqEnt.BattleBodyManager);
                }

                AttackTypes[] currentAttack = eqEnt.EquipmentManager.GetCurrentWeaponBody(eqEnt.BattleBodyManager).GetCurrentAttack(type);
                CurrentDuration = eqEnt.EquipmentManager.GetCurrentWeaponBody(eqEnt.BattleBodyManager).CalculatePredictedFinalSwingTime(eqEnt.EquipmentManager.GetCurrentWeaponBody(eqEnt.BattleBodyManager).BattleBodyData.MoveSet, currentAttack) * 1.5f;
            }
            else if(Entity is NonEquipmentEntity noEqEnt)
            {
                AttackTypes[] currentAttack = noEqEnt.BattleBodyManager.BattleBodies[0].GetCurrentAttack(type);
                CurrentDuration = noEqEnt.BattleBodyManager.BattleBodies[0].CalculatePredictedFinalSwingTime(noEqEnt.BattleBodyManager.BattleBodies[0].BattleBodyData.MoveSet, currentAttack) * 1.5f;
            }
            

            if (CommandTime < CurrentDuration * Graphics.Graphics.UpdatesPerSecond / 1.5f)
            {
                Entity.Model.ModelState = state;
            }
            else
            {
                StandStill();

                if(Entity.StatsManager.GetStatAbility(EntityStatFeatures.FLY) != null)
                {
                    Entity.Model.ModelState = ModelStates.FLYING;
                }
            }

        }

        public void FollowEntity(PhysicalEntity ent, float? stopDistance = null)
        {
            if (ent == null || ent.Model?.Body == null) return;

            Vector2 directionToEntity = EntityAIHelper.GetEntityDirection(ent, Entity);
            float distance = directionToEntity.Length();
            float defaultStopDistance = 0.1f;

            if (distance > (stopDistance ?? defaultStopDistance))
            {
                directionToEntity.Normalize();
                float speed = Entity.StatsManager?.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue ?? 1f;
                Vector2 velocity = directionToEntity * speed;
                Entity.Model.Direction = velocity.X > 0 ? Directions.RIGHT : Directions.LEFT;
                Move();

                if (Entity.StatsManager.GetStatAbility(EntityStatFeatures.FLY) != null)
                {
                    Entity.Model.ModelState = ModelStates.FLYING_AND_MOVING;

                    float distanceX = EntityAIHelper.GetEntityXDistance(ent, Entity);

                    if(distanceX < 50f)
                    {
                        Entity.StatsManager.GetStatAbility<FlyAbility>().FlyingUpwards = false;

                        if (distanceX < 10f)
                        {
                            Entity.Model.ModelState = ModelStates.FLYING;
                            Entity.StatsManager.GetStatAbility<FlyAbility>().FlyingUpwards = false;
                        }
                    }
                }

                
            }
        }

        public void FollowEntityAndAttack(BattleEntity ent, AttackTypes type)
        {
            BattleComboHit currentHit = null;

            if (Entity is EquipmentEntity eqEnt)
            {
                AttackTypes[] currentAttack = eqEnt.EquipmentManager.GetCurrentWeaponBody(eqEnt.BattleBodyManager).GetCurrentAttack(type);
                currentHit = GetComboHit(eqEnt.EquipmentManager.GetCurrentWeaponBody(eqEnt.BattleBodyManager).BattleBodyData.MoveSet, currentAttack);
            }
            else if(Entity is NonEquipmentEntity nonEquipmentEnt)
            {
                AttackTypes[] currentAttack = nonEquipmentEnt.BattleBodyManager.BattleBodies[0].GetCurrentAttack(type);
                currentHit = GetComboHit(nonEquipmentEnt.BattleBodyManager.BattleBodies[0].BattleBodyData.MoveSet, currentAttack);
            }

            if (currentHit != null)
            {
                float attackRange = currentHit.EntityPositionOffset.X + currentHit.HitboxOffset.Height;

                FollowEntity(ent, attackRange);

                Vector2 directionToEntity = EntityAIHelper.GetEntityDirection(ent, Entity);
                float distance = directionToEntity.Length();

                if(distance < attackRange)
                {
                    PerformAttack(type);
                }
            }
        }

        public void FollowEntityAndAttackNearestOfFraction(StatsEntity.EntityFractions fraction, AttackTypes attackType)
        {
            BattleEntity entity = NearestEntityFinder.GetNearestBattleEntityOfFraction(Entity, fraction);

            if (entity != null)
            {
                FollowEntityAndAttack(entity, attackType);
            }
        }

        public void FollowEntityAndAttackNearestOfAggroFraction(AttackTypes attackType)
        {
            BattleEntity entity = NearestEntityFinder.GetNearestBattleEntity(Entity);

            if (entity != null)
            {
                if(EntityAIHelper.IsBattleEntityOfAggroFraction(Entity, entity))
                {
                    FollowEntityAndAttack(entity, attackType);
                }
            }
        }

        public void FollowEntityNearestOfAggroFraction()
        {
            BattleEntity entity = NearestEntityFinder.GetNearestBattleEntity(Entity);

            if (entity != null)
            {
                if (EntityAIHelper.IsBattleEntityOfAggroFraction(Entity, entity))
                {
                    FollowEntity(entity);
                }
            }
        }

        /*
        public void PerformWeaponAttackCombo(AttackTypes[] sequence)
        {
            if (WorldEntity is EquipmentEntity eqEnt)
            {
                EntityAICommand[] commands = new EntityAICommand[sequence.Length];
                if (!ComplexCommandSet)
                {
                    for (int i = 0; i < sequence.Length; i++)
                    {
                        Console.WriteLine($"Creating action for AttackTypes[{i}]: {sequence[i]}");
                        int index = i;
                        commands[i] = new EntityAICommand(entity => entity.PerformAttack(sequence[index]));
                    }
                    ComplexCommandSet = true;
                }

                PerformComplexCommand(commands);
            }
        }

        public void PerformComplexCommand(EntityAICommand[] commands)
        {
            IsDurationInfinite = false;
            RepeatAfterRestart = true;

            EntityAIComplexCommand command = new EntityAIComplexCommand();

            if(command.UnInitialized)
            {
                command = new EntityAIComplexCommand(commands);
                CurrentDuration = command.TotalDurationSec;
            }

            command.Execute();
        }
        */
    }
}
