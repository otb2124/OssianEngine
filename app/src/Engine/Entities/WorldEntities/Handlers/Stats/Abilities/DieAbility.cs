using Resources;
using System;
using Utils;

namespace Entities
{
    public class DieAbility : EntityAbility
    {

        public bool IsDying = false;

        public DieAbility()
        {
            Type = EntityStatFeatures.DIE;
        }

        public override void Update(StatsManager statsManager, Model model)
        {
            if (GameStateManager.IsGod && model.OwnerId == Entities.Player.Id)
                return;


            if (statsManager.CheckDead())
            {
                IsDying = true;
            }
        }
    }
}