using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class LedgeEntity : PhysicalEntity
    {

        public Vector2 HangingPosition;

        public LedgeEntity(Vector2 pos, Directions direction) : base()
        {
            HangingPosition = new Vector2(pos.X + 10 * Resources.Model.GetDirectionCoefficient(direction), pos.Y - 10);
            Init(Models.LEDGE, pos, 0f, direction);
        }

        public override void SetAnimations()
        {
            Model.animationState = AnimationStates.IDLE;
            Model.aManager.AddAnimationForBothDirections(Model.spriteData, AnimationStates.IDLE, new Graphics.AnimationData(1, Vector2.Zero, new Vector2(32, 32), 1));
            Model.aManager.Update(new Tuple<Directions, AnimationStates>(Model.Direction, Model.animationState));
        }
    }
}
