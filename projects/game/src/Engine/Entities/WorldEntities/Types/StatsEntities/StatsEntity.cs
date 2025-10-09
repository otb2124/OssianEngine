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

        public bool CanRegensStamina;
        public bool CanUpdateIFrames;
        public bool CanFall;
        public bool CanHangLedges;
        public bool CanFly;

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

        public StatsEntity(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, float rotation = 0f) : base(sprite, body, pos, rotation)
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


            if(UpdatesModelStates)
            {
                EntityModelStateHandler.Update(this);
            }

            if (CanRegensStamina)
            {
                StatsManager.RegenStamina();
            }
            if (CanUpdateIFrames)
            {
                StatsManager.UpdateInvincibleFrames();
            }
            if(CanFall)
            {
                StatsManager.UpdateFallen(Model);
            }
            if(CanHangLedges)
            {
                StatsManager.UpdateLedgeHanging(Model);
            }
            if(CanFly)
            {
                StatsManager.UpdateFly(this);
            }

            if(Model.UpdatesSurroundingRectangles)
            {
                Model.UpdateSurroundingRectangles();
                StatsManager.UpdateGCSStates(Model);
            }
            
            StatsManager.UpdateDescending(Model);
            StatsManager.UpdatePickup();

            base.Update();
        }


        public virtual void SetStats()
        {
            StatsManager = new StatsManager();
            EntityFraction = EntityFractions.NEUTRAL;
            //BloodDropParticle = ParticleSet.ParticleSets.NONE;

            CanRegensStamina = false;
            CanUpdateIFrames = false;
            CanFall = false;
            CanHangLedges = false;
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
                        Graphics.Graphics.lightManager.AddEntityEmissionLightSource(itemEnt);
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
