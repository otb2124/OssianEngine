using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{

    public enum UIInventorySortingOptions
    {
        WEAPONS,
        ARMORS,
        ACCESSORIES,
        POTIONS,
        MISC,
    };

    public class UIInventorySortingService
    {

        public List<Item> TotalItems;

        public UIInventorySortingService(List<Item> totalItems)
        {
            TotalItems = totalItems;
        }

        public List<Item> GetSortedItems(UIInventorySortingOptions newOption = UIInventorySortingOptions.WEAPONS)
        {

            List<Item> sortedItems = TotalItems
                .Where(item => item != null && ItemTypeToUISortingOption.TryGetValue(item.Type, out var sortingOption) && sortingOption == newOption)
                .ToList();

            while (sortedItems.Count < TotalItems.Count)
            {
                sortedItems.Add(null);
            }

            return sortedItems;
        }

        public List<Item> GetChangedItems(List<Item> newItems, UIInventorySortingOptions sortingOption)
        {
            TotalItems.RemoveAll(item => item != null && ItemTypeToUISortingOption.TryGetValue(item.Type, out var option) && option == sortingOption);

            TotalItems.AddRange(newItems.Where(item => item != null));

            int originalSize = TotalItems.Count;
            while (TotalItems.Count < originalSize)
            {
                TotalItems.Add(null);
            }

            return TotalItems;
        }

        public static Dictionary<ItemLib.ItemTypes, UIInventorySortingOptions> ItemTypeToUISortingOption = new()
        {
            { ItemLib.ItemTypes.WEAPON, UIInventorySortingOptions.WEAPONS },

            { ItemLib.ItemTypes.CHESTPLATE, UIInventorySortingOptions.ARMORS },
            { ItemLib.ItemTypes.HELMET, UIInventorySortingOptions.ARMORS },
            { ItemLib.ItemTypes.BOOTS, UIInventorySortingOptions.ARMORS },
            { ItemLib.ItemTypes.GLOVES, UIInventorySortingOptions.ARMORS },

            { ItemLib.ItemTypes.NECKLACE, UIInventorySortingOptions.ACCESSORIES },
            { ItemLib.ItemTypes.BELT, UIInventorySortingOptions.ACCESSORIES },
            { ItemLib.ItemTypes.RING, UIInventorySortingOptions.ACCESSORIES },
            { ItemLib.ItemTypes.CAPE, UIInventorySortingOptions.ACCESSORIES },
            { ItemLib.ItemTypes.PET, UIInventorySortingOptions.ACCESSORIES },
            { ItemLib.ItemTypes.PET_LIGHT, UIInventorySortingOptions.ACCESSORIES },
            { ItemLib.ItemTypes.CONTAINMENT, UIInventorySortingOptions.ACCESSORIES },

            { ItemLib.ItemTypes.CONSUMABLE, UIInventorySortingOptions.POTIONS },
            { ItemLib.ItemTypes.MATERIAL, UIInventorySortingOptions.POTIONS },

            { ItemLib.ItemTypes.CURRENCY, UIInventorySortingOptions.MISC },
            { ItemLib.ItemTypes.KEY, UIInventorySortingOptions.MISC },
            { ItemLib.ItemTypes.QUEST_ITEM, UIInventorySortingOptions.MISC },
        };
    }
}
