using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Resources;
using Utils;
using Entities;
using System.Linq;

namespace Graphics
{
    public class BackgroundManager
    {


        public enum BGState
        {
            NONE,
            CLOUDS
        }

        public ParallaxBackground parallax;
        public List<BackgroundEntity> backgrounds;
        public List<BackgroundEntity> backgroundsToRemove;

        public BGState state;

        public void Init()
        {
            state = BGState.CLOUDS;

            //parallax
            parallax = new ParallaxBackground();

            //bg entities
            backgrounds = new List<BackgroundEntity>();
            backgroundsToRemove = new List<BackgroundEntity>();
            backgrounds.Add(new BackgroundEntity(StaticSprites.GRAPHICS_STATIC_DRAGON, new Vector2(-200, 0), BackgroundEntity.BGEntityDynamics.STATIC));
            backgrounds.Add(new BackgroundEntity(StaticSprites.GRAPHICS_SUN, new Vector2(-200, 200), BackgroundEntity.BGEntityDynamics.STATIC) { isStickToCamera = true, isStickToZoom = true });
        }

        public void Update()
        {
            parallax.Update();


            if(GameStateManager.gameMode != GameStateManager.GameModes.debugMode)
            {
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
                parallax.Draw();

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
            parallax.DrawCanvas();
        }

    }
}
