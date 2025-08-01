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
    public class EquipmentEntity : StatsEntity
    {

        public EquipmentManager EquipmentManager;

        public EquipmentEntity(Models modelPreset, Vector2 pos, float rot = 0) : base(modelPreset, pos, rot)
        {
            SetEquipment();
        }

        public virtual void SetEquipment()
        {
            EquipmentManager = new EquipmentManager();
        }


        public override void Update()
        {
            UpdateHitboxes();
            base.Update();
        }

        public virtual void UpdateHitboxes()
        {
            UpdateWeapon();
            UpdateArmor();
        }

        public virtual void UpdateWeapon()
        {
            EquipmentManager.GetCurrentWeapon().WeaponEntity.Update(Model);
        }

        public virtual void UpdateArmor()
        {
            EquipmentManager.GetCurrentArmor().Update(Model);
        }

        public virtual void DrawWeapon()
        {
            EquipmentManager.Draw(Model);
        }

        public override void DrawHitboxes()
        {
            EquipmentManager.DrawHitbox();
        }
    }
}
