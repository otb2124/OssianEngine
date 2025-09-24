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

        public List<Item> OriginalItemList;
        public UIInventorySortingOptions LastSortingOption;

        public UIInventorySortingService(List<Item> itemList)
        {
            OriginalItemList = itemList;
        }

        public List<Item> GetSortedItems(UIInventorySortingOptions newOption = UIInventorySortingOptions.NONE)
        {
            LastSortingOption = newOption;

            if (newOption == UIInventorySortingOptions.NONE)
                return OriginalItemList;

            List<Item> sortedItems = OriginalItemList
                .Where(item => item != null && ItemTypeToUISortingOption.TryGetValue(item.Type, out var sortingOption) && sortingOption == newOption)
                .OrderBy(item => item.Name)
                .ToList();

            sortedItems.AddRange(OriginalItemList
                .Where(item => item != null && (!ItemTypeToUISortingOption.TryGetValue(item.Type, out var sortingOption) || sortingOption != newOption)));

            while (sortedItems.Count < OriginalItemList.Count)
            {
                sortedItems.Add(null);
            }

            return sortedItems;
        }


        public void UpdateOriginalItemList(List<Item> itemList)
        {
            for (int i = 0; i < OriginalItemList.Count; i++)
            {
                if (OriginalItemList[i] != null && !itemList.Any(item => item != null && item.ItemKey == OriginalItemList[i].ItemKey))
                {
                    OriginalItemList[i] = null;
                }
            }

            var newItems = itemList
                .Where(item => item != null && !OriginalItemList.Any(orig => orig != null && orig.ItemKey == item.ItemKey))
                .ToList();

            int nullIndex = 0;
            foreach (var newItem in newItems)
            {
                while (nullIndex < OriginalItemList.Count && OriginalItemList[nullIndex] != null)
                {
                    nullIndex++;
                }

                if (nullIndex < OriginalItemList.Count)
                {
                    OriginalItemList[nullIndex] = newItem;
                    nullIndex++;
                }
                else
                {
                    break;
                }
            }
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

            { ItemLib.ItemTypes.CONSUMABLE, UIInventorySortingOptions.CONSUMABLES },
            { ItemLib.ItemTypes.MATERIAL, UIInventorySortingOptions.MATERIALS },

            { ItemLib.ItemTypes.CURRENCY, UIInventorySortingOptions.CURRENCIES },
            { ItemLib.ItemTypes.KEY, UIInventorySortingOptions.KEYS },
            { ItemLib.ItemTypes.QUEST_ITEM, UIInventorySortingOptions.QUEST_ITEMS },
        };
    }
}
