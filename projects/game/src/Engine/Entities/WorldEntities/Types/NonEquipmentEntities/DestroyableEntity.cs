using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DestroyableEntity : StatsEntity
    {



        public DestroyableEntity(Utils.Models modelPreset, Vector2 pos, float rot = 0) : base(modelPreset, pos, rot)
        {
            SetStats();
            //UpdateSlots();
            //SetDropInventory();
        }

        public override void SetAnimations()
        {
            base.SetAnimations();
        }

        public override void SetStats()
        {
            base.SetStats();

            CanRegensStamina = false;
            CanUpdateIFrames = true;
            CanFall = false;

            StatsManager.IndicatorStats = new IndicatorStats(100, 0, 0);
            StatsManager.Refill();
        }


        public override void Update()
        {
            //UpdateDamageHitbox(FlatConverter.ToVector2(this.Model.Body.Position), new Vector2(this.Model.Body.Width, this.Model.Body.Height), 0f);

            base.Update();
        }
    }
}
