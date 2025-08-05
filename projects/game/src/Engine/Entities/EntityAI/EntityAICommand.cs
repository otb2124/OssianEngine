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

        public float Duration;
        public Action<EntityAICommand> CommandAction;

        public bool IsDurationInfinite = false;
        public bool RepeatAfterRestart = false;
        public int RepeatAfterCommandsCount = 0;

        public float CommandTime = 0;

        public bool ComplexCommandSet = false;

        public EntityAICommand(Action<EntityAICommand> commandAction, float duration)
        {
            Duration = duration;
            CommandAction = commandAction;
        }

        public EntityAICommand(Action<EntityAICommand> commandAction, float duration, bool repeatAfterRestart)
        {
            Duration = duration;
            CommandAction = commandAction;
            RepeatAfterRestart = repeatAfterRestart;
        }

        public EntityAICommand(Action<EntityAICommand> commandAction, float duration, int repeatAfterCommandsCount)
        {
            Duration = duration;
            CommandAction = commandAction;
            RepeatAfterCommandsCount = repeatAfterCommandsCount;
        }

        public EntityAICommand(Action<EntityAICommand> commandAction)
        {
            IsDurationInfinite = true;
            CommandAction = commandAction;

            Duration = 0f;
        }

        public void StandStill()
        {
            Entity.Model.ModelState = ModelStates.IDLE;
        }

        public void Move()
        {
            Move(Entity.Model.direction);
        }

        public void Move(Directions direction)
        {
            Entity.Model.ModelState = ModelStates.MOVING;
            Entity.Model.direction = direction;
        }

        public void Jump()
        {
            Entity.Model.ModelState = ModelStates.JUMPING;
        }

        public void JumpAndMove(Directions direction, StatsEntity Entity)
        {
            Entity.Model.direction = direction;
            Entity.Model.ModelState = ModelStates.JUMPING_AND_MOVING;
        }

        public void Sprint()
        {
            Sprint(Entity.Model.direction);
        }

        public void Sprint(Directions direction)
        {
            Entity.Model.ModelState = ModelStates.SPRINTING;
            Entity.Model.direction = direction;
        }

        public void PerformWeaponAttack(AttackTypes type)
        {
            if (Entity is EquipmentEntity eqEnt)
            {
                IsDurationInfinite = false;
                RepeatAfterRestart = true;

                ModelStates state = ModelStates.ATTACKING_LIGHT;

                AttackTypes[] history = eqEnt.EquipmentManager.GetCurrentWeapon().WeaponEntity.AttackHistory.ToArray();
                AttackTypes[] currentAttack = new AttackTypes[history.Length + 1];
                for (global::System.Int32 i = 0; i < history.Length; i++)
                {
                    currentAttack[i] = history[i];
                }
                currentAttack[currentAttack.Length-1] = type;

                Duration = eqEnt.EquipmentManager.GetCurrentWeapon().WeaponEntity.CalculatePredictedFinalSwingTime(eqEnt.EquipmentManager.GetCurrentWeapon().WeaponEntity.MoveSet, currentAttack) * 1.5f;

                if (type == AttackTypes.LIGHT)
                {
                    state = ModelStates.ATTACKING_LIGHT;
                }
                else
                {
                    state = ModelStates.ATTACKING_HEAVY;
                }

                if (CommandTime < Duration * Graphics.Graphics.UpdatesPerSecond / 2f)
                {
                    eqEnt.Model.ModelState = state;
                }
                else
                {
                   eqEnt.Model.ModelState = ModelStates.IDLE;
                }
            }
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
                Duration = command.TotalDurationSec;
            }

            command.Execute();
        }
        */

        public void FollowPlayer(float? stopDistance = null)
        {
            if (Entities.Player == null || Entities.Player.Model?.body == null) return;

            Vector2 EntityPos = FlatConverter.ToVector2(Entities.Player.Model.body.Position);
            Vector2 currentPos = FlatConverter.ToVector2(Entity.Model.body.Position);
            Vector2 directionToPlayer = EntityPos - currentPos;
            float distance = directionToPlayer.Length();

            if (distance > (stopDistance ?? 0.1f))
            {
                directionToPlayer.Normalize();
                float speed = Entity.Stats?.speed ?? 100f;
                Vector2 velocity = directionToPlayer * speed;
                Entity.Model.direction = velocity.X > 0 ? Directions.RIGHT : Directions.LEFT;
                Entity.Model.body.Move(FlatConverter.ToFlatVector(velocity));
            }
        }
    }
}
