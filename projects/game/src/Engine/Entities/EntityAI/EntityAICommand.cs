using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

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
            Entity.Model.modelState = ModelStates.IDLE;
        }

        public void Move()
        {
            Move(Entity.Model.direction);
        }

        public void Move(Directions direction)
        {
            Entity.Model.modelState = ModelStates.MOVING;
            Entity.Model.direction = direction;
        }

        public void Jump()
        {
            Entity.Model.modelState = ModelStates.JUMPING;
        }

        public void JumpAndMove(Directions direction, StatsEntity Entity)
        {
            Entity.Model.direction = direction;
            Entity.Model.modelState = ModelStates.JUMPING_AND_MOVING;
        }

        public void Sprint()
        {
            Sprint(Entity.Model.direction);
        }

        public void Sprint(Directions direction)
        {
            Entity.Model.modelState = ModelStates.SPRINTING;
            Entity.Model.direction = direction;
        }

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
