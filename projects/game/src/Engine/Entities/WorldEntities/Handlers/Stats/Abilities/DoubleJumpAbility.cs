using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DoubleJumpAbility : EntityAbility
    {



        public DoubleJumpAbility()
        {
            Type = EntityStatFeatures.DOUBLE_JUMP;
        }


        public override void Update(StatsManager statsManager, Model model)
        {
            if(model.ModelState == Utils.ModelStates.JUMPING_DESCENDING || 
                model.ModelState == Utils.ModelStates.JUMPING_DESCENDING_AND_MOVING || 
                model.ModelState == Utils.ModelStates.DOUBLE_JUMPING ||
                model.ModelState == Utils.ModelStates.DOUBLE_JUMPING_AND_MOVING)
            {
                statsManager.AllowDoubleJump = true;
            }
            else
            {
                statsManager.AllowDoubleJump = false;
            }
        }
    }
}
