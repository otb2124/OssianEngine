using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class ModelStateRequirement : Requirement
    {

        public ModelStates ModelState;

        public ModelStateRequirement(ModelStates modelState)
        {
            ModelState = modelState;
        }

        public override bool Check()
        {
            return Entities.Player.Model.ModelState == ModelState;
        }
    }
}
