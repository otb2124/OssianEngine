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

            CanRegensStamina = false;
            CanUpdateIFrames = true;
            CanFall = false;

            Stats.maxHP = 100;
            Stats.Refill();
        }


        public override void Update()
        {
            if(Model.body.BodyShapeType == BodyShapeType.Box)
            {
                UpdateBodyHitbox(FlatConverter.ToVector2(this.Model.body.Position), new Vector2(this.Model.body.Width, this.Model.body.Height), Model.body.Angle);
            }
            else
            {
                UpdateBodyHitbox(FlatConverter.ToVector2(this.Model.body.Position), new Vector2(this.Model.body.Radius*2f, this.Model.body.Radius*2f), Model.body.Angle);
            }
            
            //UpdateDamageHitbox(FlatConverter.ToVector2(this.Model.body.Position), new Vector2(this.Model.body.Width, this.Model.body.Height), 0f);

            base.Update();
        }
    }
}
