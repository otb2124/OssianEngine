using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Entities
{
    public class Weapon : EquipmentEntity
    {

        public AnimationManager aManager;
        public StaticSpriteFactory.StaticSprites sprite;

        public Weapon()
        {
            aManager = new AnimationManager();

            this.sprite = StaticSpriteFactory.StaticSprites.SWORD;
            this.aManager.AddStaticAnimation(this.sprite);
        }



        public override void Draw(Hitbox hitboxData)
        {
            //model
            Rectangle spriteSize = aManager.GetCurrent().GetCurrentFrame();
            float scaleX = 1f;
            float scaleY = 1f;
            Vector2 newPos = new Vector2(hitboxData.extends.Center.X, hitboxData.extends.Center.Y);
            Vector2 textureCenter = new Vector2(spriteSize.Width / 2f, spriteSize.Height / 2f);

            //for offsets
            //float bodyWidth = hitboxData.extends.Width + bodyOffset.X;
            //float bodyHeight = hitboxData.extends.Height + bodyOffset.Y;

            float bodyWidth = hitboxData.extends.Width;
            float bodyHeight = hitboxData.extends.Height;

            scaleX = bodyWidth / spriteSize.Width;
            scaleY = bodyHeight / spriteSize.Height;
            newPos = hitboxData.extends.Center - new Vector2(bodyWidth / 2f, bodyHeight / 2f);
            newPos += new Vector2(spriteSize.Width / 2f * scaleX, spriteSize.Height / 2f * scaleY);
            


            this.aManager.GetCurrent().Draw(newPos, Color.White, hitboxData.extends.Rotation, textureCenter, new Vector2(scaleX, scaleY), 0f);
        }



    }
}
