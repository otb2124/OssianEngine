using Graphics;
using Microsoft.Xna.Framework;
using Physics;

namespace Entities
{
    public class PlatformEntity : PhysicalEntity
    {

        public AnimationManager[] aManagers;

        public PlatformEntity(Utils.Models model, Vector2 pos, float rot) : base(model, pos, rot)
        {
            aManagers = new AnimationManager[2];
            for (int i = 0; i < aManagers.Length; i++)
            {
                aManagers[i] = new AnimationManager();
            }
            aManagers[0].AddStaticAnimation(Utils.StaticSprites.ENTITIES_STATIC_PLATFORM_0_1);
            aManagers[1].AddStaticAnimation(Utils.StaticSprites.ENTITIES_STATIC_PLATFORM_0_2);
        }
        public override void Draw()
        {
            base.Draw(); //draw top

            

            this.aManagers[0].GetCurrent().Draw(FlatConverter.ToVector2(this.model.body.Position), Color.White, model.body.Angle, Vector2.Zero, Vector2.One, 0f);
            this.aManagers[1].GetCurrent().Draw(FlatConverter.ToVector2(this.model.body.Position), Color.White, model.body.Angle, Vector2.Zero, Vector2.One, 0f);


        }
    }
}
