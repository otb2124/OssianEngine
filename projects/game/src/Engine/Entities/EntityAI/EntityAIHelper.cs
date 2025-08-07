using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.EntityAIBehaviourManager;
using static Entities.WeaponComboHitSetFactory;

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

        public static PhysicalEntity GetNearestPhysicalEntityOfClass(PhysicalEntity entFrom, Type type)
        {
            if (type == null || !type.IsAssignableTo(typeof(PhysicalEntity)))
            {
                return null;
            }

            return FindNearestEntity<PhysicalEntity>(
                entFrom,
                entity => type.IsAssignableFrom(entity.GetType()),
                type.Name,
                "GetNearestPhysicalEntityOfClass"
            );
        }

        public static StatsEntity GetNearestStatsEntity(PhysicalEntity entFrom)
        {
            return (StatsEntity)GetNearestPhysicalEntityOfClass(entFrom, typeof(StatsEntity));
        }

        public static StatsEntity GetNearestStatsEntityOfFraction(PhysicalEntity entFrom, StatsEntity.EntityFractions fraction)
        {
            if (!Enum.IsDefined(typeof(StatsEntity.EntityFractions), fraction))
            {
                return null;
            }

            return FindNearestEntity<StatsEntity>(
                entFrom,
                entity => entity.EntityFraction == fraction,
                fraction.ToString(),
                "GetNearestStatsEntityOfFraction"
            );
        }

        private static T FindNearestEntity<T>(
            PhysicalEntity entFrom,
            Func<T, bool> predicate,
            string filterDescription,
            string methodName) where T : PhysicalEntity
        {
            EntityMap map = Entities.entityMapManager.GetCurrentMap();
            if (map == null || map.Entities == null)
            {
                return null;
            }

            T nearestEntity = null;
            float minDistance = float.MaxValue;

            foreach (var entity in map.Entities)
            {
                if (entity is T typedEntity && predicate(typedEntity))
                {
                    float distance = FlatConverter.ToVector2(typedEntity.Model.Body.Position - entFrom.Model.Body.Position).Length();
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        if(typedEntity != entFrom)
                        {
                            nearestEntity = typedEntity;
                        }
                    }
                }
            }

            return nearestEntity;
        }


        public static bool IsStatsEntityOfAggroFraction(StatsEntity entFrom, StatsEntity entTo)
        {
            StatsEntity.EntityFractions[] aggroFractions = EntityAIBehaviourManager.automaticAggroFractionsMap[entFrom.EntityFraction];

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


        public static BehaviourCases GetBehaviourCase(StatsEntity ent)
        {
            if (ent.Stats.DistanceToAggro != -1f)
            {
                StatsEntity entTo = GetNearestStatsEntity(ent);
                if (entTo == null)
                {
                    return GetCurrentBehaviourCase(ent);
                }

                float distance = GetEntityDistance(ent, entTo);
                if (distance == float.MaxValue)
                {
                    return GetCurrentBehaviourCase(ent);
                }

                if (IsStatsEntityOfAggroFraction(ent, entTo))
                {
                    if (distance < ent.Stats.DistanceToAggro)
                    {
                        return BehaviourCases.AGGRO;
                    }

                    if (GetCurrentBehaviourCase(ent) == BehaviourCases.AGGRO && ent.Stats.DistanceToUnaggro != -1f && distance > ent.Stats.DistanceToUnaggro)
                    {
                        return BehaviourCases.IDLE_RANDOM;
                    }
                }
            }

            return GetCurrentBehaviourCase(ent);
        }

        private static BehaviourCases GetCurrentBehaviourCase(StatsEntity ent)
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
    }
}
