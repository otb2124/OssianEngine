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
            statsManager.stats.maxHP = 100;
            statsManager.stats.HP = 100;
            statsManager.stats.maxSpeed = 5;

            statsManager.stats.Refill();

            statsManager.equipmentManager = new EquipmentManager();
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_L).Equipment = (WeaponEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE));
            statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_CHESTPLATE));

            base.SetStats();
        }


        public override void Update()
        {
            this.statsManager.equipmentManager.GetCurrentWeapon().hitbox.Update(FlatConverter.ToVector2(this.model.body.Position), new Vector2(this.model.body.Width * 2, this.model.body.Height), 0f);
            ((ArmorEquipment)this.statsManager.equipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment).hitbox.Update(FlatConverter.ToVector2(this.model.body.Position), new Vector2(this.model.body.Width, this.model.body.Height), 0f);
        }


        public override void Draw()
        {
            base.Draw();
        }
    }
}
