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

        public static InteractiveItemEntity CreateItemDrop(EquatableKey itemKey, Vector2 pos)
        {
            return new InteractiveItemEntity(StaticSpriteFactory.GetItemUISpriteByItemKey(itemKey), PhysicalBodies.ITEM_DROP, pos, new Vector2(30, 30), new Inventory(new EquatableKey[] { itemKey }), new InteractionData(InteractionTriggers.INTERACTION_BUTTON_PRESSED, InteractionActions.ADD_ITEM_TO_INVENTORY));
        }

        public static InteractiveItemEntity CreateItemDrop(Item item, Vector2 pos)
        {
            return new InteractiveItemEntity(StaticSpriteFactory.GetItemUISpriteByItemKey(item.ItemKey), PhysicalBodies.ITEM_DROP, pos, new Vector2(30, 30), new Inventory(new Item[] { item }), new InteractionData(InteractionTriggers.INTERACTION_BUTTON_PRESSED, InteractionActions.ADD_ITEM_TO_INVENTORY));
        }
    }
}
