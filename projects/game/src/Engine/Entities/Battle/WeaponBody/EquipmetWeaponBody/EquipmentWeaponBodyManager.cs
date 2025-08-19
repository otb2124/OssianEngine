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

    public class EquipmentWeaponBodyManager : WeaponBodyManager
    {
        
        public EquipmentWeaponBodyManager() : base()
        {
            CreateBodies(2);
        }


        public override void CreateBodies(int count)
        {
            WeaponBodies.Clear();

            for (int i = 0; i < count; i++)
            {
                WeaponBodies.Add(new EquipmetWeaponBody());
            }
        }

        public override void CreateBody()
        {
            WeaponBodies.Add(new EquipmetWeaponBody());
        }


        public EquipmetWeaponBody HandToEquipmentWeaponBody(WeaponHand hand) =>
           hand == WeaponHand.LEFT ? (EquipmetWeaponBody)WeaponBodies[0] : (EquipmetWeaponBody)WeaponBodies[1];
    }
}
