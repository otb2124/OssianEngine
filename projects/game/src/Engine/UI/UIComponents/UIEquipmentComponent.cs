using Entities;
using Myra.Graphics2D.UI;
using Resources;
using System.Collections.Generic;
using static Entities.EquipmentSlot;

namespace UI
{
    public class UIEquipmentComponent : UIComponent
    {
        private static readonly Dictionary<string, EquipmentSlots> SlotMap = new()
        {
            { "eqHead",         EquipmentSlots.HEAD         },
            { "eqTorso",        EquipmentSlots.TORSO        },
            { "eqHands",        EquipmentSlots.HANDS        },
            { "eqLegs",         EquipmentSlots.LEGS         },

            { "eqNecklace",     EquipmentSlots.NECKLACE     },
            { "eqCape",         EquipmentSlots.CAPE         },
            { "eqContainment",  EquipmentSlots.CONTAINMENT  },
            { "eqBelt",         EquipmentSlots.BELT         },

            { "eqPet0",         EquipmentSlots.PET_0        },
            { "eqPet1",         EquipmentSlots.PET_1        },
            { "eqWeapon",       EquipmentSlots.WEAPON       },
            { "eqRing0",        EquipmentSlots.RING_0       },
            { "eqRing1",        EquipmentSlots.RING_1       },
        };

        public UIEquipmentComponent()
        {
            SetTemplate(UITemplates.EQUIPMENT);
        }

        public override void Init()
        {
            var frame = UI.UIManager.UIDesktop.FindById("equipmentFrame") as Panel;

            foreach (var kvp in SlotMap)
            {
                var widget = UI.UIManager.UIDesktop.FindById(kvp.Key) as ImageButton;
                if (widget == null) continue;

                UI.UIManager.UIDesktop.DragDropService
                    .RegisterEquipmentSlot(widget, frame, kvp.Value);
            }

            var btnClose = UI.UIManager.UIDesktop.FindById("btnCloseEquipment") as TextButton;
            if (btnClose != null)
                btnClose.TouchUp += (s, e) => UI.UIManager.ExecuteAction("ingame.equipment");

            base.Init();
        }
    }
}