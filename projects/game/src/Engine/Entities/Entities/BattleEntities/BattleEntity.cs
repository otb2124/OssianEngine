using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using MathHelper = Utils.MathHelper;

namespace Entities
{
    public class BattleEntity : StatsEntity
    {

        public WeaponBodyManager WeaponBodyManager;

        public BattleEntity(Models modelPreset, Vector2 pos, float rot = 0) : base(modelPreset, pos, rot)
        {
            SetWeaponBodies();
        }

        public BattleEntity() : base()
        {
            SetWeaponBodies();
        }

        public virtual void SetWeaponBodies()
        {
            WeaponBodyManager = new WeaponBodyManager();
        }


        public override void Update()
        {
            base.Update();
        }

        public virtual void DrawWeapon()
        {
            //EquipmentManager.Draw(Model);
        }

        public override void DrawHitboxes()
        {
            //EquipmentManager.DrawHitbox();
        }
    }
}
