using Utils;

namespace Entities
{
    public static class ModelStateSwapHandler
    {

        public static ModelStateSwap[] ModelStateSwappers = new[]
        {
            new ModelStateSwap(ModelStates.ATTACKING_LIGHT, new Requirement[] {  })
        };

        public static void Update()
        {
            ToggleWeapon();
            AttackLight();
            AttackHeavy();
            Block();

            TurnRight();
            TurnLeft();

            JumpingAndMoving();
            JumpingDescendingAndMoving();
            DescendingAndMoving();
            SprintingAndMoving();
            RollingAndMoving();
            Moving();
            IdleMoving();

            JumpingDescending();
            Descending();
            Jumping();
        }


        public static void ToggleWeapon()
        {
            Player player = Entities.Player;
            // WEAPON TOGGLE
            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.TOGGLEWEAPONPRESSED])
            {
                player.EquipmentManager.ToggleWeaponInOut(player.BattleBodyManager);
            }
        }

        public static void AttackLight()
        {
            Player player = Entities.Player;
            // ATTACK
            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.ATTACKLIGHTPRESSED])
            {
                if (!UI.UI.PreventButtonPressedOverlap
                    && !KeyHandlerUtil.isPlayerMoving()
                    && player.Model.ModelState != ModelStates.JUMPING_DESCENDING
                    && player.Model.ModelState != ModelStates.JUMPING_DESCENDING_AND_MOVING
                    && player.Model.ModelState != ModelStates.JUMPING
                    && player.Model.ModelState != ModelStates.JUMPING_AND_MOVING
                    && player.Model.ModelState != ModelStates.DESCENDING
                    && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE

                    && player.Model.ModelState != ModelStates.ATTACKING_HEAVY
                    && player.Model.ModelState != ModelStates.BLOCKING)
                {
                    if (player.StatsManager.CheckEnoughFinalBattleMana(player) &&
                    player.StatsManager.CheckEnoughFinalBattleStamina(player))
                    {
                        player.Model.ModelState = ModelStates.ATTACKING_LIGHT;
                    }
                }
            }
        }


        public static void AttackHeavy()
        {
            Player player = Entities.Player;

            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.ATTACKHEAVYPRESSED] && !UI.UI.PreventButtonPressedOverlap
                && !KeyHandlerUtil.isPlayerMoving()
                && player.Model.ModelState != ModelStates.JUMPING_DESCENDING
                && player.Model.ModelState != ModelStates.JUMPING_DESCENDING_AND_MOVING
                && player.Model.ModelState != ModelStates.JUMPING
                && player.Model.ModelState != ModelStates.JUMPING_AND_MOVING
                && player.Model.ModelState != ModelStates.DESCENDING
                && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE

                && player.Model.ModelState != ModelStates.ATTACKING_LIGHT
                && player.Model.ModelState != ModelStates.BLOCKING)
            {
                if (player.StatsManager.CheckEnoughFinalBattleMana(player) &&
                    player.StatsManager.CheckEnoughFinalBattleStamina(player))
                {
                    player.Model.ModelState = ModelStates.ATTACKING_HEAVY;
                }
            }
        }

        public static void Block()
        {
            Player player = Entities.Player;
            //BLOCK
            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.BLOCKPRESSED])
            {
                if (!KeyHandlerUtil.isPlayerMoving()
                    && player.Model.ModelState != ModelStates.JUMPING_DESCENDING
                    && player.Model.ModelState != ModelStates.JUMPING_DESCENDING_AND_MOVING
                    && player.Model.ModelState != ModelStates.JUMPING
                    && player.Model.ModelState != ModelStates.JUMPING_AND_MOVING
                    && player.Model.ModelState != ModelStates.DESCENDING
                    && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE

                    && player.Model.ModelState != ModelStates.ATTACKING_HEAVY
                    && player.Model.ModelState != ModelStates.ATTACKING_LIGHT)
                {
                    if (player.StatsManager.CheckEnoughStaminaForRoll())
                    {
                        player.Model.ModelState = ModelStates.BLOCKING;
                    }
                }
            }
            //BLOCK RESET
            else if (!Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.BLOCKPRESSED])
            {
                if (player.Model.ModelState == ModelStates.BLOCKING)
                {
                    player.Model.ModelState = player.EquipmentManager.WeaponInOutToggler.IsWeaponOut ? ModelStates.WEAPON_OUT_IDLE : ModelStates.IDLE;
                }
            }

        }


        public static void TurnRight()
        {
            Player player = Entities.Player;

            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
            {
                if (KeyHandlerUtil.isPlayerMoving() &&
                    player.Model.ModelState != ModelStates.ATTACKING_LIGHT &&
                    player.Model.ModelState != ModelStates.ATTACKING_HEAVY &&
                    player.Model.ModelState != ModelStates.FALLEN)
                {
                    player.Model.Direction = Directions.RIGHT;
                }
            }
        }

        public static void TurnLeft()
        {
            Player player = Entities.Player;

            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
            {
                if (KeyHandlerUtil.isPlayerMoving() &&
                    player.Model.ModelState != ModelStates.ATTACKING_LIGHT &&
                    player.Model.ModelState != ModelStates.ATTACKING_HEAVY &&
                    player.Model.ModelState != ModelStates.FALLEN)
                {
                    player.Model.Direction = Directions.LEFT;
                }
            }
        }

        public static void JumpingAndMoving()
        {
            Player player = Entities.Player;
            // JUMP
            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.JUMPPRESSED])
            {
                if (KeyHandlerUtil.isPlayerMoving() &&
                    player.Model.ModelState != ModelStates.ATTACKING_LIGHT &&
                    player.Model.ModelState != ModelStates.ATTACKING_HEAVY &&
                    player.Model.ModelState != ModelStates.FALLEN)
                {

                    if (player.StatsManager.CheckEnoughStaminaForJump()) //&& (player.StatsManager.IsGrounded || player.Model.ModelState == ModelStates.HANGING_ON_LEDGE))
                    {
                        player.Model.ModelState = ModelStates.JUMPING_AND_MOVING;

                        if (player.StatsManager.AllowDoubleJump)
                        {
                            player.Model.ModelState = ModelStates.DOUBLE_JUMPING_AND_MOVING;
                        }
                    }
                }
            }
        }


        public static void JumpingDescendingAndMoving()
        {
            Player player = Entities.Player;

            // JUMPING_DESCENDING
            if ((player.Model.ModelState == ModelStates.JUMPING
                || player.Model.ModelState == ModelStates.JUMPING_AND_MOVING
                || player.Model.ModelState == ModelStates.JUMPING_DESCENDING)

                 && player.Model.ModelState != ModelStates.DOUBLE_JUMPING
                 && player.Model.ModelState != ModelStates.DOUBLE_JUMPING_AND_MOVING
                 && !player.StatsManager.IsGrounded
                 && player.StatsManager.AllowJumpDescending)
            {

                if (KeyHandlerUtil.isPlayerMoving() &&
                    player.Model.ModelState != ModelStates.ATTACKING_LIGHT &&
                    player.Model.ModelState != ModelStates.ATTACKING_HEAVY &&
                    player.Model.ModelState != ModelStates.FALLEN)
                {
                    player.Model.ModelState = ModelStates.JUMPING_DESCENDING_AND_MOVING;
                }
            }
        }


        public static void DescendingAndMoving()
        {
            Player player = Entities.Player;

            //overall descending
            if (player.StatsManager.IsTouchingCeiling

                || (!player.StatsManager.IsGrounded
                && !player.StatsManager.AllowJumpDescending
                && player.Model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING)

                && player.Model.ModelState != ModelStates.DOUBLE_JUMPING
                && player.Model.ModelState != ModelStates.DOUBLE_JUMPING_AND_MOVING)
            {

                if (KeyHandlerUtil.isPlayerMoving() &&
                    player.Model.ModelState != ModelStates.ATTACKING_LIGHT &&
                    player.Model.ModelState != ModelStates.ATTACKING_HEAVY &&
                    player.Model.ModelState != ModelStates.FALLEN)
                {
                    player.Model.ModelState = ModelStates.DESCENDING;
                }
            }
        }

        public static void SprintingAndMoving()
        {
            Player player = Entities.Player;


            // SPRINT
            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.SPRINTPRESSED])
            {
                if (KeyHandlerUtil.isPlayerMoving() &&
                    player.Model.ModelState != ModelStates.ATTACKING_LIGHT &&
                    player.Model.ModelState != ModelStates.ATTACKING_HEAVY &&
                    player.Model.ModelState != ModelStates.FALLEN)
                {

                    if (player.Model.ModelState != ModelStates.JUMPING &&
                        player.Model.ModelState != ModelStates.JUMPING_AND_MOVING &&
                        player.Model.ModelState != ModelStates.JUMPING_DESCENDING &&
                        player.Model.ModelState != ModelStates.JUMPING_DESCENDING_AND_MOVING)
                    {

                        if (player.Model.ModelState != ModelStates.DESCENDING
                            && player.Model.ModelState != ModelStates.ROLLING
                            && player.StatsManager.IsGrounded)
                        {
                            if (player.StatsManager.CheckEnoughStaminaForsSprint() &&
                                !player.StatsManager.OnStaminaRegen)
                            {
                                player.Model.ModelState = ModelStates.SPRINTING;
                            }
                        }
                    }
                }
            }
        }


        public static void RollingAndMoving()
        {

            Player player = Entities.Player;

            //ROLL
            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.BLOCKPRESSED])
            {
                if (KeyHandlerUtil.isPlayerMoving() &&
                    player.Model.ModelState != ModelStates.ATTACKING_LIGHT &&
                    player.Model.ModelState != ModelStates.ATTACKING_HEAVY &&
                    player.Model.ModelState != ModelStates.FALLEN)
                {

                    if (player.Model.ModelState != ModelStates.JUMPING &&
                        player.Model.ModelState != ModelStates.JUMPING_AND_MOVING &&
                        player.Model.ModelState != ModelStates.JUMPING_DESCENDING &&
                        player.Model.ModelState != ModelStates.JUMPING_DESCENDING_AND_MOVING)
                    {

                        if (player.Model.ModelState != ModelStates.DESCENDING
                            && player.Model.ModelState != ModelStates.SPRINTING)
                        {
                            if (player.StatsManager.CheckEnoughStaminaForRoll() &&
                                !player.StatsManager.OnStaminaRegen)
                            {
                                player.Model.ModelState = ModelStates.ROLLING;
                            }
                        }
                    }
                }

            }
        }


        public static void Moving()
        {
            Player player = Entities.Player;

            //MOVE
            if (KeyHandlerUtil.isPlayerMoving() &&
                player.Model.ModelState != ModelStates.ATTACKING_LIGHT &&
                player.Model.ModelState != ModelStates.ATTACKING_HEAVY &&
                player.Model.ModelState != ModelStates.FALLEN)
            {

                if (player.Model.ModelState != ModelStates.JUMPING &&
                    player.Model.ModelState != ModelStates.JUMPING_AND_MOVING &&
                    player.Model.ModelState != ModelStates.JUMPING_DESCENDING &&
                    player.Model.ModelState != ModelStates.JUMPING_DESCENDING_AND_MOVING)
                {
                    if (player.Model.ModelState != ModelStates.SPRINTING
                        && player.Model.ModelState != ModelStates.ROLLING)
                    {
                        if (player.StatsManager.IsGrounded && !player.StatsManager.AllowJumpDescending && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
                        {
                            player.Model.ModelState = player.EquipmentManager.WeaponInOutToggler.IsWeaponOut ? ModelStates.WEAPON_OUT_MOVING : ModelStates.MOVING;
                        }
                        else if (!player.StatsManager.IsGrounded && !player.StatsManager.AllowJumpDescending && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
                        {
                            player.Model.ModelState = ModelStates.DESCENDING;
                        }
                    }
                }
            }
        }

        public static void IdleMoving()
        {
            Player player = Entities.Player;

            if (KeyHandlerUtil.isPlayerMoving() &&
                player.Model.ModelState != ModelStates.ATTACKING_LIGHT &&
                player.Model.ModelState != ModelStates.ATTACKING_HEAVY &&
                player.Model.ModelState != ModelStates.FALLEN)
            {
                if (player.StatsManager.IsGrounded && !player.StatsManager.AllowJumpDescending && (player.Model.ModelState == ModelStates.JUMPING_DESCENDING || player.Model.ModelState == ModelStates.JUMPING_DESCENDING_AND_MOVING))
                {
                    player.Model.ModelState = player.EquipmentManager.WeaponInOutToggler.IsWeaponOut ? ModelStates.WEAPON_OUT_IDLE : ModelStates.IDLE;
                }
            }
        }


        public static void JumpingDescending()
        {
            Player player = Entities.Player;

            if (player.Model.ModelState != ModelStates.ATTACKING_LIGHT
                && player.Model.ModelState != ModelStates.ATTACKING_HEAVY
                && player.Model.ModelState != ModelStates.BLOCKING
                && !KeyHandlerUtil.isPlayerMoving())
            {
                if ((player.Model.ModelState == ModelStates.JUMPING ||
                     player.Model.ModelState == ModelStates.JUMPING_AND_MOVING) &&
                    !player.StatsManager.IsGrounded &&
                    player.StatsManager.AllowJumpDescending)
                {
                    player.Model.ModelState = ModelStates.JUMPING_DESCENDING;
                }
            }
        }


        public static void Descending()
        {
            Player player = Entities.Player;

            if (player.Model.ModelState != ModelStates.ATTACKING_LIGHT
                && player.Model.ModelState != ModelStates.ATTACKING_HEAVY
                && player.Model.ModelState != ModelStates.BLOCKING
                && !KeyHandlerUtil.isPlayerMoving())
            {
                if (player.StatsManager.IsGrounded && !player.StatsManager.AllowJumpDescending && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE)
                {
                    player.Model.ModelState = player.EquipmentManager.WeaponInOutToggler.IsWeaponOut ? ModelStates.WEAPON_OUT_IDLE : ModelStates.IDLE;
                }
                else if (player.StatsManager.IsTouchingCeiling || (!player.StatsManager.IsGrounded && !player.StatsManager.AllowJumpDescending && player.Model.ModelState != ModelStates.JUMPING_AND_MOVING && player.Model.ModelState != ModelStates.JUMPING && player.Model.ModelState != ModelStates.HANGING_ON_LEDGE))
                {
                    player.Model.ModelState = ModelStates.DESCENDING;
                }
            }
                
        }


        public static void Jumping()
        {
            Player player = Entities.Player;
            // JUMP
            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.JUMPPRESSED])
            {
                if (player.Model.ModelState != ModelStates.ATTACKING_LIGHT
                    && player.Model.ModelState != ModelStates.ATTACKING_HEAVY
                    && player.Model.ModelState != ModelStates.BLOCKING
                    && !KeyHandlerUtil.isPlayerMoving())
                {
                    if (player.StatsManager.GetStat(EntityStats.STAMINA).CurrentValue - player.StatsManager.GetStat(EntityStats.JUMP_SPEED).StaminaDependencySec > 0)
                    {
                        player.Model.ModelState = ModelStates.JUMPING;

                        if (player.StatsManager.AllowDoubleJump)
                        {
                            player.Model.ModelState = ModelStates.DOUBLE_JUMPING;
                        }
                    }
                }

                    
            }
        }
    }
}
