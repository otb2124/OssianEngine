using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class BackgroundManager
    {

        public List<BackgroundEntity> backgrounds;

        public void Init()
        {
            backgrounds = new List<BackgroundEntity>();
            backgrounds.Add(new BackgroundEntity(Resources.SpriteFactory.Sprites.BACKGROUND, Vector2.Zero) { isStickToCamera = true});
            backgrounds.Add(new BackgroundEntity(Resources.SpriteFactory.Sprites.DRAGON, new Vector2(-200, 0)));
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
