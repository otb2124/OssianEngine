using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public static class GameManager
    {
        
        public static ResourceLoader resourceLoader;

        public static void Init()
        {
            resourceLoader = new ResourceLoader();
        }

        public static void LoadResources()
        {
            resourceLoader.LoadResources();
        }


        public static void Update() 
        {

        }
    }
}
