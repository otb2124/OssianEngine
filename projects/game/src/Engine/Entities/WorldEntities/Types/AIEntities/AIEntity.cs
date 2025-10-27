using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Entities.EntityAIBehaviourManager;

namespace Entities
{
    public class AIEntity : BattleEntity
    {

        public EntityAISet AISet;

        public AIEntity(Models modelPreset, Vector2 pos, float rot = 0) : base(modelPreset, pos, rot)
        {
            //SetAI();
        }

        public AIEntity() : base()
        {
            //SetAI();
        }

        public virtual void SetAI()
        {
            //AISet = new EntityAISet(this, BehaviourPatterns.BANDIT_DEFAULT, BehaviourCases.IDLE_RANDOM);
        }


        public override void Update()
        {
            if (AISet != null && !StatsManager.IsFallen && !StatsManager.IsFalling)
            {
                AISet.Update(this);
                Model.AManagers[0].Update(new AnimationKey(Model.AnimationState, Model.Direction));
            }

            base.Update();
        }
    }
}
