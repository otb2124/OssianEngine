using Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Utils;
using Resources;
using static Resources.ModelFactory;

namespace Entities
{
    public class EquipmentEntity : NPCEntity
    {

        public EquipmentManager EquipmentManager;

        public EquipmentEntity(Models modelPreset, Vector2 pos, float rot = 0) : base()
        {
            SetEquipment();
            base.Init(modelPreset, pos, rot);
        }

        public EquipmentEntity() : base()
        {
            SetEquipment();
        }


        public virtual void SetEquipment()
        {
            EquipmentManager = new EquipmentManager();
        }


        public override void SetAppearance()
        {
            //Model.AManagers = new List<AnimationSet>();
            Model.ModelAppearance = new ModelAppearance();
            SetBodyAppearance();
            SetEquipmentAnimations();
        }

        public override void SetBodyAppearance()
        {
            ModelAppearancePart bodyPart = new ModelAppearancePart(EntityAppearanceAttributes.BODY);
            bodyPart.AddAnimationSet(AnimationSetSetter.CreateAnimationSetBySpriteSheet(Model.SpriteData.SpriteSheet));
            Model.ModelAppearance.AppearanceParts.Add(bodyPart);
        }

        public virtual void SetEquipmentAnimations()
        {
            ModelAppearancePart armorPart = new ModelAppearancePart(EntityAppearanceAttributes.ARMOR);

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
