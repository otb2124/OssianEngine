using Resources;
using System;
using System.Collections.Generic;
using Utils;
using static Entities.PhysicalEntity;


namespace Entities
{
    public class Equipment : Item
    {

        public enum EquipmentSlotsTakes 
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
            RING,

            PET,
            PET_LIGHT,
            CONTAINMENT,
        }

        public EquipmentSlotsTakes EquipmentSlotTake;

        public BattleHitStatsData BattleItemStatsData;

        public Equipment(ItemKey itemKey) : base(itemKey)
        {
            
        }

        public override void SetItem()
        {
            BattleItemStatsData = new BattleHitStatsData();
        }

        public virtual void Draw(Model model){}



        public static Dictionary<EquipmentSlot.EquipmentSlotTypes, EquipmentSlotsTakes[]> EquipmentSlotTakeEquipmentSlotTypesMap = new()
        {
            { EquipmentSlot.EquipmentSlotTypes.WEAPON_L, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.WEAPON_SINGLE, EquipmentSlotsTakes.WEAPON_DOUBLE }},
            { EquipmentSlot.EquipmentSlotTypes.WEAPON_R, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.WEAPON_SINGLE, EquipmentSlotsTakes.WEAPON_DOUBLE }},
            { EquipmentSlot.EquipmentSlotTypes.HELMET, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.HEAD }},
            { EquipmentSlot.EquipmentSlotTypes.CHESTPLATE, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.TORSO }},
            { EquipmentSlot.EquipmentSlotTypes.GLOVES, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.HANDS }},
            { EquipmentSlot.EquipmentSlotTypes.BOOTS, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.LEGS }},
            { EquipmentSlot.EquipmentSlotTypes.CAPE, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.CAPE }},
            { EquipmentSlot.EquipmentSlotTypes.NECKLACE, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.NECKLACE }},
            { EquipmentSlot.EquipmentSlotTypes.BELT, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.BELT }},
            { EquipmentSlot.EquipmentSlotTypes.RING_L, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.RING }},
            { EquipmentSlot.EquipmentSlotTypes.RING_R, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.RING }},
            { EquipmentSlot.EquipmentSlotTypes.PET, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.PET }},
            { EquipmentSlot.EquipmentSlotTypes.PET_LIGHT, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.PET_LIGHT }},
            { EquipmentSlot.EquipmentSlotTypes.CONTAINMENT, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.CONTAINMENT }},
        };
        //dictionary for EquipmentSlotTake == EquipmentSlot


        public static Dictionary<int, EquipmentSlotsTakes[]> EquipmentSlotTakeIntEquipmentSlotTypesMap = new()
        {
            { 0, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.WEAPON_SINGLE, EquipmentSlotsTakes.WEAPON_DOUBLE }},
            { 1, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.WEAPON_SINGLE, EquipmentSlotsTakes.WEAPON_DOUBLE }},
            { 2, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.HEAD }},
            { 3, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.TORSO }},
            { 4, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.HANDS }},
            { 5, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.LEGS }},
            { 6, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.CAPE }},
            { 7, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.NECKLACE }},
            { 8, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.BELT }},
            { 10, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.RING }},
            { 11, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.RING }},
            { 12, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.PET }},
            { 13, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.PET_LIGHT }},
            { 14, new EquipmentSlotsTakes[] { EquipmentSlotsTakes.CONTAINMENT }},
        };
        //dictionary for EquipmentSlotTake == EquipmentSlot
    }
}
