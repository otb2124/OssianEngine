using Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Utils;

namespace Entities
{
    public class EquipmentEntity : NPCEntity
    {

        public EquipmentManager EquipmentManager;

        public EquipmentEntity(Models modelPreset, Vector2 pos, float rot = 0) : base(modelPreset, pos, rot)
        {
            SetEquipment();
        }

        public EquipmentEntity() : base()
        {
            SetEquipment();
        }


        public virtual void SetEquipment()
        {
            EquipmentManager = new EquipmentManager();
        }

        public virtual void SetEquipmentAnimations()
        {
            ModelAppearancePart armorPart = new ModelAppearancePart(ModelAppearanceParts.ARMOR);

            foreach (EquipmentSlot slot in EquipmentManager.Equipments.EquipmentSlots)
            {
                if (slot.Equipment is ArmorEquipment armorEq)
                {
                    armorPart.AddAnimationSet(new AnimationSet(armorEq.SpriteSheet, AnimationSetSetter.CreateAnimationSetBySpriteSheet(Model.SpriteData.SpriteSheet).Anims));
                }
            }

            Model.ModelAppearance.AppearanceParts.Add(armorPart);
        }

        public override void UpdateBattleBodyManager()
        {
            BattleBodyManager.Update(Model, EquipmentManager);
        }

        public override void SetBattleBodies()
        {
            BattleBodyManager = new BattleBodyManager(BattleBodyTypes.WEAPON);
        }
    }
}
