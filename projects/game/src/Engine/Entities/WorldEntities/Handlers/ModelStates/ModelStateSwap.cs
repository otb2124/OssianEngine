using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class ModelStateSwap
    {
        public Requirement[] Requirements;
        public ModelStates ModelState;


        public ModelStateSwap(ModelStates modelState, Requirement[] requirements = null)
        {
            ModelState = modelState;
            Requirements = requirements;
        }

        public void Check()
        {
            if(Requirements == null)
            {
                Entities.Player.Model.ModelState = ModelState;
            }
            else
            {
                foreach (Requirement requirement in Requirements)
                {
                    if (requirement.Check())
                    {
                        Entities.Player.Model.ModelState = ModelState;
                    }
                }
            }
            
        }

        public void Apply(StatsEntity Entity)
        {
            ModelStates state = Entity.Model.ModelState;
            int directionXFactor = Entity.Model.Direction == Directions.RIGHT ? 1 : -1;

            ModelStateHandler.StateActions[state](Entity, directionXFactor);
        }
    }

}
