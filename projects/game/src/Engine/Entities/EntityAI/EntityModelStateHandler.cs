using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities {
    public static class EntityModelStateHandler
    {

        public static void Update(StatsEntity Entity)
        {
            ModelStates state = Entity.Model.modelState;
            int directionXFactor = Entity.Model.direction == Directions.RIGHT ? 1 : -1;

            if (state == ModelStates.IDLE || state == ModelStates.WEAPON_OUT_IDLE)
            {
                Entity.Stats.staminaPerAttackHitSpent = false;
            }

            if (state == ModelStates.MOVING)
            {
                Entity.Model.body.Move(new FlatVector(Entity.Stats.speed * directionXFactor, 0));
            }

            if (state == ModelStates.WEAPON_OUT_MOVING)
            {
                Entity.Model.body.Move(new FlatVector(Entity.Stats.speed * directionXFactor, 0));
            }

            if (state == ModelStates.JUMPING)
            {
                Entity.Model.body.Jump(Entity.Stats.jumpSpeed);
                Entity.Stats.stamina -= Entity.Stats.staminaJumpCostSec / 60;
            }

            if (state == ModelStates.JUMPING_AND_MOVING)
            {
                Entity.Model.body.Jump(Entity.Stats.jumpSpeed);
                Entity.Stats.stamina -= Entity.Stats.staminaJumpCostSec / 60;
                Entity.Model.body.Move(new FlatVector(Entity.Stats.speed * directionXFactor, 0));
            }

            if (state == ModelStates.SPRINTING)
            {
                Entity.Model.body.Move(new FlatVector(Entity.Stats.speed * Entity.Stats.sprintMultiplier * directionXFactor, 0));
                Entity.Stats.OnUsingStamina = true;
                Entity.Stats.stamina -= Entity.Stats.staminaSprintCostSec / 60;
            }

            if (state == ModelStates.BLOCKING)
            {
                //Stats.stamina -= Stats.staminaRollCostSec / 60;
            }

            if (state == ModelStates.ROLLING)
            {
                Entity.Stats.stamina -= Entity.Stats.staminaRollCostSec / 60;
                Entity.Model.body.Move(new FlatVector(Entity.Stats.speed * Entity.Stats.rollMultiplier * directionXFactor, 0));
            }


            if(Entity is EquipmentEntity eqEnt)
            {
                if (state == ModelStates.ATTACKING_LIGHT)
                {
                    if (!eqEnt.Stats.staminaPerAttackHitSpent)
                    {
                        eqEnt.Stats.stamina -= eqEnt.Stats.staminaAttackHitCost;
                        eqEnt.Stats.staminaPerAttackHitSpent = true;
                    }

                }

                if (state == ModelStates.ATTACKING_HEAVY)
                {
                    if (!eqEnt.Stats.staminaPerAttackHitSpent)
                    {
                        eqEnt.Stats.stamina -= eqEnt.Stats.staminaAttackHitCost;
                        eqEnt.Stats.staminaPerAttackHitSpent = true;
                    }

                }
            }
            
        }


        public static void UpdatePlayerModelState(Player player)
        {
            //WEAPON TOGGLE
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.TOGGLEWEAPONPRESSED])
            {
                player.EquipmentManager.IsWeaponOut = !player.EquipmentManager.IsWeaponOut;
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.ATTACKLIGHTPRESSED])
            {
                if ((player.Stats.stamina - player.Stats.staminaAttackHitCost) > 0)
                {
                    player.Model.modelState = ModelStates.ATTACKING_LIGHT;
                }
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.ATTACKHEAVYPRESSED])
            {
                if ((player.Stats.stamina - player.Stats.staminaAttackHitCost) > 0)
                {
                    player.Model.modelState = ModelStates.ATTACKING_HEAVY;
                }
            }

            //ANY OF BELOWMENTIONED KEYS PRESSED
            if (KeyHandlerUtil.isPlayerMoving() && player.Model.modelState != ModelStates.ATTACKING_LIGHT && player.Model.modelState != ModelStates.ATTACKING_HEAVY)
            {
                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
                {
                    player.Model.direction = Directions.RIGHT;
                    player.Model.modelState = ModelStates.MOVING;

                    if (player.EquipmentManager.IsWeaponOut)
                    {
                        player.Model.modelState = ModelStates.WEAPON_OUT_MOVING;
                    }
                }
                else if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
                {
                    player.Model.direction = Directions.LEFT;
                    player.Model.modelState = ModelStates.MOVING;

                    if (player.EquipmentManager.IsWeaponOut)
                    {
                        player.Model.modelState = ModelStates.WEAPON_OUT_MOVING;
                    }
                }

                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.SPRINTPRESSED])
                {
                    if ((player.Stats.stamina - player.Stats.staminaSprintCostSec / 60) > 0 && !player.Stats.OnStaminaRegen)
                    {
                        player.Model.modelState = ModelStates.SPRINTING;
                    }
                }

                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED])
                {
                    if ((player.Stats.stamina - (player.Stats.staminaJumpCostSec / 60)) > 0)
                    {
                        player.Model.modelState = ModelStates.JUMPING;
                    }
                }

                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED] && (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED]))
                {
                    if ((player.Stats.stamina - (player.Stats.staminaJumpCostSec / 60)) > 0)
                    {
                        player.Model.modelState = ModelStates.JUMPING_AND_MOVING;
                    }
                }

                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.BLOCKPRESSED])
                {
                    if (player.Stats.stamina > 0)
                    {
                        player.Model.modelState = ModelStates.BLOCKING;
                    }
                }

                if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.BLOCKPRESSED] && (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED]))
                {
                    if ((player.Stats.stamina - (player.Stats.staminaRollCostSec / 60)) > 0)
                    {
                        player.Model.modelState = ModelStates.ROLLING;
                    }
                }
            }
            //ANY OF BELOWMENTIONED KEYS NOT PRESSED
            else
            {
                if (player.Model.modelState != ModelStates.ATTACKING_LIGHT && player.Model.modelState != ModelStates.ATTACKING_HEAVY)
                {
                    //FORCE IDLE OR WEAPON OUT IDLE IF NOT ATTACKING
                    player.Model.modelState = ModelStates.IDLE;

                    if (player.EquipmentManager.IsWeaponOut)
                    {
                        player.Model.modelState = ModelStates.WEAPON_OUT_IDLE;
                    }
                }
            }
        }
    }
}
