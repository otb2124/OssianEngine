using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class NearestEntityFinder
    {
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
                        if (typedEntity != entFrom)
                        {
                            nearestEntity = typedEntity;
                        }
                    }
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
            return (BattleEntity)GetNearestPhysicalEntityOfClass(entFrom, typeof(BattleEntity));
        }

        public static BattleEntity GetNearestBattleEntityOfFraction(PhysicalEntity entFrom, StatsEntity.EntityFractions fraction)
        {
            if (!Enum.IsDefined(typeof(BattleEntity.EntityFractions), fraction))
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
