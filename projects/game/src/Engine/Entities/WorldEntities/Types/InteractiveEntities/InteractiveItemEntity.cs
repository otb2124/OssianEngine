using Graphics;
using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class InteractiveItemEntity : InteractiveEntity
    {

        public enum InteractiveItems
        {
            GOLD_COIN,
        }

        public Inventory Containment;
        public InteractionTriggers interactiveItemTrigger;

        public InteractiveItemEntity(Utils.Models modelPreset, Vector2 pos, Vector2 interactionFieldSize, InteractionTriggers interactiveItemType, Inventory containment) : base(modelPreset, pos, interactionFieldSize)
        {
            this.interactiveItemTrigger = interactiveItemType;
            Containment = containment;
        }

        public InteractiveItemEntity(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize, InteractionTriggers interactiveItemType, Inventory containment) : base(sprite, body, pos, interactionFieldSize)
        {
            this.interactiveItemTrigger = interactiveItemType;
            Containment = containment;
        }

        public InteractiveItemEntity(StaticSpriteFactory.SpriteData spriteData, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize, InteractionTriggers interactiveItemType, Inventory containment) : base(spriteData, body, pos, interactionFieldSize)
        {
            this.interactiveItemTrigger = interactiveItemType;
            Containment = containment;
        }

        public InteractiveItemEntity(InteractiveItems preset, Vector2 pos) : base()
        {
            switch(preset)
            {
                case InteractiveItems.GOLD_COIN:
                    Init(StaticSpriteFactory.GetItemUISpriteByItemKey(new ItemKey(ItemLib.Currencies.GOLD_COIN)), FlatBodyPreset.COIN, pos, new Vector2(30, 30), InteractionTriggers.AUTO, new Inventory(new ItemKey[] { new ItemKey(ItemLib.Currencies.GOLD_COIN) }));
                    break;
            }
            
        }

        public InteractiveItemEntity() : base()
        {

        }

        public override void SetEmission()
        {
            Emission = new LightSource.LightSourceData(LightSource.LightSourceData.LightSourceForms.CIRCULAR, new Vector2(10f, 0f), Vector2.Zero, new Color(1f, 1f, 0.8f, 0.7f), 50f, 0f);
            base.SetEmission();
        }
        public virtual void Init(Utils.Models modelPreset, Vector2 pos, Vector2 interactionFieldSize, InteractionTriggers interactiveItemType, Inventory containment)
        {
            this.interactiveItemTrigger = interactiveItemType;
            Containment = containment;
            base.Init(modelPreset, pos, interactionFieldSize);
        }


        public virtual void Init(StaticSprites sprite, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize, InteractionTriggers interactiveItemType, Inventory containment)
        {
            this.interactiveItemTrigger = interactiveItemType;
            Containment = containment;
            base.Init(sprite, body, pos, interactionFieldSize);
        }

        public virtual void Init(StaticSpriteFactory.SpriteData spriteData, FlatBodyPreset body, Vector2 pos, Vector2 interactionFieldSize, InteractionTriggers interactiveItemType, Inventory containment)
        {
            this.interactiveItemTrigger = interactiveItemType;
            Containment = containment;
            base.Init(spriteData, body, pos, interactionFieldSize);
        }
    }
}
