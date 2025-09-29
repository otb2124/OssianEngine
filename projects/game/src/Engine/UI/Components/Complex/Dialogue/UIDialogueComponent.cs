using Entities;
using Microsoft.Xna.Framework;
using System;

namespace UI
{
    public class UIDialogueComponent : UIComponent
    {

        public Dialogue Dialogue;

        public UIDialogueComponent(int id, Dialogue dialogue) : base(id)
        {
            type = UIComponentTypes.DIALOGUE;

            SetDialogue(dialogue);
        }

        public void SetDialogue(Dialogue dialogue)
        {
            Dialogue = dialogue;

            int childrenCount = 3;
            for (int i = 0; i < Dialogue.Options.Length; i++)
            {
                childrenCount++;
            }

            children = new UIComponent[childrenCount];

            Position = new Vector2(20, 20);

            children[0] = new UIFrameComponent(-1, Position, new Vector2(1226, 200));

            children[1] = new UITextStringComponent(-1, new Vector2(40, 180), Dialogue.AuthorName, 0, Vector2.One, Color.DarkGray);
            children[2] = new UITextStringComponent(-1, new Vector2(40, 160), Dialogue.Text, 0, Vector2.One, Color.Black);
            

            for (int i = 3; i < children.Length; i++)
            {
                children[i] = new UIButtonTextComponent(-1, -1, new Vector2(40, 120 - ((i - 3 + 1) * 20)), (i-3 + 1) + ") " + Dialogue.Options[i-3].Text, 0, Vector2.One, Color.Black);
            }
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

                for (int i = 3; i < Dialogue.Options.Length + 3; i++)
                {
                    if (children[i] is UIButtonTextComponent option)
                    {
                        if (option.children[option.ButtonChildId] is UIButtonComponent optionButton)
                        {
                            if (optionButton.IsOnClick)
                            {
                                Entities.Entities.DialogueManager.SetAnswer(Dialogue.Options[i - 3].Id);
                                Entities.Entities.DialogueManager.StartDialogue(Dialogue.Options[i - 3].NextDialogueId);
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
