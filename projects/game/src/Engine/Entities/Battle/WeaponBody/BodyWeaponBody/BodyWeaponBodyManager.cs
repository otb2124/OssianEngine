using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BodyWeaponBodyManager : WeaponBodyManager
    {

        public BodyWeaponBodyManager() : base()
        {
            CreateBodies(1);
        }


        public override void CreateBodies(int count)
        {
            WeaponBodies.Clear();

            for (int i = 0; i < count; i++)
            {
                WeaponBodies.Add(new BodyWeaponBody());
            }
        }

        public override void CreateBody()
        {
            WeaponBodies.Add(new BodyWeaponBody());
        }
    }
}
