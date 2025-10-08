using Entities;
using Microsoft.Xna.Framework;
using Resources;
using System;
using static Resources.StaticSpriteFactory;
using Utils;

namespace UI
{
    public class UIDialoguePanelComponent : UIComponent
    {

        public Dialogue Dialogue;

        public Vector2 EntityScreenPosition;

        public Vector2 FrameSize;

        public UIDialoguePanelComponent(int id, Dialogue dialogue) : base(id)
        {
            type = UIComponentTypes.DIALOGUE_PANEL;

            SetDialogue(dialogue);
        }

        public void SetDialogue(Dialogue dialogue)
        {
            Dialogue = dialogue;

            children = new UIComponent[3];

            if(Dialogue.AuthorEntityId != -1)
            {
                EntityScreenPosition = Utils.ScreenWorldMeasuresConverter.ToScreenPos(Entities.Entities.EntityManager.GetEntityByEntityDialogueId(Dialogue.AuthorEntityId).Model.Body.Position.ToVector2());
            }
            else
            {
                Position = new Vector2(20, 20);
            }

            FrameSize = new Vector2(300, 100);

            //float OverHeadYOffset = ScreenWorldMeasuresConverter.FlatBodyBoundsToScreen(new Vector2(0, Entities.Entities.EntityManager.GetEntityByEntityDialogueId(Dialogue.AuthorEntityId).Model.Body.Height)).Y;
            float OverHeadYOffset = 100;

            Vector2 FramePosition = new Vector2(EntityScreenPosition.X - FrameSize.X/2f, EntityScreenPosition.Y - FrameSize.Y/2f + OverHeadYOffset);

            children[0] = new UIFrameComponent(-1, FramePosition, FrameSize);

            children[1] = new UITextStringComponent(-1, new Vector2(FramePosition.X + 4, FramePosition.Y + FrameSize.Y - 20 - 4), Dialogue.AuthorName, 0, Vector2.One, Color.DarkGray);
            children[2] = new UITextStringComponent(-1, new Vector2(FramePosition.X + 4, FramePosition.Y + FrameSize.Y - 20 - 20), Dialogue.Text, 0, Vector2.One, Color.Black);
        }

        public override void Update()
        {
            EntityScreenPosition = Utils.ScreenWorldMeasuresConverter.ToScreenPos(Entities.Entities.EntityManager.GetEntityByEntityDialogueId(Dialogue.AuthorEntityId).Model.Body.Position.ToVector2());

            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    if (children[i] != null)
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
