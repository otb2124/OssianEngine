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

        public List<BackgroundEntity> backgrounds;
        public List<BackgroundEntity> backgroundsToRemove;

        public BGState state;

        public void Init()
        {
            state = BGState.CLOUDS;
            backgrounds = new List<BackgroundEntity>();
            backgroundsToRemove = new List<BackgroundEntity>();
            backgrounds.Add(new BackgroundEntity(StaticSprites.BACKGROUND, Vector2.Zero, BackgroundEntity.BGEntityDynamics.STATIC) { isStickToCamera = true});
            backgrounds.Add(new BackgroundEntity(StaticSprites.DRAGON, new Vector2(-200, 0), BackgroundEntity.BGEntityDynamics.STATIC));
            backgrounds.Add(new BackgroundEntity(StaticSprites.BG_SUN, new Vector2(-200, 200), BackgroundEntity.BGEntityDynamics.STATIC) { isStickToCamera = true, isStickToZoom = true });
        }

        public void Update()
        {
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
                foreach (var background in backgrounds
                .Where(e => e is BackgroundEntity)
                .OrderBy(e => StaticSpriteFactory.spriteMappings[(e).sprite].z))
                {
                    if (!(background.sprite == StaticSprites.BACKGROUND))
                    {
                        background.Draw();
                    }
                }
            }
        }

        public void DrawCanvas()
        {
            foreach (var background in backgrounds
                .Where(e => e is BackgroundEntity))
            {
                if(background.sprite == StaticSprites.BACKGROUND)
                {
                    background.Draw();
                }
                
            }
        }

    }
}
