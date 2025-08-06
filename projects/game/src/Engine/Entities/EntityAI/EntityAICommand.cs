using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Entities.WeaponComboHitSetFactory;

namespace Entities
{
    public class EntityAICommand
    {
        public StatsEntity Entity;

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
            Entity.Model.ModelState = ModelStates.IDLE;
        }

        public void Move()
        {
            Move(Entity.Model.Direction);
        }

        public void Move(Directions direction)
        {
            Entity.Model.ModelState = ModelStates.MOVING;
            Entity.Model.Direction = direction;
        }

        public void Jump()
        {
            Entity.Model.ModelState = ModelStates.JUMPING;
        }

        public void JumpAndMove(Directions direction, StatsEntity Entity)
        {
            Entity.Model.Direction = direction;
            Entity.Model.ModelState = ModelStates.JUMPING_AND_MOVING;
        }

        public void Sprint()
        {
            Sprint(Entity.Model.Direction);
        }

        public void Sprint(Directions direction)
        {
            Entity.Model.ModelState = ModelStates.SPRINTING;
            Entity.Model.Direction = direction;
        }

        public void PerformWeaponAttack(AttackTypes type)
        {
            if (Entity is EquipmentEntity eqEnt)
            {
                IsDurationInfinite = false;
                RepeatAfterRestart = true;

                ModelStates state = SwitchAttackTypeToModelState(type);
                AttackTypes[] currentAttack = eqEnt.EquipmentManager.GetCurrentWeapon().WeaponEntity.GetCurrentAttack(type);

                CurrentDuration = eqEnt.EquipmentManager.GetCurrentWeapon().WeaponEntity.CalculatePredictedFinalSwingTime(eqEnt.EquipmentManager.GetCurrentWeapon().WeaponEntity.MoveSet, currentAttack) * 1.5f;

                if (CommandTime < CurrentDuration * Graphics.Graphics.UpdatesPerSecond / 2f)
                {
                    eqEnt.Model.ModelState = state;
                }
                else
                {
                   eqEnt.Model.ModelState = ModelStates.IDLE;
                }
            }
        }

        public void FollowPlayerAndWeaponAttack(AttackTypes type)
        {
            if (Entities.Player == null || Entities.Player.Model?.Body == null || !(Entity is EquipmentEntity eqEnt)) return;

            Vector2 directionToPlayer = EntityDirection(Entities.Player, eqEnt);
            float distance = directionToPlayer.Length();
            AttackTypes[] currentAttack = eqEnt.EquipmentManager.GetCurrentWeapon().WeaponEntity.GetCurrentAttack(type);
            WeaponComboHit currentHit = GetComboHit(eqEnt.EquipmentManager.GetCurrentWeapon().WeaponEntity.MoveSet, currentAttack);

            if(currentHit != null) 
            {
                float attackRange = currentHit.EntityPositionOffset.X + currentHit.HitboxOffset.Height;

                if (distance > attackRange)
                {
                    directionToPlayer.Normalize();
                    float speed = eqEnt.Stats?.speed ?? 1f;
                    Vector2 velocity = directionToPlayer * speed;
                    eqEnt.Model.Direction = velocity.X > 0 ? Directions.RIGHT : Directions.LEFT;

                    eqEnt.Model.ModelState = ModelStates.MOVING;
                }
                else
                {
                    PerformWeaponAttack(type);
                }
            }
            
        }



        public void FollowPlayer(float? stopDistance = null)
        {
            if (Entities.Player == null || Entities.Player.Model?.Body == null) return;

            Vector2 directionToPlayer = EntityDirection(Entities.Player, Entity);
            float distance = directionToPlayer.Length();

            if (distance > (stopDistance ?? 0.1f))
            {
                directionToPlayer.Normalize();
                float speed = Entity.Stats?.speed ?? 1f;
                Vector2 velocity = directionToPlayer * speed;
                Entity.Model.Direction = velocity.X > 0 ? Directions.RIGHT : Directions.LEFT;
                Entity.Model.ModelState = ModelStates.MOVING;
            }
            else
            {
                Entity.Model.ModelState = ModelStates.IDLE;
            }
        }

        public static float EntityDistance(PhysicalEntity entityFrom, PhysicalEntity entityTo)
        {
            return EntityDirection(entityFrom, entityTo).Length();
        }

        public static Vector2 EntityDirection(PhysicalEntity entityFrom, PhysicalEntity entityTo)
        {
            Vector2 EntityPos1 = FlatConverter.ToVector2(entityFrom.Model.Body.Position);
            Vector2 EntityPos2 = FlatConverter.ToVector2(entityTo.Model.Body.Position);
            return EntityPos1 - EntityPos2;
        }


        /*
        public void PerformWeaponAttackCombo(AttackTypes[] sequence)
        {
            if (Entity is EquipmentEntity eqEnt)
            {
                EntityAICommand[] commands = new EntityAICommand[sequence.Length];
                if (!ComplexCommandSet)
                {
                    for (int i = 0; i < sequence.Length; i++)
                    {
                        Console.WriteLine($"Creating action for AttackTypes[{i}]: {sequence[i]}");
                        int index = i;
                        commands[i] = new EntityAICommand(entity => entity.PerformWeaponAttack(sequence[index]));
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
