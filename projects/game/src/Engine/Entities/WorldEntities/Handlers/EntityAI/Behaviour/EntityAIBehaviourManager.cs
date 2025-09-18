using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Entities.BattleMovesetFactory;

namespace Entities
{
    public class EntityAIBehaviourManager
    {
        public enum BehaviourPatterns
        {
            ANIMAL_WALKING,
            ANIMAL_FLYING,
            BANDIT_DEFAULT
        };

        public enum BehaviourCases
        {
            NONE,
            STILL,
            IDLE,
            IDLE_RANDOM,
            AGGRO,
        }; 


        public EntityAICommand[] CommandPool;
        public BehaviourPatterns Pattern;
        public BehaviourCases CurrentCase;

        private int commandsExecutedSinceLastRepeat = 0;

        public bool CaseUpdated = false;

        public static readonly Dictionary<StatsEntity.EntityFractions, StatsEntity.EntityFractions[]> AutomaticAggroFractionsMap = new()
        {
            { StatsEntity.EntityFractions.NEUTRAL, new StatsEntity.EntityFractions[]{ }},
            { StatsEntity.EntityFractions.BANDIT, new StatsEntity.EntityFractions[]{ StatsEntity.EntityFractions.ANIMAL, StatsEntity.EntityFractions.PLAYER}},
            { StatsEntity.EntityFractions.ANIMAL, new StatsEntity.EntityFractions[]{ StatsEntity.EntityFractions.BANDIT, StatsEntity.EntityFractions.PLAYER}}
        };


        public EntityAIBehaviourManager(BehaviourPatterns pattern)
        {
            Pattern = pattern;
            CurrentCase = BehaviourCases.NONE;
        }

        public void UpdateCommands(Queue<EntityAICommand> currentQueue, EntityAICommand lastCompletedCommand)
        {
            if (lastCompletedCommand != null)
            {
                commandsExecutedSinceLastRepeat++;
                if (lastCompletedCommand.RepeatAfterCommandsCount > 0 &&
                    commandsExecutedSinceLastRepeat >= lastCompletedCommand.RepeatAfterCommandsCount)
                {
                    currentQueue.Enqueue(lastCompletedCommand);
                    commandsExecutedSinceLastRepeat = 0;
                }
            }

            if (currentQueue.Count == 0)
            {
                foreach (EntityAICommand command in CommandPool)
                {
                    if (command.RepeatAfterRestart)
                    {
                        command.ReInit();
                        currentQueue.Enqueue(command);
                    }
                }
                commandsExecutedSinceLastRepeat = 0;
            }
        }

        public void UpdateCurrentCase(Queue<EntityAICommand> currentQueue, AIEntity entity, BehaviourCases bCase)
        {
            if(CurrentCase == bCase) 
            {
                CaseUpdated = false;
                return; 
            }

            CurrentCase = bCase;

            switch (Pattern)
            {
                case BehaviourPatterns.ANIMAL_WALKING:

                    switch (CurrentCase)
                    {
                        case BehaviourCases.STILL:

                            CommandPool = new EntityAICommand[]
                            {
                                new EntityAICommand(entity => { entity.StandStill(); })
                            };

                            break;

                        case BehaviourCases.IDLE:

                            CommandPool = new EntityAICommand[]
                            {
                                //new EntityAICommand(entity => { entity.Move(Directions.RIGHT); },   5f),
                                //new EntityAICommand(entity => { entity.Move(Directions.LEFT); },    6f),
                                //new EntityAICommand(entity => { entity.Move(Directions.RIGHT); },   7f),
                                //new EntityAICommand(entity => { entity.Move(Directions.LEFT); },    5f),
                                new EntityAICommand(entity => { entity.MoveUntillUngrounded(); },   3f, true),
                                new EntityAICommand(entity => { entity.Jump(); },                    1.5f, true),
                                new EntityAICommand(entity => { entity.MoveUntillUngrounded(); },   3f, true),
                                new EntityAICommand(entity => { entity.StandStill(); },             3f, true),
                            };
                            
                            break;
                        case BehaviourCases.IDLE_RANDOM:

                            CommandPool = new EntityAICommand[]
                            {
                                new EntityAICommand(entity => { entity.MoveUntillUngrounded(); },   10f, true),
                                new EntityAICommand(entity => { entity.Jump(); },                    1.5f, true),
                                new EntityAICommand(entity => { entity.MoveUntillUngrounded(); },   10f, true),
                                new EntityAICommand(entity => { entity.StandStill(); },             3f, true),
                            };

                            CommandPool = Shuffle();

                            break;

                        case BehaviourCases.AGGRO:

                            CommandPool = new EntityAICommand[]
                            {
                                new EntityAICommand(entity => { entity.FollowEntityAndAttackNearestOfAggroFraction(AttackTypes.LIGHT); }, 10f, true),
                                new EntityAICommand(entity => { entity.FollowEntityAndAttackNearestOfAggroFraction(AttackTypes.LIGHT); }, 10f, true),
                                new EntityAICommand(entity => { entity.FollowEntityAndAttackNearestOfAggroFraction(AttackTypes.LIGHT); }, 10f, true),
                                new EntityAICommand(entity => { entity.StandStill(); },                                                    1f, true),
                            };

                            break;
                    }
                    break;


                case BehaviourPatterns.ANIMAL_FLYING:

                    switch (CurrentCase)
                    {
                        case BehaviourCases.STILL:

                            CommandPool = new EntityAICommand[]
                            {
                                new EntityAICommand(entity => { entity.StandStill(); })
                            };

                            break;

                        case BehaviourCases.IDLE:

                            CommandPool = new EntityAICommand[]
                            {
                                //new EntityAICommand(entity => { entity.Move(Directions.RIGHT); },   5f),
                                //new EntityAICommand(entity => { entity.Move(Directions.LEFT); },    6f),
                                //new EntityAICommand(entity => { entity.Move(Directions.RIGHT); },   7f),
                                //new EntityAICommand(entity => { entity.Move(Directions.LEFT); },    5f),
                                new EntityAICommand(entity => { entity.MoveUntillUngrounded(); },   3f, true),
                                new EntityAICommand(entity => { entity.Jump(); },                    1.5f, true),
                                new EntityAICommand(entity => { entity.MoveUntillUngrounded(); },   3f, true),
                                new EntityAICommand(entity => { entity.StandStill(); },             3f, true),
                            };

                            break;
                        case BehaviourCases.IDLE_RANDOM:

                            CommandPool = new EntityAICommand[]
                            {
                                //new EntityAICommand(entity => { entity.MoveUntillUngrounded(); },   10f, true),
                                //new EntityAICommand(entity => { entity.Jump(); },                    1.5f, true),
                                //new EntityAICommand(entity => { entity.MoveUntillUngrounded(); },   10f, true),
                                new EntityAICommand(entity => { entity.StandStill(); },             3f, true),
                            };

                            CommandPool = Shuffle();

                            break;

                        case BehaviourCases.AGGRO:

                            CommandPool = new EntityAICommand[]
                            {
                                //new EntityAICommand(entity => { entity.FollowEntityAndAttackNearestOfAggroFraction(AttackTypes.LIGHT); }, 10f, true),
                                //new EntityAICommand(entity => { entity.FollowEntityAndAttackNearestOfAggroFraction(AttackTypes.LIGHT); }, 10f, true),
                                //new EntityAICommand(entity => { entity.FollowEntityAndAttackNearestOfAggroFraction(AttackTypes.LIGHT); }, 10f, true),
                                new EntityAICommand(entity => { entity.FollowEntityNearestOfAggroFraction(); },                            1f, true),
                                new EntityAICommand(entity => { entity.FollowEntityAndAttackNearestOfAggroFraction(AttackTypes.LIGHT); },  5f, true),
                            };

                            break;
                    }
                    break;



                case BehaviourPatterns.BANDIT_DEFAULT:
                    switch (CurrentCase)
                    {
                        case BehaviourCases.STILL:

                            CommandPool = new EntityAICommand[]
                            {
                                new EntityAICommand(entity => { entity.StandStill(); })
                            };

                            break;

                        case BehaviourCases.IDLE:

                            CommandPool = new EntityAICommand[]
                            {
                                //new EntityAICommand(entity => { entity.Move(Directions.RIGHT); },   5f),
                                //new EntityAICommand(entity => { entity.Move(Directions.LEFT); },    6f),
                                //new EntityAICommand(entity => { entity.Move(Directions.RIGHT); },   7f),
                                //new EntityAICommand(entity => { entity.Move(Directions.LEFT); },    5f),
                                new EntityAICommand(entity => { entity.MoveUntillUngrounded(); },   3f, true),
                                new EntityAICommand(entity => { entity.Jump(); },                   1.5f, true),
                                new EntityAICommand(entity => { entity.MoveUntillUngrounded(); },    3f, true),
                                new EntityAICommand(entity => { entity.StandStill(); },             3f, true),
                            };

                            break;
                        case BehaviourCases.IDLE_RANDOM:

                            CommandPool = new EntityAICommand[]
                            {
                                new EntityAICommand(entity => { entity.MoveUntillUngrounded(); },   3f, true),
                                new EntityAICommand(entity => { entity.Jump(); },                    1.5f, true),
                                new EntityAICommand(entity => { entity.MoveUntillUngrounded(); },   3f, true),
                                new EntityAICommand(entity => { entity.StandStill(); },             3f, true),
                            };

                            CommandPool = Shuffle();

                            break;
                        case BehaviourCases.AGGRO:

                            CommandPool = new EntityAICommand[]
                            {
                                new EntityAICommand(entity => { entity.FollowEntityAndAttackNearestOfAggroFraction(AttackTypes.LIGHT); }, 10f, true),
                                new EntityAICommand(entity => { entity.FollowEntityAndAttackNearestOfAggroFraction(AttackTypes.LIGHT); }, 10f, true),
                                new EntityAICommand(entity => { entity.FollowEntityAndAttackNearestOfAggroFraction(AttackTypes.LIGHT); }, 10f, true),
                                new EntityAICommand(entity => { entity.StandStill(); },                                                    1f, true),
                            };

                            break;
                    }
                    break;


            }



            if(CommandPool != null)
            {
                foreach (EntityAICommand command in CommandPool)
                {
                    command.Entity = entity;
                }


                currentQueue.Clear();
                foreach (EntityAICommand command in CommandPool)
                {
                    currentQueue.Enqueue(command);
                }
            }
            

            CaseUpdated = true;
        }


        private EntityAICommand[] Shuffle()
        {
            var list = CommandPool.ToList();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = RandomHelper.RandomInteger(0, n + 1);
                var temp = list[k];
                list[k] = list[n];
                list[n] = temp;
            }
            return list.ToArray();
        }
    }
}
