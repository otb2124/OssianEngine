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
        private BattleCombo Combo;
        private RotatedRectangle NoAttackHitbox;
        private Model Model;

        private LightSourceData TempData;

        public WeaponLightSource(LightSourceData data) : base(Vector2.Zero, data)
        {
            Id = Graphics.LightManager.GenerateId();
        }

        public void Init(BattleCombo combo, RotatedRectangle noAttackHitbox, Model model, LightSourceData newData)
        {
            Combo = combo;
            NoAttackHitbox = noAttackHitbox;
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
                var currentHit = Combo.GetCurrentHit();
                Vector2 horizontalXFactor = Model.Direction == Directions.RIGHT ? new Vector2(1, 1) : new Vector2(-1, 1);

                Vector2 modelCenter = new Vector2(Model.Body.Position.ToVector2().X + Model.Body.Width / 2f, Model.Body.Position.ToVector2().Y + Model.Body.Height / 2f);

                Vector2 oldWeaponPosition = modelCenter + NoAttackHitbox.Position * horizontalXFactor;


                Vector2 weaponRotOffset = new Vector2(0, NoAttackHitbox.Height);
                weaponRotOffset = Vector2.Transform(weaponRotOffset, Matrix.CreateRotationZ(NoAttackHitbox.Rotation));
                Vector2 lightPos = oldWeaponPosition - (weaponRotOffset * horizontalXFactor);

                if (currentHit != null)
                {
                    oldWeaponPosition = modelCenter + currentHit.HitboxOffset.Position * horizontalXFactor;
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
