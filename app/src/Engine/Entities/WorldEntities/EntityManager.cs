using Resources;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Utils;
using System.Diagnostics;
using System;
using Physics;
using Graphics;

namespace Entities
{
    public class EntityManager
    {

        private int nextId = 1;
        private readonly float EntityOutOfBoudsDeletionMapSizeMultiplier = 1.5f;

        public EntityManager()
        {
            
        }

        public void Init()
        {
            //Entities.EntityMapManager.maps[Entities.EntityMapManager.CurrentMapId].Entities = EntityMapSetter.FillEntityMapLayer(Entities.EntityMapManager.CurrentMapId);
        }

        public void Update()
        {
            var entitiesSnapshot = Entities.EntityMapManager.GetCurrentMapLayer().Entities.ToList();

            foreach (var entFrom in entitiesSnapshot)
            {

                //if entityFrom is physical
                if (entFrom is PhysicalEntity)
                {

                    //if entityFrom is stats or interactive
                    if (entFrom is StatsEntity || entFrom is InteractiveEntity)
                    {
                        entFrom.Update();

                        //contact A to B
                        foreach (var entTo in entitiesSnapshot)
                        {

                            if(entFrom != entTo)
                            {
                                //if entityTo is also stats 
                                if (entTo is StatsEntity)
                                {
                                    if(entFrom is BattleEntity && entTo is BattleEntity)
                                    {
                                        HitboxChecker.CheckWeaponToBodyCollision((BattleEntity)entFrom, (BattleEntity)entTo);
                                        HitboxChecker.CheckWeaponToWeaponCollision((BattleEntity)entFrom, (BattleEntity)entTo);
                                    }

                                    if(entFrom is InteractiveEntity && entTo is Player)
                                    {
                                        HitboxChecker.CheckForInterraction((InteractiveEntity)entFrom, (EquipmentEntity)entTo);
                                    }

                                    if(entFrom is NPCEntity npcEnt && entTo is Player)
                                    {
                                        if(npcEnt.InteractionManager != null)
                                        {
                                            HitboxChecker.CheckForInterraction((NPCEntity)entFrom, (EquipmentEntity)entTo);
                                        }
                                    }
                                }
                            }

                            
                        }
                    }

                    RemovePhysicalEntityWhenOutOfBounds((PhysicalEntity)entFrom);
                }
            }
        }


        public void SetShaders()
        {
            foreach (var entity in Entities.EntityMapManager.GetCurrentMapLayer().Entities)
            {
                if(entity is PhysicalEntity physEnt && physEnt.EntityFX != null)
                {
                    physEnt.EntityFX.SetShaders();
                }
            }
        }


        public bool HasPlayer()
        {
            return Entities.EntityMapManager.GetCurrentMapLayer().Entities.Contains(Entities.Player);
        }

        public void AddEntity(WorldEntity ent)
        {
            if (ent is PhysicalEntity physicalEntity)
            {
                Physics.Physics.flatWorld.AddBody(physicalEntity.Model.Body);
            }
            Entities.EntityMapManager.GetCurrentMapLayer().Entities.Add(ent);

        }
        public void RemoveEntity(WorldEntity ent)
        {
            if (ent is PhysicalEntity physicalEntity)
            {
                Physics.Physics.flatWorld.RemoveBody(physicalEntity.Model.Body);
            }
            Entities.EntityMapManager.GetCurrentMapLayer().Entities.Remove(ent);   
        }

        public void RemoveAll()
        {
            var entitiesSnapshot = Entities.EntityMapManager.GetCurrentMapLayer().Entities.ToList();
            foreach (WorldEntity entity in entitiesSnapshot)
            {
                if(!(entity is Player))
                {
                    RemoveEntity(entity);
                }
                
            }
        }

        public void RemovePhysicalEntityWhenOutOfBounds(PhysicalEntity ent)
        {
            Vector2 entPos = ent.Model.Body.Position.ToVector2();
            Vector2 mapSize = Entities.EntityMapManager.GetCurrentMapLayer().Size.ToVector2() * EntityOutOfBoudsDeletionMapSizeMultiplier;
            Rectangle worldBounds = new Rectangle((int)-mapSize.X/2, (int)-mapSize.Y/2, (int)mapSize.X, (int)mapSize.Y);

            if (!worldBounds.Contains(entPos))
            {
                Console.WriteLine("Deleted at: " + entPos + " , MapSize: " + worldBounds);
                RemoveEntity(ent);
            }
        }


        public int GenerateId()
        {
            if (Entities.EntityMapManager == null || Entities.EntityMapManager.maps == null || Entities.EntityMapManager.CurrentMapId < 0 || Entities.EntityMapManager.CurrentMapId >= Entities.EntityMapManager.maps.Length)
            {
                return nextId++;
            }

            var entities = Entities.EntityMapManager.GetCurrentMapLayer().Entities ?? (Entities.EntityMapManager.GetCurrentMapLayer().Entities = new List<WorldEntity>());
            while (entities.Any(e => e.Id == nextId))
            {
                nextId++;
                if (nextId < 0)
                {
                    nextId = 1;
                }
            }
            return nextId++;
        }

        public WorldEntity GetEntityById(int id)
        {
            return Entities.EntityMapManager.GetCurrentMapLayer().Entities.FirstOrDefault(e => e.Id == id);
        }

        public NPCEntity GetEntityByEntityDialogueId(int entityDialogueId)
        {
            foreach (WorldEntity entity in Entities.EntityMapManager.GetCurrentMapLayer().Entities)
            {
                if(entity is NPCEntity interactiveEnt)
                {
                    if(interactiveEnt.InteractionManager != null)
                    {
                        if(interactiveEnt.InteractionManager.InteractionData != null)
                        {
                            if(interactiveEnt.InteractionManager.InteractionData.DialogueSequenceData != null)
                            {
                                if(interactiveEnt.InteractionManager.InteractionData.DialogueSequenceData.EntityDialogueId == entityDialogueId)
                                {
                                    return interactiveEnt;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }


        //models
        public void Draw()
        {
            var sortedEntities = Entities.EntityMapManager.GetCurrentMapLayer().Entities
                .OrderBy(e =>
                    (e is TileEntity plent) ? plent.SpriteZ
                    : (e is PhysicalEntity phent) ? phent.SpriteZ
                    : (e is PlatformEntity platformEntity) ? 0
                    : float.MaxValue);

            foreach (var entity in sortedEntities)
            {
                // Skip entities with FX — they get drawn separately via BlitEntityFXResults
                //if (entity is PhysicalEntity phys && phys.EntityFX != null && phys.EntityFX.HasEffects)
                //    continue;

                entity.Draw();
            }
        }

        public void DrawFXEntities()
        {
            var sortedEntities = Entities.EntityMapManager.GetCurrentMapLayer().Entities
                .OrderBy(e => (e is PhysicalEntity phent) ? phent.SpriteZ : float.MaxValue);

            foreach (var entity in sortedEntities)
            {
                if (entity is PhysicalEntity phys && phys.EntityFX != null && phys.EntityFX.HasEffects)
                    entity.Draw();  // this sets _pendingFXResult
            }
        }



        //collisions
        public void DrawColliders()
        {
            foreach (var entity in Entities.EntityMapManager.GetCurrentMapLayer().Entities)
            {
                if (entity is PhysicalEntity physEnt)
                {
                    physEnt.DrawCollider();

                    if (entity is InteractiveEntity iEnt)
                    {
                        iEnt.DrawInteractionField();
                    }
                }
            }
        }

        
        //hitboxes
        public void DrawHitboxes()
        {
            foreach (var entity in Entities.EntityMapManager.GetCurrentMapLayer().Entities)
            {
                if (entity is BattleEntity bEnt)
                {
                    bEnt.DrawHitboxes();
                }
            }
        }

        public void BlitEntityFXResults(Sprites sprites, Rectangle fullRect)
        {
            foreach (var entity in Entities.EntityMapManager.GetCurrentMapLayer().Entities)
            {
                if (entity is PhysicalEntity phys)
                    phys.BlitFXResult(sprites, fullRect);
            }
        }
    }
}
