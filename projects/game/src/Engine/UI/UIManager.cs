using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIManager
    {
        public List<UIComponent> components;


        public UIManager()
        {
            components = new List<UIComponent>();
        }

        public void Init()
        {
            UI.UINavigator.HandleInitialNavigation();
        }


        public UIComponent GetComponent(UIComponent.ComponentTypes type)
        {
            foreach (UIComponent component in components)
            {
                UIComponent result = FindComponentRecursive(component, type);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        private UIComponent FindComponentRecursive(UIComponent component, UIComponent.ComponentTypes type)
        {
            if (component.type == type)
            {
                return component;
            }

            if (component.children != null)
            {
                foreach (UIComponent child in component.children)
                {
                    UIComponent result = FindComponentRecursive(child, type);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }

        public bool RemoveLatestComponent(UIComponent.ComponentTypes type)
        {

            for (int i = components.Count - 1; i >= 0; i--)
            {
                if (RemoveLatestComponentRecursive(components[i], type, components, i))
                {
                    return true;
                }
            }
            return false;
        }

        private bool RemoveLatestComponentRecursive(UIComponent component, UIComponent.ComponentTypes type, List<UIComponent> parentList, int indexInParent)
        {

            if (component.children != null)
            {
                for (int i = component.children.Length - 1; i >= 0; i--)
                {
                    if (RemoveLatestComponentRecursive(component.children[i], type, component.children.ToList(), i))
                    {
                        component.children = component.children.ToList().Where((c, idx) => idx != i).ToArray();
                        return true;
                    }
                }
            }

            if (component.type == type)
            {
                parentList.RemoveAt(indexInParent);
                return true;
            }

            return false;
        }

        public int RemoveAllComponents(UIComponent.ComponentTypes type)
        {
            int removedCount = 0;

            for (int i = components.Count - 1; i >= 0; i--)
            {
                removedCount += RemoveAllComponentsRecursive(components[i], type, components, i);
            }

            return removedCount;
        }

        private int RemoveAllComponentsRecursive(UIComponent component, UIComponent.ComponentTypes type, List<UIComponent> parentList, int indexInParent)
        {
            int removedCount = 0;

            if (component.children != null)
            {
                List<UIComponent> childrenList = component.children.ToList();
                for (int i = childrenList.Count - 1; i >= 0; i--)
                {
                    removedCount += RemoveAllComponentsRecursive(childrenList[i], type, childrenList, i);
                }
                component.children = childrenList.ToArray();
            }

            if (component.type == type)
            {
                parentList.RemoveAt(indexInParent);
                removedCount++;
            }

            return removedCount;
        }


        public void Update()
        {
            UI.UINavigator.HandleNavigation();

            {
                for (int i = 0; i < components.Count; i++)
                {
                    components[i].Update();
                }
            }
        }

        public void Draw()
        {
            {
                for (int i = 0; i < components.Count; i++)
                {
                    components[i].Draw();
                }
            }
        }
    }
}