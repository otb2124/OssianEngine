using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.EntityAIBehaviourManager;

namespace Entities
{
    public static class NearestEntityFinder
    {
        public static T FindNearestEntity<T>(
            PhysicalEntity entFrom,
            Func<T, bool> predicate,
            string filterDescription,
            string methodName) where T : PhysicalEntity
        {
            EntityMap map = Entities.EntityMapManager.GetCurrentMap();
            if (map == null || map.Entities == null)
            {
                return null;
            }

            T nearestEntity = null;
            float minDistance = float.MaxValue;

            foreach (var entity in map.Entities.OfType<T>().Where(predicate))
            {
                if (entity == entFrom) continue;

                float distance = EntityAIHelper.GetEntityDistance(entFrom, entity);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEntity = entity;
                }
            }

            return nearestEntity;
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

        public static BattleEntity GetNearestBattleEntity(PhysicalEntity entFrom)
        {
            return FindNearestEntity<BattleEntity>(
                entFrom,
                entity => true,
                "BattleEntity",
                "GetNearestBattleEntity"
            );
        }

        public static BattleEntity GetNearestBattleEntityInAggroRange(BattleEntity entFrom)
        {
            var candidate = FindNearestEntity<BattleEntity>(
                entFrom,
                entity => EntityAIHelper.IsBattleEntityOfAggroFraction(entFrom, entity),
                "BattleEntity in aggro fraction",
                "GetNearestBattleEntityInAggroRange"
            );

            if (candidate == null) return null;

            float distance = EntityAIHelper.GetEntityDistance(entFrom, candidate);
            if (distance < entFrom.StatsManager.GetStat(EntityStats.AGGRO_RANGE).CurrentValue)
            {
                return candidate;
            }

            if (EntityAIHelper.GetCurrentBehaviourCase(entFrom) == BehaviourCases.AGGRO && distance > entFrom.StatsManager.GetStat(EntityStats.UNAGGRO_RANGE).CurrentValue)
            {
                return null;
            }

            return candidate;
        }

        public static BattleEntity GetNearestBattleEntityOfFraction(PhysicalEntity entFrom, StatsEntity.EntityFractions fraction)
        {
            if (!Enum.IsDefined(typeof(StatsEntity.EntityFractions), fraction))
            {
                return null;
            }

            return FindNearestEntity<BattleEntity>(
                entFrom,
                entity => entity.EntityFraction == fraction,
                fraction.ToString(),
                "GetNearestBattleEntityOfFraction"
            );
        }


    }
}
