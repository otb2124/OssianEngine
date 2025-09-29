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

        public EntityStats Stats;
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
            if(Stats.CheckDead())
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
                Stats.RegenStamina();
                Stats.OnUsingStamina = false;
            }
            if (CanUpdateIFrames)
            {
                Stats.UpdateInvincibleFrames();
            }
            if(CanFall)
            {
                Stats.UpdateFallen(this.Model);
            }
            if(CanHangLedges)
            {
                Stats.UpdateLedgeHanging(this);
            }
            if(CanFly)
            {
                Stats.UpdateFly(this);
            }

            if(Model.UpdatesSurroundingRectangles)
            {
                Model.UpdateSurroundingRectangles();
            }
            
            Stats.UpdateDescending(this);
            Stats.UpdatePickup();

            base.Update();
        }


        public virtual void SetStats()
        {
            Stats = new EntityStats();
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
