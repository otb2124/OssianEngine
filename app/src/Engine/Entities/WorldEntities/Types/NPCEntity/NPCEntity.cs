using Microsoft.Xna.Framework;
using Physics;
using Resources;
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

        public InteractionManager InteractionManager;
       
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
            //InteractionManager = new InteractionManager();
        }

        public override void Update()
        {
            if(InteractionManager != null)
            {
                InteractionManager.Update(Model);
            }

            base.Update();
        }

        public override void DrawCollider()
        {
            if (InteractionManager != null)
            {
                InteractionManager.InteractionField.Draw();
            }

            base.DrawCollider();
        }

    }
}
