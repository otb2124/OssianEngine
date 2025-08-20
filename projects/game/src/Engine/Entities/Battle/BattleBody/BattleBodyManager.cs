using Microsoft.Xna.Framework;
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
    public class BattleBodyManager
    {

        public List<BattleBody> BattleBodies;
        public Hitbox BodyHitbox;

        public BattleBodyManager() 
        {
            BattleBodies = new List<BattleBody>();
            BodyHitbox = new Hitbox();
        }

        public virtual void CreateBodies(int count)
        {
            BattleBodies.Clear();

            for (int i = 0; i < count; i++)
            {
                BattleBodies.Add(new BattleBody());
            }
        }

        public virtual void CreateBody()
        {
            BattleBodies.Add(new BattleBody());
        }

        public virtual void InitBody(int id, WeaponBodyData data)
        {
            BattleBodies[id].Init(data);
        }

        public virtual void Init(WeaponBodyData[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                BattleBodies[i].Init(data[i]);
            }
        }


        public virtual void Update(Model model)
        {
            foreach (BattleBody item in BattleBodies)
            {
                if(item != null)
                {
                    if(item.BattleBodyData != null)
                    {
                        item.Update(model);
                    }
                }
            }

            BodyHitbox.Update(model.Body.Position.ToVector2(), new Vector2(model.Body.Width, model.Body.Height), model.Body.Angle);
        }

        public virtual void Draw(Model model)
        {
            foreach (var item in BattleBodies)
            {
                item.Draw(model);
            }
        }

        public virtual void DrawHitboxes()
        {
            foreach (var item in BattleBodies)
            {
                item.DrawHitbox();
            }

            BodyHitbox.Draw(Color.Blue);
        }
    }
}
