using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using MathHelper = Utils.MathHelper;

namespace Entities
{
    public class NPCEntity : AIEntity
    {

        public NPCInteractionManager NPCInteractionManager;
       
        public NPCEntity(Models modelPreset, Vector2 pos, float rot = 0) : base(modelPreset, pos, rot)
        {
            //SetInteractionType();
        }

        public NPCEntity() : base()
        {
            //SetInteractionType();
        }

        
        public virtual void SetInteractionType()
        {
            //NPCInteractionManager = new NPCInteractionManager();
        }

        public override void Update()
        {
            if(NPCInteractionManager != null)
            {
                NPCInteractionManager.Update(Model);
            }

            base.Update();
        }

        public override void DrawCollider()
        {
            if (NPCInteractionManager != null)
            {
                NPCInteractionManager.InteractionField.Draw();
            }

            base.DrawCollider();
        }

    }
}
