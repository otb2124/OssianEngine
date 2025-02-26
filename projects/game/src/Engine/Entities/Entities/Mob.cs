using Microsoft.Xna.Framework;
using Physics;
using Resources;


namespace Entities
{
    public class Mob : LivingEntity
    {

        public Mob(Vector2 pos, float rotation) : base(FlatBodyFactory.FlatBodyPreset.HUMANOID, Sprite.Sprites.MOB, pos, rotation)
        {
            sprite.zIndex = -10;
        }


        public override void setStats()
        {
            stats.maxHP = 100;
            stats.HP = 100;
            stats.dmg = 1;
            stats.maxSpeed = 5;
            stats.speed = 5;

            base.setStats();
        }


        public override void Update()
        {
            base.Update();
        }


        public override void Draw()
        {
            base.Draw();
        }
    }
}
