using Entities;
using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Graphics
{
    public class WeaponLightSource : LightSource
    {
        private WeaponBody WeaponBody;
        private Model Model;

        private LightSourceData TempData;

        public WeaponLightSource(LightSourceData data) : base(Vector2.Zero, data)
        {
        }

        public void Init(WeaponBody weaponBody, Model model, LightSourceData newData)
        {
            WeaponBody = weaponBody;
            Model = model;
            Data = newData;
            TempData = newData;
        }

        public override void Update()
        {
            if (Model.ModelState == ModelStates.WEAPON_OUT_IDLE ||
                Model.ModelState == ModelStates.WEAPON_OUT_MOVING ||
                Model.ModelState == ModelStates.ATTACKING_HEAVY ||
                Model.ModelState == ModelStates.ATTACKING_LIGHT ||
                Model.ModelState == ModelStates.BLOCKING)
            {
                var currentHit = WeaponBody.Combo.GetCurrentHit();
                int horizontalXFactor = Model.Direction == Directions.RIGHT ? 1 : -1;

                Vector2 oldWeaponPosition = Model.Body.Position.ToVector2() + WeaponBody.NoAttackHitbox.Position * new Vector2(horizontalXFactor, 1f);
                Vector2 weaponRotOffset = new Vector2(0, WeaponBody.NoAttackHitbox.Height);
                weaponRotOffset = Vector2.Transform(weaponRotOffset, Matrix.CreateRotationZ(WeaponBody.NoAttackHitbox.Rotation));
                Vector2 lightPos = oldWeaponPosition - (weaponRotOffset * horizontalXFactor);

                if (currentHit != null)
                {
                    oldWeaponPosition = Model.Body.Position.ToVector2() + currentHit.HitboxOffset.Position * new Vector2(horizontalXFactor, 1f);
                    weaponRotOffset = new Vector2(0, currentHit.HitboxOffset.Height);
                    weaponRotOffset = Vector2.Transform(weaponRotOffset, Matrix.CreateRotationZ(currentHit.HitboxOffset.Rotation));
                    lightPos = oldWeaponPosition - (weaponRotOffset * horizontalXFactor);
                }

                Position = lightPos;
                Data = TempData;
            }
            else
            {
                Position = Vector2.Zero;
                Data = null;
            }
        }
    }

}
