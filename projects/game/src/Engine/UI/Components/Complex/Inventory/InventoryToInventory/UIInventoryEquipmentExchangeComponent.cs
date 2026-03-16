using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace UI
{
    // Hosts the inventory grid + equipment grid side by side.
    // Right-click an inventory item  -> equip it (displaced item goes back to inventory).
    // Right-click an equipment slot  -> unequip it (item goes to first free inventory slot).
    public class UIInventoryEquipmentExchangeComponent : UIComponent
    {
        public Inventory Inventory;
        public EquipmentManager EquipmentManager;
        public BattleBodyManager BattleBodyManager;

        private UIInventoryComponent InvComp => (UIInventoryComponent)children[0];
        private UIEquipmentComponent EqComp => (UIEquipmentComponent)children[1];

        public UIInventoryEquipmentExchangeComponent(
            int id, Vector2 pos,
            Inventory inventory,
            EquipmentManager equipmentManager,
            BattleBodyManager battleBodyManager) : base(id)
        {
            Position = pos;
            type = UIComponentTypes.INVENTORY_TO_EQUIPMENT;

            Inventory = inventory;
            EquipmentManager = equipmentManager;
            BattleBodyManager = battleBodyManager;

            children = new UIComponent[2];
            children[0] = new UIInventoryComponent(-1, new Vector2(100, 500), Inventory);
            children[1] = new UIEquipmentComponent(-1, new Vector2(500, 500), EquipmentManager);
        }

        public override void Update()
        {
            for (int i = 0; i < children.Length; i++)
                children[i]?.Update();

            // Right-click inventory slot -> equip
            if (InvComp.ClickedSlot != null && InvComp.SlotRightClicked)
                TryEquip(InvComp.ClickedSlot.Item);

            // Right-click equipment slot -> unequip
            if (EqComp.ClickedSlot != null && EqComp.SlotRightClicked)
                TryUnequip(EqComp.ClickedSlot);
        }

        // ── Equip ────────────────────────────────────────────────────────────

        private void TryEquip(Item item)
        {
            if (!(item is Equipment eq)) return;

            // Weapons always go to the right hand slot — never fall through to left
            EquipmentSlot.EquipmentSlotTypes targetSlotType;
            if (eq.EquipmentSlotTake == Equipment.EquipmentSlotsTakes.WEAPON)
            {
                targetSlotType = EquipmentSlot.EquipmentSlotTypes.WEAPON;
            }
            else
            {
                try
                {
                    targetSlotType = EquipmentHelper.ItemkeyToEquipmentSlot(
                        eq.ItemKey, EquipmentManager.Equipments.EquipmentSlots);
                }
                catch
                {
                    return;
                }
            }

            // Get whatever is currently in that slot
            EquipmentSlot targetSlot = EquipmentManager.Equipments.GetEquipmentSlot(targetSlotType);
            Equipment displaced = targetSlot.Equipment;

            // Put the new item in
            targetSlot.Equipment = eq;

            // Remove item from inventory
            int invIndex = Inventory.Items.IndexOf(item);
            if (invIndex != -1) Inventory.Items[invIndex] = null;

            // Return displaced item to inventory (if it's not bare hands / null)
            if (displaced != null && !IsBareHands(displaced))
                PlaceInInventory(displaced);

            // Update weapon toggler if needed
            if (eq.EquipmentSlotTake == Equipment.EquipmentSlotsTakes.WEAPON)
                ApplyWeapon(eq as WeaponEquipment ?? EquipmentHelper.CreateBareHands());

            Refresh();
        }

        // ── Unequip ──────────────────────────────────────────────────────────

        private void TryUnequip(UIInventorySlotComponent slot)
        {
            if (slot.EquipmentSlotType == EquipmentSlot.EquipmentSlotTypes.NONE) return;

            EquipmentSlot eqSlot = EquipmentManager.Equipments.GetEquipmentSlot(slot.EquipmentSlotType);
            Equipment item = eqSlot.Equipment;

            if (item == null || IsBareHands(item)) return;

            // Place item back in inventory
            if (!PlaceInInventory(item)) return; // inventory full, don't unequip

            // Clear the slot
            eqSlot.Equipment = null;

            // Reset weapon if needed
            if (item.EquipmentSlotTake == Equipment.EquipmentSlotsTakes.WEAPON)
                ApplyWeapon(EquipmentHelper.CreateBareHands());

            Refresh();
        }

        // ── Weapon toggler ───────────────────────────────────────────────────

        private void ApplyWeapon(WeaponEquipment weapon)
        {
            if (EquipmentManager.WeaponInOutToggler.IsWeaponOut)
                EquipmentManager.ToggleWeaponInOut(BattleBodyManager);

            EquipmentManager.SetWeapon(BattleBodyManager, weapon);
        }

        // ── Helpers ──────────────────────────────────────────────────────────

        private bool PlaceInInventory(Item item)
        {
            int slot = Inventory.Items.IndexOf(null);
            if (slot == -1) return false;
            Inventory.Items[slot] = item;
            return true;
        }

        private static bool IsBareHands(Equipment eq) =>
            eq?.ItemKey.EnumValue is ItemLib.Weapons w && w == ItemLib.Weapons.BARE_HAND;

        public override void Refresh()
        {
            InvComp.SortingService.OriginalItemList = Inventory.Items;
            InvComp.RefreshBoard();
            EqComp.RefreshBoard();
        }

        public override void Draw()
        {
            for (int i = 0; i < children.Length; i++)
                children[i]?.Draw();
        }
    }
}