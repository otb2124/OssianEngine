using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DestroyableEntity : NonHumanoidEntity
    {



        public DestroyableEntity(Utils.Models modelPreset, Vector2 pos, float rot = 0) : base(modelPreset, pos, rot)
        {

        }


        public override void SetStats()
        {
            base.SetStats();

            Stats.maxHP = 100;
            Stats.Refill();
        }


        public override void Update()
        {
            UpdateBodyHitbox(FlatConverter.ToVector2(this.Model.body.Position), new Vector2(this.Model.body.Width, this.Model.body.Height), 0f);
            UpdateDamageHitbox(FlatConverter.ToVector2(this.Model.body.Position), new Vector2(this.Model.body.Width, this.Model.body.Height), 0f);

            base.Update();
        }
    }
}
