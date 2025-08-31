using Resources;
using System;
using Utils;
using static Entities.PhysicalEntity;


namespace Entities
{
    public class Equipment : Item
    {

        public enum EquipmentSlotsTake 
        {
            WEAPON_SINGLE,
            WEAPON_DOUBLE,

            TORSO,
            HEAD,
            LEGS,
            HANDS,

            NECKLACE,
            CAPE,
            BELT,
            RING_L,
            RING_R,

            PET,
            PET_LIGHT,
            CONTAINMENT,
        }

        public EquipmentSlotsTake EquipmentSlot;

        public BattleDamageStatsData BattleItemStatsData;

        public Equipment(ItemKey itemKey) : base(itemKey)
        {
            
        }

        public override void SetItem()
        {
            BattleItemStatsData = new BattleDamageStatsData();
        }

        public virtual void Draw(Model model){}
    }
}
