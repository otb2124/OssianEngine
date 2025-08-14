using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public static class EntityHelper
    {

        public static InteractiveItemEntity CreateItemDrop(ItemKey itemKey, Vector2 pos)
        {
            return new InteractiveItemEntity(StaticSpriteFactory.GetItemUISpriteByItemKey(itemKey), FlatBodyPreset.ITEM_DROP, pos, new Vector2(30, 30), InteractiveItemEntity.InteractiveItemType.PICKUP, new Inventory(new ItemKey[] { itemKey }));
        }

        public static InteractiveItemEntity CreateItemDrop(Item item, Vector2 pos)
        {
            return new InteractiveItemEntity(StaticSpriteFactory.GetItemUISpriteByItemKey(item.ItemKey), FlatBodyPreset.ITEM_DROP, pos, new Vector2(30, 30), InteractiveItemEntity.InteractiveItemType.PICKUP, new Inventory(new Item[] { item }));
        }
    }
}
