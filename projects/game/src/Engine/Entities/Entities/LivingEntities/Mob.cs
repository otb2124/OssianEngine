using Microsoft.Xna.Framework;
using Physics;
using Resources;
using UI;
using Utils;


namespace Entities
{
    public class Mob : LivingEntity
    {


        public Mob(Vector2 pos, float rotation) : base(Models.MOB, pos, rotation)
        {
        }


        public override void SetStats()
        {
            sManager.stats.maxHP = 100;
            sManager.stats.HP = 100;
            sManager.stats.maxSpeed = 5;

            sManager.stats.Refill();

            sManager.equipmentManager = new EquipmentManager();
            sManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_L).Equipment = (WeaponEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE));
            sManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_CHESTPLATE));

            base.SetStats();
        }


        public override void Update()
        {
            this.sManager.equipmentManager.GetCurrentWeapon().hitbox.Update(FlatConverter.ToVector2(this.model.body.Position), new Vector2(this.model.body.Width * 2, this.model.body.Height), 0f);
            ((ArmorEquipment)this.sManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment).hitbox.Update(FlatConverter.ToVector2(this.model.body.Position), new Vector2(this.model.body.Width, this.model.body.Height), 0f);
        }


        public override void Draw()
        {
            base.Draw();
        }
    }
}
