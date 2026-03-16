using Physics;
using Resources;

namespace Entities
{
    public class LadderClimbingAbility : EntityAbility
    {


        public LadderClimbingAbility()
        {
            Type = EntityStatFeatures.LADDER_CLIMBING;
        }

        public override void Update(StatsManager statsManager, Model model)
        {

            LadderEntity ladder = CollisionHelper.GetAnyLadders(model.Body);
            if (ladder != null)
            {
                if (Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.MOVEUPPRESSED])
                {
                    model.ModelState = ModelStates.CLIMBING_LADDER;
                }

                if (model.ModelState == ModelStates.CLIMBING_LADDER)
                {
                    model.Body.IsFrozen = true;

                    if (Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
                    {
                        model.Body.Move(new PhysicalVector(statsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * -1f, 0));
                    }
                            
                    if(Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
                    {
                        model.Body.Move(new PhysicalVector(statsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * 1f, 0));
                    }

                    if (Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.MOVEUPPRESSED])
                    {
                        model.Body.Move(new PhysicalVector(0, statsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * 1f));
                    }

                    if (Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.MOVEDOWNPRESSED])
                    {
                        model.Body.Move(new PhysicalVector(0, statsManager.GetStat(EntityStats.MOVEMENT_SPEED).CurrentValue * -1f));
                    }
                }
            }
            else
            {
                if(model.ModelState == ModelStates.CLIMBING_LADDER)
                {
                    model.Body.IsFrozen = false;
                    model.ModelState = ModelStates.IDLE;
                }
            }
        }
    }
}
