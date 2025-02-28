using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class BackgroundEntity
    {


        public Vector2 pos;
        public Sprite sprite;

        public bool isStickToCamera;
        public bool isStickToZoom;

        public BackgroundEntity(StaticSpriteFactory.StaticSprites spritePreset, Vector2 pos) 
        {
            sprite = StaticSpriteFactory.GetSprite(spritePreset);
            this.pos = pos;
        }

        public void Update()
        {
            
        }

        public void Draw()
        {
            Vector2 adjustedPos = pos;

            if(isStickToCamera)
            {
                adjustedPos += Graphics.camera.position;
            }

            sprite.Draw(adjustedPos);
        }
    }
}
