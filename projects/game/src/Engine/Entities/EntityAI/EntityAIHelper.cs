using Microsoft.Xna.Framework;
using Physics;
using System;
using Utils;
using static Entities.EntityAIBehaviourManager;

namespace Entities
{
    public static class EntityAIHelper
    {


        public static float GetEntityDistance(PhysicalEntity entityFrom, PhysicalEntity entityTo)
        {
            return GetEntityDirection(entityFrom, entityTo).Length();
        }

        public static Vector2 GetEntityDirection(PhysicalEntity entityFrom, PhysicalEntity entityTo)
        {
            if (entityFrom == null || entityTo == null)
                return Vector2.Zero;

            Vector2 EntityPos1 = FlatConverter.ToVector2(entityFrom.Model.Body.Position);
            Vector2 EntityPos2 = FlatConverter.ToVector2(entityTo.Model.Body.Position);
            return EntityPos1 - EntityPos2;
        }


        public static bool IsBattleEntityOfAggroFraction(BattleEntity entFrom, BattleEntity entTo)
        {
            StatsEntity.EntityFractions[] aggroFractions = EntityAIBehaviourManager.AutomaticAggroFractionsMap[entFrom.EntityFraction];

            if (entTo != null)
            {
                for (int i = 0; i < aggroFractions.Length; i++)
                {
                    if (entTo.EntityFraction == aggroFractions[i])
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public static BehaviourCases GetBehaviourCase(BattleEntity ent)
        {
            if (ent == null) throw new ArgumentNullException(nameof(ent));
            if (ent.Stats == null) throw new ArgumentException("Entity has null Stats.");

            if (ent.Stats.DistanceToAggro != -1f)
            {
                BattleEntity entTo = NearestEntityFinder.GetNearestBattleEntityInAggroRange(ent);

                if (entTo == null)
                {
                    if (GetCurrentBehaviourCase(ent) == BehaviourCases.AGGRO)
                    {
                        var anyAggroTarget = NearestEntityFinder.FindNearestEntity<BattleEntity>(
                            ent,
                            e => e != ent && IsBattleEntityOfAggroFraction(ent, e),
                            "Aggressive BattleEntity",
                            "GetBehaviourCase"
                        );
                        if (anyAggroTarget == null || GetEntityDistance(ent, anyAggroTarget) > ent.Stats.DistanceToUnaggro)
                        {
                            return BehaviourCases.IDLE_RANDOM;
                        }
                    }
                    else
                    {
                        return BehaviourCases.IDLE_RANDOM;
                    }
                }
                else
                {
                    float distance = GetEntityDistance(ent, entTo);

                    if (distance < ent.Stats.DistanceToAggro)
                    {
                        return BehaviourCases.AGGRO;
                    }

                    if (GetCurrentBehaviourCase(ent) == BehaviourCases.AGGRO && distance <= ent.Stats.DistanceToUnaggro)
                    {
                        return BehaviourCases.AGGRO;
                    }
                }
            }

            return GetCurrentBehaviourCase(ent);
        }

        public static BehaviourCases GetCurrentBehaviourCase(StatsEntity ent)
        {
            if (ent is HumanoidMob hMob)
            {
                return hMob.AISet.BehaviourManager.CurrentCase;
            }
            else if (ent is AnimalMob aMob)
            {
                return aMob.AISet.BehaviourManager.CurrentCase;
            }
            return BehaviourCases.IDLE_RANDOM;
        }

        public static bool HasGroundForward(StatsEntity ent)
        {
            RotatedRectangle rectToCheck = CollisionHelper.CreateGroundingRectangle(ent.Model.Body);
            int distanceToStopBeforeEdge = (int)ent.Model.Body.Width * 2;

            if (ent.Model.Direction == Directions.RIGHT)
            {
                rectToCheck.Position.X += distanceToStopBeforeEdge;
            }
            else
            {
                rectToCheck.Position.X -= distanceToStopBeforeEdge;
            }

            return CollisionHelper.GetAnyBodyAtRectangleForOtherBody(ent.Model.Body, rectToCheck) != null;
        }
    }
}
