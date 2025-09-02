using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIManager
    {
        public static UIManager Instance { get; private set; }
        public List<UIComponent> components;
        private static int nextId = 10;

        public bool PreventButtonPressedOverlap = false;

        public UIManager()
        {
            Instance = this;
            components = new List<UIComponent>();
        }

        public void Init()
        {
            UI.UINavigator.HandleInitialNavigation();
        }

        public UIComponent GetComponent(UIComponent.UIComponentTypes type, int id = -1)
        {
            foreach (UIComponent component in components)
            {
                UIComponent result = FindComponentRecursive(component, type, id);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        private UIComponent FindComponentRecursive(UIComponent component, UIComponent.UIComponentTypes type, int id)
        {
            if (component.type == type && (id == -1 || component.Id == id))
            {
                return component;
            }

            if (component.children != null)
            {
                foreach (UIComponent child in component.children)
                {
                    UIComponent result = FindComponentRecursive(child, type, id);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }

        public bool RemoveComponent(UIComponent.UIComponentTypes type, int id)
        {
            for (int i = components.Count - 1; i >= 0; i--)
            {
                if (RemoveComponentRecursive(components[i], type, id, components, i))
                {
                    return true;
                }
            }
            return false;
        }

        private bool RemoveComponentRecursive(UIComponent component, UIComponent.UIComponentTypes type, int id, List<UIComponent> parentList, int indexInParent)
        {
            if (component.children != null)
            {
                List<UIComponent> childrenList = component.children.ToList();
                for (int j = childrenList.Count - 1; j >= 0; j--)
                {
                    if (RemoveComponentRecursive(childrenList[j], type, id, childrenList, j))
                    {
                        component.children = childrenList.ToArray();
                        return true;
                    }
                }
            }

            if (component.type == type && component.Id == id)
            {
                parentList.RemoveAt(indexInParent);
                return true;
            }

            return false;
        }

        public int RemoveAllComponents(UIComponent.UIComponentTypes type)
        {
            int removedCount = 0;

            for (int i = components.Count - 1; i >= 0; i--)
            {
                removedCount += RemoveAllComponentsRecursive(components[i], type, components, i);
            }

            return removedCount;
        }

        private int RemoveAllComponentsRecursive(UIComponent component, UIComponent.UIComponentTypes type, List<UIComponent> parentList, int indexInParent)
        {
            int removedCount = 0;

            if (component.children != null)
            {
                List<UIComponent> childrenList = component.children.ToList();
                for (int j = childrenList.Count - 1; j >= 0; j--)
                {
                    removedCount += RemoveAllComponentsRecursive(childrenList[j], type, childrenList, j);
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

        public void ToggleComponent(UIComponent component, UIComponent.UIComponentTypes type)
        {
            if (component == null)
            {
                return;
            }

            component.type = type;
            if (component.Id == -1)
            {
                component.Id = GenerateUniqueId();
            }

            if (GetComponent(type, component.Id) != null)
            {
                RemoveComponent(type, component.Id);
            }
            else
            {
                components.Add(component);
            }
        }

        public int GenerateUniqueId()
        {
            int id = nextId++;
            return id;
        }

        public void Update()
        {
            UI.UINavigator.HandleNavigation();

            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update();
            }
        }

        public void Draw()
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].type != UIComponent.UIComponentTypes.CURSOR)
                {
                    components[i].Draw();
                }
            }

            UIComponent cursor = GetComponent(UIComponent.UIComponentTypes.CURSOR);
            if (cursor != null)
            {
                cursor.Draw();
            }
        }

        public void DrawDebug()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].DrawDebug();
            }
        }
    }
}