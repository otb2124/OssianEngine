using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public enum UINavigationStates
    {
        NONE,
        CLEAR,
        INGAME_MENU_OPEN,
        TRADE_MENU_OPEN,
    }


    public static class UI
    {
        public static UIManager UIManager;

        public static UIOuterNavigator UIOuterNavigator;
        public static UIInnerNavigator UIInnerNavigator;

        public static UINavigationStates UIState;

        public static void Init()
        {
            UIManager = new UIManager();
            UIInnerNavigator = new UIInnerNavigator();
            UIOuterNavigator = new UIOuterNavigator();

            UIManager.Init();

            UIState = UINavigationStates.CLEAR;
        }
    }
}
