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
        public static UINavigator UINavigator;
        public static UIButtonHandler UIButtonHandler;

        public static void Init()
        {
            UIManager = new UIManager();
            UINavigator = new UINavigator();
            UIButtonHandler= new UIButtonHandler();

            UIManager.Init();
        }
    }
}
