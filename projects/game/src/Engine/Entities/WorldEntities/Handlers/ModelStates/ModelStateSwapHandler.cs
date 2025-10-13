using Utils;

namespace Entities
{
    public static class ModelStateSwapHandler
    {

        public static ModelStateSwap[] ModelStateSwappers = new[]
        {

            //ATTACKING_LIGHT
            new ModelStateSwap(
                ModelStates.ATTACKING_LIGHT,
                new Requirement[]
                {
                    new AndRequirement(
                        new Requirement[]
                        {
                            new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.ATTACKLIGHTPRESSED),

                            new OrRequirement(
                                new Requirement[]
                                {
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED),
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED)
                                },
                                true
                            ),

                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING_AND_MOVING, true),
                            new ModelStateRequirement(ModelStates.JUMPING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_AND_MOVING, true),
                            new ModelStateRequirement(ModelStates.DESCENDING, true),
                            new ModelStateRequirement(ModelStates.HANGING_ON_LEDGE, true),

                            new ModelStateRequirement(ModelStates.ATTACKING_HEAVY, true),
                            new ModelStateRequirement(ModelStates.BLOCKING, true),

                            new CurrentEnoughBattleManaRequirement(),
                            new CurrentEnoughBattleStaminaRequirement(),
                        }
                    )
                }
            ),


            //ATTACKING_HEAVY
            new ModelStateSwap(
                ModelStates.ATTACKING_HEAVY,
                new Requirement[]
                {
                    new AndRequirement(
                        new Requirement[]
                        {
                            new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.ATTACKHEAVYPRESSED),

                            new OrRequirement(
                                new Requirement[]
                                {
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED),
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED)
                                },
                                true
                            ),

                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING_AND_MOVING, true),
                            new ModelStateRequirement(ModelStates.JUMPING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_AND_MOVING, true),
                            new ModelStateRequirement(ModelStates.DESCENDING, true),
                            new ModelStateRequirement(ModelStates.HANGING_ON_LEDGE, true),

                            new ModelStateRequirement(ModelStates.ATTACKING_LIGHT, true),
                            new ModelStateRequirement(ModelStates.BLOCKING, true),

                            new CurrentEnoughBattleManaRequirement(),
                            new CurrentEnoughBattleStaminaRequirement(),
                        }
                    )
                }
            ),


            //BLOCK
            new ModelStateSwap(
                ModelStates.BLOCKING,
                new Requirement[]
                {
                    new AndRequirement(
                        new Requirement[]
                        {
                            new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.BLOCKPRESSED),

                            new OrRequirement(
                                new Requirement[]
                                {
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED),
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED)
                                },
                                true
                            ),

                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING_AND_MOVING, true),
                            new ModelStateRequirement(ModelStates.JUMPING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_AND_MOVING, true),
                            new ModelStateRequirement(ModelStates.DESCENDING, true),
                            new ModelStateRequirement(ModelStates.HANGING_ON_LEDGE, true),

                            new ModelStateRequirement(ModelStates.ATTACKING_LIGHT, true),
                            new ModelStateRequirement(ModelStates.ATTACKING_HEAVY, true),

                            new CurrentEnoughStaminaForDependentStatRequirement(EntityStats.ROLL_SPEED_MULTIPLIER)
                        }
                    )
                }
            ),



            //BLOCK RESET
            new ModelStateSwap(
                ModelStates.IDLE,
                new Requirement[]
                {
                    new AndRequirement(
                        new Requirement[]
                        {
                            new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.BLOCKPRESSED, true),

                            new ModelStateRequirement(ModelStates.BLOCKING),

                            new CurrentWeaponOutRequirement(),
                        }
                    )
                }
            ),
            new ModelStateSwap(
                ModelStates.WEAPON_OUT_IDLE,
                new Requirement[]
                {
                    new AndRequirement(
                        new Requirement[]
                        {
                            new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.BLOCKPRESSED, true),

                            new ModelStateRequirement(ModelStates.BLOCKING),

                            new CurrentWeaponOutRequirement(true),
                        }
                    )
                }
            ),
        };

        public static void Update()
        {

            foreach (ModelStateSwap modelStateSwap in ModelStateSwappers)
            {
                modelStateSwap.Update();
            }

            ToggleWeapon();

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


        public static void TurnRight()
        {
            Player player = Entities.Player;

            if (Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
            {
                if ((Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
                    && player.Model.ModelState != ModelStates.ATTACKING_LIGHT &&
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
                if ((Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED]) &&
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
                if ((Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED]) &&
                    player.Model.ModelState != ModelStates.ATTACKING_LIGHT &&
                    player.Model.ModelState != ModelStates.ATTACKING_HEAVY &&
                    player.Model.ModelState != ModelStates.FALLEN)
                {

                    if (player.StatsManager.CheckEnoughStaminaForStat(EntityStats.JUMP_SPEED)) //&& (player.StatsManager.IsGrounded || player.Model.ModelState == ModelStates.HANGING_ON_LEDGE))
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

                if ((Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED]) &&
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

                if ((Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED]) &&
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
                if ((Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED]) &&
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
                            if (player.StatsManager.CheckEnoughStaminaForStat(EntityStats.SPRINT_SPEED_MULTIPLIER) &&
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
                if ((Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED]) &&
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
                            if (player.StatsManager.CheckEnoughStaminaForStat(EntityStats.ROLL_SPEED_MULTIPLIER) &&
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
            if ((Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED]) &&
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

            if ((Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED]) &&
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
                && !(Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED]))
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
                && !(Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED]))
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
                    && !(Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED] || Inputs.Inputs.KeyHandler.KeyStateMap[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED]))
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
