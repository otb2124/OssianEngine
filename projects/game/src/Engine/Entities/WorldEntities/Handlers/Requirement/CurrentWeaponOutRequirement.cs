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

        public override bool Check()
        {
            bool result = Entities.Player.EquipmentManager.WeaponInOutToggler.IsWeaponOut;
            return IsNegation ? !result : result;
        }
    }
}
