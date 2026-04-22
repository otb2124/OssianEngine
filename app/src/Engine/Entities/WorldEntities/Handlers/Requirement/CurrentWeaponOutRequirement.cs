using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class CurrentWeaponOutRequirement : Requirement
    {
        public CurrentWeaponOutRequirement(bool negate = false)
        {
            IsNegation = negate;
        }

        public override bool Check(StatsEntity Entity)
        {

            bool result = false;


            if (Entity != null && Entity is EquipmentEntity ent)
            {
                result = ent.EquipmentManager.WeaponInOutToggler.IsWeaponOut;
            }

            return IsNegation ? !result : result;
        }
    }
}
