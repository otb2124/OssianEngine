using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using System;
using System.Collections.Generic;

namespace UI
{
    public class UIComposite
    {

        public Vector2 Position;

        public List<UIComponent> components;
        public List<UIComposite> children;

        public UIComposite()
        {
            components = new List<UIComponent>();
            children = new List<UIComposite>();
        }


        //public virtual void Update()
        //{
        //    for (int i = 0; i < children.Count; i++)
        //    {
        //        children[i].Update();
        //    }

        //    for (int i = 0; i < components.Count; i++)
        //    {
        //        components[i].Update();
        //    }

        //}



        //public virtual void 



        //    ()
        //{
        //    for (int i = 0; i < components.Count; i++)
        //    {
        //        components[i].Draw();
        //    }

        //    for (int i = 0; i < children.Count; i++)
        //    {
        //        children[i].Draw();
        //    }
        //}



    }
}