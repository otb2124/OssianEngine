using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class CurrentInventoryItemKeyRequirement : Requirement
    {

        public EquatableKey ItemKey;

        public CurrentInventoryItemKeyRequirement(EquatableKey itemkey) : base()
        {
            ItemKey = itemkey;
        }

        public override bool Check(StatsEntity Entity)
        {
            return Entity.Inventory.GetItemWithItemKey(ItemKey) != null;
        }
    }
}
