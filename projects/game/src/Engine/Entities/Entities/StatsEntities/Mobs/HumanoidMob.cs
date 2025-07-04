using Microsoft.Xna.Framework;
using Physics;
using Resources;
using UI;
using Utils;


namespace Entities
{
    public class HumanoidMob : EquipmentEntity
    {


        public HumanoidMob(Models modelPreset, Vector2 pos, float rotation) : base(modelPreset, pos, rotation)
        {
        }

        public override void SetStats()
        {
            base.SetStats();

            Stats.maxHP = 100;
            Stats.HP = 100;
            Stats.maxSpeed = 5;

            Stats.Refill();
        }

        public override void SetEquipment()
        {
            base.SetEquipment();

            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.WEAPON_L).Equipment = (WeaponEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Weapons.TERRABLADE));
            EquipmentManager.GetEquipmentSlot(EquipmentSlot.EquipmentSlots.CHESTPLATE).Equipment = (ArmorEquipment)ItemFactory.CreateItem(new ItemKey(ItemLib.Armors.IRON_CHESTPLATE));
        }


        public override void Update()
        {
            EquipmentManager.GetCurrentWeapon().hitbox.Update(FlatConverter.ToVector2(this.Model.body.Position), new Vector2(this.Model.body.Width * 2, this.Model.body.Height), 0f);
            EquipmentManager.GetCurrentArmor().hitbox.Update(FlatConverter.ToVector2(this.Model.body.Position), new Vector2(this.Model.body.Width, this.Model.body.Height), 0f);
        }


        public override void Draw()
        {
            base.Draw();
        }
    }
}
