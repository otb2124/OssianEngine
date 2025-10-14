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



            //JUMPING
            new ModelStateSwap(
                ModelStates.JUMPING,
                new Requirement[]
                {
                    new AndRequirement(
                        new Requirement[]
                        {
                            new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.JUMPPRESSED),

                            new OrRequirement(
                                new Requirement[]
                                {
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED),
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED)
                                },
                                true
                            ),


                            new ModelStateRequirement(ModelStates.ATTACKING_LIGHT, true),
                            new ModelStateRequirement(ModelStates.ATTACKING_HEAVY, true),
                            new ModelStateRequirement(ModelStates.FALLEN, true),

                            new CurrentEnoughStaminaForDependentStatRequirement(EntityStats.JUMP_SPEED)
                        }
                    )
                }
            ),



            //JUMPING_AND_MOVING
            new ModelStateSwap(
                ModelStates.JUMPING_AND_MOVING,
                new Requirement[]
                {
                    new AndRequirement(
                        new Requirement[]
                        {
                            new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.JUMPPRESSED),

                            new OrRequirement(
                                new Requirement[]
                                {
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED),
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED)
                                }
                            ),

                            new ModelStateRequirement(ModelStates.ATTACKING_LIGHT, true),
                            new ModelStateRequirement(ModelStates.ATTACKING_HEAVY, true),
                            new ModelStateRequirement(ModelStates.FALLEN, true),

                            new CurrentEnoughStaminaForDependentStatRequirement(EntityStats.JUMP_SPEED)
                        }
                    )
                }
            ),


            //JUMPING_DESCENDING
            new ModelStateSwap(
                ModelStates.JUMPING_DESCENDING,
                new Requirement[]
                {
                    new AndRequirement(
                        new Requirement[]
                        {
                             new AndRequirement(
                                new Requirement[]
                                {
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED, true),
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED, true)
                                }
                            ),

                            new OrRequirement(
                                new Requirement[]
                                {
                                    new ModelStateRequirement(ModelStates.JUMPING),
                                    new ModelStateRequirement(ModelStates.JUMPING_AND_MOVING),
                                    new ModelStateRequirement(ModelStates.JUMPING_DESCENDING),
                                }
                            ),

                            new ModelStateRequirement(ModelStates.ATTACKING_LIGHT, true),
                            new ModelStateRequirement(ModelStates.ATTACKING_HEAVY, true),
                            new ModelStateRequirement(ModelStates.FALLEN, true),

                            new ModelStateRequirement(ModelStates.DOUBLE_JUMPING, true),
                            new ModelStateRequirement(ModelStates.DOUBLE_JUMPING_AND_MOVING, true),

                            //new IsGroundedRequirement(true),
                            new AllowJumpDescendingRequirement(),
                        }
                    )
                }
            ),


            //JUMPING_DESCENDING_AND_MOVING
            new ModelStateSwap(
                ModelStates.JUMPING_DESCENDING_AND_MOVING,
                new Requirement[]
                {
                    new AndRequirement(
                        new Requirement[]
                        {
                             new OrRequirement(
                                new Requirement[]
                                {
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED),
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED)
                                }
                            ),

                            new OrRequirement(
                                new Requirement[]
                                {
                                    new ModelStateRequirement(ModelStates.JUMPING),
                                    new ModelStateRequirement(ModelStates.JUMPING_AND_MOVING),
                                    new ModelStateRequirement(ModelStates.JUMPING_DESCENDING),
                                }
                            ),

                            new ModelStateRequirement(ModelStates.ATTACKING_LIGHT, true),
                            new ModelStateRequirement(ModelStates.ATTACKING_HEAVY, true),
                            new ModelStateRequirement(ModelStates.FALLEN, true),

                            new ModelStateRequirement(ModelStates.DOUBLE_JUMPING, true),
                            new ModelStateRequirement(ModelStates.DOUBLE_JUMPING_AND_MOVING, true),

                            //new IsGroundedRequirement(true),
                            new AllowJumpDescendingRequirement(),
                        }
                    )
                }
            ),




            //DESCENDING
            new ModelStateSwap(
                ModelStates.DESCENDING,
                new Requirement[]
                {
                    new OrRequirement(
                        new Requirement[]
                        {
                            new IsTouchingCeilingRequirement(),
                            new AndRequirement(
                                new Requirement[]
                                {
                                    new AllowJumpDescendingRequirement(true),

                                    new IsGroundedRequirement(true),

                                    new OrRequirement(
                                        new Requirement[]
                                        {
                                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING_AND_MOVING),
                                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING),
                                            new ModelStateRequirement(ModelStates.MOVING),
                                            new ModelStateRequirement(ModelStates.SPRINTING),
                                            new ModelStateRequirement(ModelStates.ROLLING),
                                            new ModelStateRequirement(ModelStates.IDLE),
                                        }
                                    ),

                                    new ModelStateRequirement(ModelStates.DOUBLE_JUMPING, true),
                                    new ModelStateRequirement(ModelStates.DOUBLE_JUMPING_AND_MOVING, true),

                                    new ModelStateRequirement(ModelStates.ATTACKING_LIGHT, true),
                                    new ModelStateRequirement(ModelStates.ATTACKING_HEAVY, true),
                                    new ModelStateRequirement(ModelStates.FALLEN, true),
                                }
                            )
                        }
                    )
                    
                }
            ),



            //SPRING
            new ModelStateSwap(
                ModelStates.SPRINTING,
                new Requirement[]
                {
                    new AndRequirement(
                        new Requirement[]
                        {
                            new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.SPRINTPRESSED),

                             new OrRequirement(
                                new Requirement[]
                                {
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED),
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED)
                                }
                             ),


                            new ModelStateRequirement(ModelStates.JUMPING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_AND_MOVING, true),

                            new ModelStateRequirement(ModelStates.ROLLING, true),

                            new ModelStateRequirement(ModelStates.DESCENDING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING_AND_MOVING, true),

                            new ModelStateRequirement(ModelStates.ATTACKING_LIGHT, true),
                            new ModelStateRequirement(ModelStates.ATTACKING_HEAVY, true),
                            new ModelStateRequirement(ModelStates.FALLEN, true),

                            new CurrentEnoughStaminaForDependentStatRequirement(EntityStats.SPRINT_SPEED_MULTIPLIER),
                            new IsOnStaminaRegenRequirement(true),
                            new IsGroundedRequirement(),
                        }
                    )
                }
            ),



            //ROLLING
            new ModelStateSwap(
                ModelStates.ROLLING,
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
                                }
                             ),


                            new ModelStateRequirement(ModelStates.JUMPING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_AND_MOVING, true),

                            new ModelStateRequirement(ModelStates.SPRINTING, true),

                            new ModelStateRequirement(ModelStates.DESCENDING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING_AND_MOVING, true),

                            new ModelStateRequirement(ModelStates.ATTACKING_LIGHT, true),
                            new ModelStateRequirement(ModelStates.ATTACKING_HEAVY, true),
                            new ModelStateRequirement(ModelStates.FALLEN, true),

                            new CurrentEnoughStaminaForDependentStatRequirement(EntityStats.ROLL_SPEED_MULTIPLIER),
                            new IsOnStaminaRegenRequirement(true),
                            new IsGroundedRequirement(),
                        }
                    )
                }
            ),




            //MOVING
            new ModelStateSwap(
                ModelStates.MOVING,
                new Requirement[]
                {
                    new AndRequirement(
                        new Requirement[]
                        {
                             new OrRequirement(
                                new Requirement[]
                                {
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED),
                                    new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED)
                                }
                             ),

                            new ModelStateRequirement(ModelStates.JUMPING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_AND_MOVING, true),

                            new ModelStateRequirement(ModelStates.SPRINTING, true),
                            new ModelStateRequirement(ModelStates.ROLLING, true),

                            new ModelStateRequirement(ModelStates.DESCENDING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING, true),
                            new ModelStateRequirement(ModelStates.JUMPING_DESCENDING_AND_MOVING, true),

                            new ModelStateRequirement(ModelStates.ATTACKING_LIGHT, true),
                            new ModelStateRequirement(ModelStates.ATTACKING_HEAVY, true),
                            new ModelStateRequirement(ModelStates.FALLEN, true),

                            new IsGroundedRequirement(),
                            new AllowJumpDescendingRequirement(true),
                        }
                    )
                }
            ),


            //IDLE
            new ModelStateSwap(
                ModelStates.IDLE,
                new Requirement[]
                {
                    new AndRequirement(
                        new Requirement[]
                        {
                            new OrRequirement(
                                new Requirement[]
                                {
                                    new ModelStateRequirement(ModelStates.JUMPING),
                                    new ModelStateRequirement(ModelStates.JUMPING_AND_MOVING),
                                },
                                true
                             ),


                            new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED, true),
                            new CurrentInputKeyRequirement(Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED, true),

                            new IsGroundedRequirement(),
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

    }
}
