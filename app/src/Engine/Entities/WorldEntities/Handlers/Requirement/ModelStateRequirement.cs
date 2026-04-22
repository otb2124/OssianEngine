using Microsoft.Xna.Framework.Input;
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

        public ModelStateRequirement(ModelStates modelState, bool negation = false)
        {
            ModelState = modelState;
            IsNegation = negation;
        }

        public override bool Check(StatsEntity Entity)
        {
            bool result = Entity.Model.ModelState == ModelState;
            return IsNegation ? !result : result;
        }
    }
}
