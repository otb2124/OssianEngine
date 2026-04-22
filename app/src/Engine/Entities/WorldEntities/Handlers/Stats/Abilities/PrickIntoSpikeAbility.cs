using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;

namespace Entities
{
    public class PrickIntoSpikeAbility : EntityAbility
    {


        public PrickIntoSpikeAbility()
        {
            Type = EntityStatFeatures.PRICK_INTO_SPIKE;
        }

        public override void Update(StatsManager statsManager, Model model)
        {
            SpikeEntity spike = CollisionHelper.GetAnySpikes(model.Body);
            if (spike != null)
            {
                Vector2 entityPos = model.Body.Position.ToVector2();
                Vector2 spikePos = spike.Model.Body.Position.ToVector2();

                Vector2 pushDirection = Vector2.Normalize(entityPos - spikePos);

                BattleHitHandler.HandleHit((BattleEntity)Entities.EntityManager.GetEntityById(model.OwnerId), spike);
            }
        }
    }
}
