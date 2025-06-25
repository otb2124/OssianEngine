using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Resources;
using Utils;
using System.Linq;

namespace Graphics
{
    public class BackgroundManager
    {


        public enum BackgroundState
        {
            NONE,
            CLOUDS
        }

        public ParallaxBackground parallax;
        public List<BackgroundEntity> backgrounds;
        public List<BackgroundEntity> backgroundsToRemove;
        public BackgroundState state;

        public void Init()
        {
            state = BackgroundSetter.SetBackgroundState(Entities.Entities.entityMapManager.CurrentMapId);
            parallax = BackgroundSetter.SetParallax(Entities.Entities.entityMapManager.CurrentMapId);
            backgrounds = BackgroundSetter.SetBackgrounds(Entities.Entities.entityMapManager.CurrentMapId);
            backgroundsToRemove = new List<BackgroundEntity>();
        }

        public void Update()
        {
            
            if(GameStateManager.gameMode != GameStateManager.GameModes.debugMode)
            {
                parallax.Update();
                BackgroundEntityDynamicsHandler.Handle();
            }
            

            foreach (var backgroundEntity in backgroundsToRemove)
            {
                backgrounds.Remove(backgroundEntity);
            }
        }

        public void AddEntity(BackgroundEntity ent)
        {
            this.backgrounds.Add(ent);
        }


        public void RemoveEntity(BackgroundEntity ent)
        {
            this.backgroundsToRemove.Add(ent);
        }

        public void Draw()
        {
            if (GameStateManager.gameMode != GameStateManager.GameModes.debugMode)
            {
                parallax.DrawParallaxBackLayers();

                foreach (var background in backgrounds
                .Where(e => e is BackgroundEntity)
                .OrderBy(e => StaticSpriteFactory.spriteMappings[(e).sprite].z))
                {
                    background.Draw();   
                }
            }
        }

        public void DrawCanvas()
        {
            if (GameStateManager.gameMode != GameStateManager.GameModes.debugMode)
            {
                parallax.DrawCanvas();
            }
        }

        public void DrawParallaxFrontLayers()
        {
            parallax.DrawParallaxFrontLayers();   
        }

    }
}
