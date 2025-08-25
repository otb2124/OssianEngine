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
    public enum WeaponHands
    {
        LEFT,
        RIGHT,
        BOTH
    }

    public enum BattleBodyTypes
    {
        WEAPON,
        BODY
    }

    public class BattleBodyManager
    {

        public List<BattleBody> BattleBodies;
        public Hitbox BodyHitbox;
        public BattleBodyTypes BattleBodyType;

        public BattleBodyManager(BattleBodyTypes bodyType) 
        {
            BattleBodies = new List<BattleBody>();
            BodyHitbox = new Hitbox();
            BattleBodyType = bodyType;

            Init();
        }

        public void Init()
        {
            if (BattleBodyType == BattleBodyTypes.WEAPON)
                CreateBodies(2);
            else
                CreateBodies(1);
        }

        public void CreateBodies(int count)
        {
            BattleBodies.Clear();

            for (int i = 0; i < count; i++)
            {
                BattleBodies.Add(new BattleBody());
            }
        }

        public void CreateBody()
        {
            BattleBodies.Add(new BattleBody());
        }

        public void InitBody(int id, BattleBodyData data)
        {
            BattleBodies[id].Init(data);
        }

        public void InitBodies(BattleBodyData[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                BattleBodies[i].Init(data[i]);
            }
        }

        public void Update(Model model)
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

        public void Draw(Model model)
        {
            foreach (var item in BattleBodies)
            {
                item.Draw(model);
            }
        }

        public void DrawHitboxes()
        {
            foreach (var item in BattleBodies)
            {
                item.DrawHitbox();
            }

            BodyHitbox.Draw(Color.Blue);
        }


        public BattleBody HandToEquipmentWeaponBody(WeaponHands hand) =>
           hand == WeaponHands.LEFT ? BattleBodies[0] : BattleBodies[1];


        public BattleHitData GetCurrentBattleHitData()
        {
            return BattleBodies[0].Combo.GetCurrentHit().BattleHitData;
        }

        public BattleHitData GetCurrentBattleHitData(EquipmentManager equipmentManager)
        {
            if(HandToEquipmentWeaponBody(equipmentManager.CurrentHand).Combo.GetCurrentHit() != null)
            {
                BattleHitData data = HandToEquipmentWeaponBody(equipmentManager.CurrentHand).Combo.GetCurrentHit().BattleHitData;
                if (data != null)
                {
                    return data;
                }
            }
            
            return new BattleHitData(1, 1, 1, 1);
        }
    }
}
