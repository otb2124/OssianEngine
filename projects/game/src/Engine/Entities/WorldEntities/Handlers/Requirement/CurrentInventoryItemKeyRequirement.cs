using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CurrentInventoryItemKeyRequirement : Requirement
    {

        public EquatableKey ItemKey;

        public CurrentInventoryItemKeyRequirement(EquatableKey itemkey) : base()
        {
            ItemKey = itemkey;
        }

        public override bool Check()
        {
            return Entities.Player.Inventory.GetItemWithItemKey(ItemKey) != null;
        }
    }
}
