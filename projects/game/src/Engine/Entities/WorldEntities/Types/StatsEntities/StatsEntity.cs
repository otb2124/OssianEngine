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


        public override void Update()
        {
            if(StatsManager.CheckDead())
            {
                Die();
                return;
            }


            StatsManager.UpdateAbilities(Model);
            StatsManager.UpdateStatEffects();

            if (UpdatesModelStates)
            {
                ModelStateManager.Apply(this);
            }

            if(StatsManager.GetStatAbilities(EntityStatFeatures.GCS) != null)
            {
                Model.UpdateSurroundingRectangles();
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


        public virtual void Die()
        {
            if(DropInventory != null)
            {
                if (!DropInventory.IsEmpty())
                {
                    List<Item> droppedItems = DropInventory.TryDrop();

                    foreach (Item item in droppedItems)
                    {
                        InteractiveItemEntity itemEnt = EntityHelper.CreateItemDrop(item, Model.Body.Position.ToVector2());
                        Entities.EntityMapManager.GetCurrentMap().Entities.Add(itemEnt);
                        Graphics.Graphics.LightManager.AddEntityEmissionLightSource(itemEnt);
                    }
                }
            }

            Entities.EntityManager.RemoveEntity(this);
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
