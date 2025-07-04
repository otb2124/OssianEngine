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
            float speed = Entity.Stats.speed;
            Move(Entity.Model.direction);
        }

        public void Move(Directions direction)
        {
            Entity.Model.modelState = ModelStates.IDLE;
            Entity.Model.direction = direction;

            float speed = Entity.Stats.speed;
            Vector2 velocity = direction switch
            {
                Directions.LEFT => new Vector2(-speed, 0),
                Directions.RIGHT => new Vector2(speed, 0),
                _ => Vector2.Zero
            };

            Entity.Model.body.Move(FlatConverter.ToFlatVector(velocity));
        }

        public void Jump()
        {
            Entity.Model.modelState = ModelStates.JUMPING;
            Entity.Model.body.Jump(Entity.Stats.jumpSpeed);
        }

        public void JumpAndMove(Directions direction, StatsEntity Entity)
        {
            Move(direction);
            Jump();
        }

        public void FollowPlayer(float? stopDistance = null)
        {
            if (Entities.player == null || Entities.player.Model?.body == null) return;

            Vector2 EntityPos = FlatConverter.ToVector2(Entities.player.Model.body.Position);
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
