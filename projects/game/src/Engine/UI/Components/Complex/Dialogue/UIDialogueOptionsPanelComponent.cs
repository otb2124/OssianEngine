using Entities;
using Microsoft.Xna.Framework;
using Resources;
using System;
using static Resources.StaticSpriteFactory;
using Utils;

namespace UI
{
    public class UIDialogueOptionsPanelComponent : UIComponent
    {

        public Dialogue Dialogue;

        public UIDialogueOptionsPanelComponent(int id, Dialogue dialogue) : base(id)
        {
            type = UIComponentTypes.DIALOGUE_OPTIONS_PANEL;

            SetDialogueOptions(dialogue);
        }

        public void SetDialogueOptions(Dialogue dialogue)
        {
            Dialogue = dialogue;

            children = new UIComponent[Dialogue.GetAllowedOptions().Length];

            Position = new Vector2(20, 20);

            if (Dialogue.IsOnlyContinueDialogue)
            {
                children[0] = new UIButtonIconComponent(-1, -1, new Vector2(200, 20), new SpriteData(SpriteSheets.UI_ICONS, new Rectangle(0, 128 + 32, 64, 32), 0), Vector2.One);
            }
            else
            {
                for (int i = 0; i < children.Length; i++)
                {
                    children[i] = new UIButtonTextComponent(-1, -1, new Vector2(40, 120 - ((i + 1) * 20)), (i + 1) + ") " + Dialogue.GetAllowedOptions()[i].Text, 0, Vector2.One, GetDialogueOptionColor(Dialogue.GetAllowedOptions()[i]));
                }
            }
        }

        public Color GetDialogueOptionColor(DialogueOption option)
        {
            if (option.TimesUsed >= 1)
            {

                /*
                DialogueOption[] nextOptions = Entities.Entities.DialogueManager.GetAllowedOptionsForDialogue(option.NextDialogueId);

                if(nextOptions != null)
                {
                    foreach (DialogueOption nextOption in nextOptions)
                    {
                        if (nextOption.TimesUsed >= 1)
                        {
                            return UITextSeverity.Read.TextColor;
                        }
                    }
                }
                */

                return UITextStringComponent.UITextSeverity.Read.TextColor;
            }

            return UITextStringComponent.UITextSeverity.None.TextColor;
        }


        public override void Update()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    if (children[i] != null)
                    {
                        children[i].Update();
                    }
                }

                for (int i = 0; i < children.Length; i++)
                {
                    if (children[0] is UIButtonIconComponent optionOnlyContinue)
                    {
                        if (optionOnlyContinue.IsOnClick)
                        {
                            Entities.Entities.DialogueManager.SetAnswer(Dialogue.GetAllowedOptions()[0].Id);
                            Entities.Entities.DialogueManager.SetDialogue(Dialogue.GetAllowedOptions()[0].Actions);
                        }
                    }

                    if (children[i] is UIButtonTextComponent option)
                    {
                        if (option.children[option.ButtonChildId] is UIButtonComponent optionButton)
                        {
                            if (optionButton.IsOnClick)
                            {
                                Entities.Entities.DialogueManager.SetAnswer(Dialogue.GetAllowedOptions()[i].Id);
                                Entities.Entities.DialogueManager.SetDialogue(Dialogue.GetAllowedOptions()[i].Actions);
                            }
                        }
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
