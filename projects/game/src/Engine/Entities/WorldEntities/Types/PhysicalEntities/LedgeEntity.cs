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
        public enum Ledges
        {
            INVISIBLE,
            LEDGE0,
        }

        public Vector2 HangingPosition;
        public Ledges Type;

        public bool AutoClimbing;
        public Vector2 AutoClimbingDestination;

        public LedgeEntity(Vector2 pos, Directions direction, Ledges type = Ledges.INVISIBLE, bool autoClimbing = false) : base()
        {
            HangingPosition = new Vector2(pos.X + 10 * Resources.Model.GetDirectionCoefficient(direction), pos.Y - 10);
            Type = type;
            AutoClimbing = autoClimbing;
            if(autoClimbing)
            {
                AutoClimbingDestination = new Vector2(pos.X - 20 * Resources.Model.GetDirectionCoefficient(direction), pos.Y + 30);
            }
            Init(Models.LEDGE, pos, 0f, direction);
        }

        public override void SetAnimations()
        {
            Model.AnimationState = AnimationStates.IDLE;
            Model.AManagers = new Graphics.AnimationSet[1];
            Model.AManagers[0] = new Graphics.AnimationSet(Model.SpriteData);
            //Model.AManagers.AddAnimationForBothDirections(, AnimationStates.IDLE, new Graphics.AnimationFramesData(1, Vector2.Zero, new Vector2(32, 32), 1));
            Model.AManagers[0].Update(new Graphics.AnimationKey(Model.AnimationState, Model.Direction));
        }

        public override void Draw()
        {
            if(Type != Ledges.INVISIBLE)
                base.Draw();
        }
    }
}
