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

        public bool AllowDoubleJump = false;

        public DoubleJumpAbility()
        {
            Type = EntityStatFeatures.DOUBLE_JUMP;
        }


        public override void Update(StatsManager statsManager, Model model)
        {
            if(model.ModelState == ModelStates.JUMPING_DESCENDING || 
                model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING || 
                model.ModelState == ModelStates.DOUBLE_JUMPING ||
                model.ModelState == ModelStates.DOUBLE_JUMPING_AND_MOVING)
            {
                AllowDoubleJump = true;
            }
            else
            {
                AllowDoubleJump = false;
            }
        }
    }
}
