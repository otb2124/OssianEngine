using Entities;
using Microsoft.Xna.Framework;
using Resources;
using System;
using static Resources.StaticSpriteFactory;
using Utils;

namespace UI
{
    public class UIDialogueComponent : UIComponent
    {

        public Dialogue Dialogue;

        public UIDialogueComponent(int id, Dialogue dialogue) : base(id)
        {
            type = UIComponentTypes.DIALOGUE;

            children = new UIComponent[2];

            children[0] = new UIDialoguePanelComponent(-1, dialogue);
            children[1] = new UIDialogueOptionsPanelComponent(-1, dialogue);

            SetDialogue(dialogue);
        }

        public void SetDialogue(Dialogue dialogue)
        {
            Dialogue = dialogue;

            ((UIDialoguePanelComponent)children[0]).SetDialogue(dialogue);
            ((UIDialogueOptionsPanelComponent)children[1]).SetDialogueOptions(dialogue);
        }


        public override void Update()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    if(children[i] != null)
                    {
                        children[i].Update();
                    }
                }
            }
        }

        public override void Draw()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    if (children[i] != null)
                    {
                        children[i].Draw();
                    }
                }
            }
        }
    }
}
