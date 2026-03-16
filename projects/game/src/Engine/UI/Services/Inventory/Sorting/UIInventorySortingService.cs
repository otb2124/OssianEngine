using Entities;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace UI
{
    public enum UIInventorySortingOptions
    {
        NONE,
        WEAPONS,
        ARMORS,
        ACCESSORIES,
        MATERIALS,
        CONSUMABLES,
        KEYS,
        QUEST_ITEMS,
        CURRENCIES
    };

    public class UIInventorySortingService
    {
        // The canonical, unmodified full inventory list. Never paged, never filtered.
        public List<Item> OriginalItemList;
        public UIInventorySortingOptions CurrentSortingOption;

        public UIInventorySortingService(List<Item> itemList)
        {
            OriginalItemList = itemList;
        }

        public void SetSortingOption(UIInventorySortingOptions newOption = UIInventorySortingOptions.NONE)
        {
            CurrentSortingOption = newOption;
        }

        // Returns filtered list (matching items only + null padding to original size).
        // NONE returns original list as-is.
        public List<Item> GetFilteredItems()
        {
            if (CurrentSortingOption == UIInventorySortingOptions.NONE)
                return OriginalItemList;

            List<Item> filtered = OriginalItemList
                .Where(item => item != null &&
                       ItemTypeToUISortingOption.TryGetValue(item.Type, out var opt) &&
                       opt == CurrentSortingOption)
                .OrderBy(item => item.ItemKey.EnumValue)
                .ToList();

            while (filtered.Count < OriginalItemList.Count)
                filtered.Add(null);

            return filtered;
        }

        // Syncs OriginalItemList after drag-drop. Updates counts, nullifies removed, inserts new.
        public void SyncFromDragResult(List<Item> updatedList)
        {
            for (int i = 0; i < OriginalItemList.Count; i++)
            {
                if (OriginalItemList[i] == null) continue;
                Item match = updatedList.FirstOrDefault(
                    item => item != null && item.ItemKey == OriginalItemList[i].ItemKey);
                if (match == null)
                    OriginalItemList[i] = null;
                else
                    OriginalItemList[i].Count = match.Count;
            }

            var tracked = new HashSet<EquatableKey>(
                OriginalItemList.Where(i => i != null).Select(i => i.ItemKey));

            foreach (var newItem in updatedList.Where(item => item != null && !tracked.Contains(item.ItemKey)))
            {
                int slot = OriginalItemList.IndexOf(null);
                if (slot == -1) break;
                OriginalItemList[slot] = newItem;
                tracked.Add(newItem.ItemKey);
            }
        }

        public static readonly Dictionary<ItemLib.ItemTypes, UIInventorySortingOptions> ItemTypeToUISortingOption = new()
        {
            { ItemLib.ItemTypes.WEAPON,      UIInventorySortingOptions.WEAPONS },
            { ItemLib.ItemTypes.CHESTPLATE,  UIInventorySortingOptions.ARMORS },
            { ItemLib.ItemTypes.HELMET,      UIInventorySortingOptions.ARMORS },
            { ItemLib.ItemTypes.BOOTS,       UIInventorySortingOptions.ARMORS },
            { ItemLib.ItemTypes.GLOVES,      UIInventorySortingOptions.ARMORS },
            { ItemLib.ItemTypes.NECKLACE,    UIInventorySortingOptions.ACCESSORIES },
            { ItemLib.ItemTypes.BELT,        UIInventorySortingOptions.ACCESSORIES },
            { ItemLib.ItemTypes.RING,        UIInventorySortingOptions.ACCESSORIES },
            { ItemLib.ItemTypes.CAPE,        UIInventorySortingOptions.ACCESSORIES },
            { ItemLib.ItemTypes.PET,         UIInventorySortingOptions.ACCESSORIES },
            { ItemLib.ItemTypes.PET_LIGHT,   UIInventorySortingOptions.ACCESSORIES },
            { ItemLib.ItemTypes.CONTAINMENT, UIInventorySortingOptions.ACCESSORIES },
            { ItemLib.ItemTypes.CONSUMABLE,  UIInventorySortingOptions.CONSUMABLES },
            { ItemLib.ItemTypes.MATERIAL,    UIInventorySortingOptions.MATERIALS },
            { ItemLib.ItemTypes.CURRENCY,    UIInventorySortingOptions.CURRENCIES },
            { ItemLib.ItemTypes.KEY,         UIInventorySortingOptions.KEYS },
            { ItemLib.ItemTypes.QUEST_ITEM,  UIInventorySortingOptions.QUEST_ITEMS },
        };
    }
}