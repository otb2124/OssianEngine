using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Resources;
using Utils;
using System.Linq;

namespace Graphics
{
    public class BackgroundManager
    {

        public ParallaxBackground parallax;
        public List<DynamicBackgroundEvent> events;

        public List<BackgroundEntity> entities;
        public List<BackgroundEntity> entitiesToRemove;

        public void Init()
        {
            events = BackgroundSetter.SetDynamicBackgroundEvents(Entities.Entities.entityMapManager.CurrentMapId);
            parallax = BackgroundSetter.SetParallax(Entities.Entities.entityMapManager.CurrentMapId);
            entities = BackgroundSetter.SetEntities(Entities.Entities.entityMapManager.CurrentMapId);
            entitiesToRemove = new List<BackgroundEntity>();
        }

        public void Update()
        {

            if (GameStateManager.gameMode == GameStateManager.GameModes.PLAY_MODE)
            {
                parallax.Update();
                for (global::System.Int32 i = 0; i < events.Count; i++)
                {
                    events[i].Update();
                }
            }
            

            foreach (var backgroundEntity in entitiesToRemove)
            {
                entities.Remove(backgroundEntity);
            }
        }

        public void AddEntity(BackgroundEntity ent)
        {
            this.entities.Add(ent);
        }


        public void RemoveEntity(BackgroundEntity ent)
        {
            this.entitiesToRemove.Add(ent);
        }

        public void RemoveAll()
        {
            foreach (BackgroundEntity entities in entities)
            {
                RemoveEntity(entities);
            }
        }

        public void Draw()
        {
            if (GameStateManager.gameMode == GameStateManager.GameModes.PLAY_MODE)
            {
                // Draw entities before the first parallax layer (LayerToDrawOn < 0)
                foreach (var background in entities
                    .Where(e => e is BackgroundEntity && e.LayerToDrawOn < 0)
                    .OrderBy(e => StaticSpriteFactory.spriteMappings[e.sprite].z))
                {
                    background.Draw();
                }

                // Draw interleaved entities and parallax back mapLayers
                for (int i = 0; i < parallax.ParallaxBackLayers.Length; i++)
                {
                    // Draw entities for LayerToDrawOn == i (before layer i)
                    foreach (var background in entities
                        .Where(e => e is BackgroundEntity && e.LayerToDrawOn == i)
                        .OrderBy(e => StaticSpriteFactory.spriteMappings[e.sprite].z))
                    {
                        background.Draw();
                    }

                    // Draw parallax back layer i
                    parallax.DrawParallaxBackLayer(i);
                }

                // Draw entities over the last parallax back layer (LayerToDrawOn == Length)
                foreach (var background in entities
                    .Where(e => e is BackgroundEntity && e.LayerToDrawOn == parallax.ParallaxBackLayers.Length)
                    .OrderBy(e => StaticSpriteFactory.spriteMappings[e.sprite].z))
                {
                    background.Draw();
                }

                // Draw entities after all parallax back mapLayers (LayerToDrawOn > Length)
                foreach (var background in entities
                    .Where(e => e is BackgroundEntity && e.LayerToDrawOn > parallax.ParallaxBackLayers.Length)
                    .OrderBy(e => StaticSpriteFactory.spriteMappings[e.sprite].z))
                {
                    background.Draw();
                }
            }
        }

        public void DrawCanvas()
        {
            if (GameStateManager.gameMode == GameStateManager.GameModes.PLAY_MODE)
            {
                parallax.DrawCanvas();
            }
        }

        public void DrawParallaxFrontLayers()
        {
            if (GameStateManager.gameMode == GameStateManager.GameModes.PLAY_MODE)
            {
                parallax.DrawParallaxFrontLayers();
            }
        }

    }
}
