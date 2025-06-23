using Microsoft.Xna.Framework;
using Physics;
using Resources;
using Utils;


namespace Entities
{
    public class Mob : LivingEntity
    {


        public Mob(Vector2 pos, float rotation) : base(Models.MOB, pos, rotation)
        {
        }


        public override void setStats()
        {
            sManager.stats.maxHP = 100;
            sManager.stats.HP = 100;
            sManager.stats.maxSpeed = 5;
            sManager.stats.speed = 5;

            sManager.equipmentManager.weaponL.physDmg = 1;

            base.setStats();
        }


        public override void Update()
        {
            this.sManager.equipmentManager.GetCurrentWeapon().hitbox.Update(FlatConverter.ToVector2(this.model.body.Position), new Vector2(this.model.body.Width * 2, this.model.body.Height), 0f);
            this.sManager.equipmentManager.armorHB.Update(FlatConverter.ToVector2(this.model.body.Position), new Vector2(this.model.body.Width, this.model.body.Height), 0f);
        }


        public override void Draw()
        {
            base.Draw();
        }
    }
}
