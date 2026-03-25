using Resources;
using System.Collections.Generic;
using Utils;
using static Entities.PhysicalEntity;

namespace Entities
{
    public class Equipment : Item
    {
        public enum EquipmentSlotsTakes
        {
            WEAPON,

            TORSO,
            HEAD,
            LEGS,
            HANDS,

            NECKLACE,
            CAPE,
            BELT,
            RING,

            PET,
            PET_LIGHT,
            CONTAINMENT,
        }

        public EquipmentSlotsTakes EquipmentSlotTake;
        public BattleHitStatsSet BattleItemStatsData;

        public Equipment(EquatableKey itemKey) : base(itemKey) { }

        public override void SetItem()
        {
            BattleItemStatsData = new BattleHitStatsSet();
        }

        public virtual void Draw(Model model) { }

        public static Dictionary<EquipmentSlot.EquipmentSlots, EquipmentSlotsTakes[]> EquipmentSlotTakeEquipmentSlotTypesMap = new()
        {
            { EquipmentSlot.EquipmentSlots.WEAPON,      new EquipmentSlotsTakes[] { EquipmentSlotsTakes.WEAPON }},
            { EquipmentSlot.EquipmentSlots.HEAD,      new EquipmentSlotsTakes[] { EquipmentSlotsTakes.HEAD }},
            { EquipmentSlot.EquipmentSlots.TORSO,  new EquipmentSlotsTakes[] { EquipmentSlotsTakes.TORSO }},
            { EquipmentSlot.EquipmentSlots.HANDS,      new EquipmentSlotsTakes[] { EquipmentSlotsTakes.HANDS }},
            { EquipmentSlot.EquipmentSlots.LEGS,       new EquipmentSlotsTakes[] { EquipmentSlotsTakes.LEGS }},
            { EquipmentSlot.EquipmentSlots.CAPE,        new EquipmentSlotsTakes[] { EquipmentSlotsTakes.CAPE }},
            { EquipmentSlot.EquipmentSlots.NECKLACE,    new EquipmentSlotsTakes[] { EquipmentSlotsTakes.NECKLACE }},
            { EquipmentSlot.EquipmentSlots.BELT,        new EquipmentSlotsTakes[] { EquipmentSlotsTakes.BELT }},
            { EquipmentSlot.EquipmentSlots.RING_0,      new EquipmentSlotsTakes[] { EquipmentSlotsTakes.RING }},
            { EquipmentSlot.EquipmentSlots.RING_1,      new EquipmentSlotsTakes[] { EquipmentSlotsTakes.RING }},
            { EquipmentSlot.EquipmentSlots.PET_0,         new EquipmentSlotsTakes[] { EquipmentSlotsTakes.PET }},
            { EquipmentSlot.EquipmentSlots.PET_1,   new EquipmentSlotsTakes[] { EquipmentSlotsTakes.PET_LIGHT }},
            { EquipmentSlot.EquipmentSlots.CONTAINMENT, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.CONTAINMENT }},
        };
    }
}