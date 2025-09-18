using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIOuterNavigator
    {


        public void HandleNavigation()
        {
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.TOGGLEMENUPRESSED])
            {
                UI.UIManager.RemoveComponent(UIComponent.UIComponentTypes.INVENTORY_TO_INVENTORY, 999);
            }
        }


        public void ToggleTradeMenu(StatsEntity entFrom, StatsEntity entTo)
        {
            UI.UIManager.ToggleComponent(new UIInventoryInventoryBoardsComponent(999, Vector2.Zero, entFrom.Inventory, entTo.Inventory), UIComponent.UIComponentTypes.INVENTORY_TO_INVENTORY);
        }
    }
}
