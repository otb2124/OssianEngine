using Entities;
using Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class EntityEmissionLightSource : LightSource
    {
        public int EntityId;

        public EntityEmissionLightSource(int entityId, LightSourceData data) : base(Vector2.Zero, data)
        {
            EntityId = entityId;
        }

        public override void Update()
        {
            WorldEntity ent = Entities.Entities.entityManager.GetEntityById(EntityId);

            if(ent is PhysicalEntity phent)
            {
                Position = phent.Model.Body.Position.ToVector2();
            }
            
            base.Update();
        }
    }
}
