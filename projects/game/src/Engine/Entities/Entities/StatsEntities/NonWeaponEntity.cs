using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class NonWeaponEntity : StatsEntity
    {

        public Hitbox BodyHitbox;
        public Hitbox DamageHitbox;


        public NonWeaponEntity(Models modelPreset, Vector2 pos, float rotation = 0f) : base(modelPreset, pos, rotation)
        {
            SetHitboxes();
        }

        public virtual void SetHitboxes()
        {
            BodyHitbox = new Hitbox();
            DamageHitbox = new Hitbox();
        }

        

        public virtual void UpdateBodyHitbox(Vector2 pos, Vector2 size, float rot = 0f)
        {
            BodyHitbox.Update(pos, size, rot);
        }

        public virtual void UpdateDamageHitbox(Vector2 pos, Vector2 size, float rot = 0f)
        {
            DamageHitbox.Update(pos, size, rot);
        }

        public override void DrawHitboxes()
        {
            BodyHitbox.Draw(Color.Blue);
            DamageHitbox.Draw(Color.Red);
        }
    }
}
