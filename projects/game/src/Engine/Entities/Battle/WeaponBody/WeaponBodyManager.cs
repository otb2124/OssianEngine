using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = Resources.Model;

namespace Entities
{
    public class WeaponBodyManager
    {

        public List<WeaponBody> WeaponBodies;

        public WeaponBodyManager() 
        {
            WeaponBodies = new List<WeaponBody>();
        }

        public virtual void CreateBodies(int count)
        {
            WeaponBodies.Clear();

            for (int i = 0; i < count; i++)
            {
                WeaponBodies.Add(new WeaponBody());
            }
        }

        public virtual void CreateBody()
        {
            WeaponBodies.Add(new WeaponBody());
        }

        public virtual void InitBody(int id, WeaponBodyData data)
        {
            WeaponBodies[id].Init(data);
        }

        public virtual void Init(WeaponBodyData[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                WeaponBodies[i].Init(data[i]);
            }
        }

        public virtual void Update(Model model)
        {
            foreach (var item in WeaponBodies)
            {
                item.Update(model);
            }
        }

        public virtual void Draw(Model model)
        {
            foreach (var item in WeaponBodies)
            {
                item.Draw(model);
            }
        }

        public virtual void DrawHitboxes()
        {
            foreach (var item in WeaponBodies)
            {
                item.DrawHitbox();
            }
        }
    }
}
