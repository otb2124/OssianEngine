using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;

namespace Entities
{
    public class StatsManager
    {
        public EntityStats stats;
        public EquipmentManager equipmentManager;
        public Inventory inventory;

        public StatsManager()
        {
            stats = new EntityStats();
            equipmentManager = new EquipmentManager();

            inventory = new Inventory();
        }


        public void DealDamageTo(LivingEntity target)
        {
            target.sManager.stats.HP -= this.equipmentManager.GetCurrentWeapon().PhysDmg;
        }
    }
}