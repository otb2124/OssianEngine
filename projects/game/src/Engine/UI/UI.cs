using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{


    public static class UI
    {
        public static UIManager UIManager;

        public static UIOuterNavigator UIOuterNavigator;
        public static UIInnerNavigator UIInnerNavigator;

        public static bool PreventButtonPressedOverlap = false;

        public static void Init()
        {
            UIManager = new UIManager();
            UIInnerNavigator = new UIInnerNavigator();
            UIOuterNavigator = new UIOuterNavigator();

            UIManager.Init();
        }
    }
}
