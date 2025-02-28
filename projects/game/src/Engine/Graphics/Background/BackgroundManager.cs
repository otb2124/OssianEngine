using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Resources;

namespace Graphics
{
    public class BackgroundManager
    {

        public List<BackgroundEntity> backgrounds;

        public void Init()
        {
            backgrounds = new List<BackgroundEntity>();
            backgrounds.Add(new BackgroundEntity(StaticSpriteFactory.StaticSprites.BACKGROUND, Vector2.Zero) { isStickToCamera = true});
            backgrounds.Add(new BackgroundEntity(StaticSpriteFactory.StaticSprites.DRAGON, new Vector2(-200, 0)));
        }

        public void Update()
        {
            foreach (var background in backgrounds)
            {
                background.Update();
            }
        }

        public void Draw()
        {
            foreach (var background in backgrounds)
            {
                background.Draw();
            }
        }
    }
}
