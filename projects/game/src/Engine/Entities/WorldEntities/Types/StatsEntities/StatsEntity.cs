using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Entities
{
    public class StatsEntity : PhysicalEntity
    {

        public StatsManager StatsManager;
        public Inventory Inventory;
        public DropInventory DropInventory;

        public bool UpdatesModelStates = true;

        public ParticleSet.ParticleSets BloodDropParticle;

        public EntityControlHandler EntityControlHandler;

        public enum EntityFractions
        {
            NEUTRAL,
            PLAYER,
            ANIMAL,
            BANDIT,
        };

        public EntityFractions EntityFraction;

        public StatsEntity(Models modelPreset, Vector2 pos, float rotation = 0f) : base(modelPreset, pos, rotation)
        {
            //SetStats();
            //UpdateSlots();
            //SetDropInventory();
        }

        public StatsEntity(StaticSprites sprite, PhysicalBodies body, Vector2 pos, float rotation = 0f) : base(sprite, body, pos, rotation)
        {
            //SetStats();
            //UpdateSlots();
            //SetDropInventory();
        }

        public StatsEntity() : base()
        {
            //SetStats();
            //UpdateSlots();
            //SetDropInventory();
        }


        public virtual void SetControl()
        {
            //
        }


        public override void Update()
        {
            if(EntityControlHandler != null)
            {
                ModelStateManager.Update(this);
            }
            

            if (StatsManager.GetStatAbility(EntityStatFeatures.GCS) != null)
            {
                Model.UpdateSurroundingRectangles();
            }

            StatsManager.UpdateAbilities(Model);
            StatsManager.UpdateStatEffects();

            if (UpdatesModelStates)
            {
                ModelStateManager.Apply(this);
            }

            base.Update();
        }


        public virtual void SetStats()
        {
            StatsManager = new StatsManager();
            EntityFraction = EntityFractions.NEUTRAL;
            //BloodDropParticle = ParticleSet.ParticleSets.NONE;
        }

        public virtual void SetInventory()
        {
            Inventory = new Inventory();
        }

        public virtual void SetDropInventory()
        {
            DropInventory = new DropInventory();
        }

        public override void DrawCollider()
        {
            if(Model.UpdatesSurroundingRectangles)
            {
                Model.DrawSurroundigRectangles();
            }
            
            base.DrawCollider();
        }
    }
}
