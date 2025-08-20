using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Entities
{

    public enum WeaponHand
    {
        LEFT,
        RIGHT,
        BOTH
    }

    public class EquipmenBattleBodyManager : BattleBodyManager
    {
        
        public EquipmenBattleBodyManager() : base()
        {
            CreateBodies(2);
        }


        public override void CreateBodies(int count)
        {
            BattleBodies.Clear();

            for (int i = 0; i < count; i++)
            {
                BattleBodies.Add(new EquipmetWeaponBody());
            }
        }

        public override void CreateBody()
        {
            BattleBodies.Add(new EquipmetWeaponBody());
        }


        public EquipmetWeaponBody HandToEquipmentWeaponBody(WeaponHand hand) =>
           hand == WeaponHand.LEFT ? (EquipmetWeaponBody)BattleBodies[0] : (EquipmetWeaponBody)BattleBodies[1];
    }
}
