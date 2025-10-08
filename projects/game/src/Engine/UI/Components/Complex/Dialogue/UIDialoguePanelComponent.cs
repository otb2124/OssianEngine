using Entities;
using Microsoft.Xna.Framework;
using Resources;
using System;
using static Resources.StaticSpriteFactory;
using Utils;
using System.Collections.Generic;
using static Utils.TupleObjectsHelper;
using Microsoft.Xna.Framework.Graphics;

namespace UI
{


    public class UIDialoguePanelComponent : UIComponent
    {

        public static readonly int CHAR_PER_DIALOGUE_TEXTAREA_ROW = 100;

        public int FontId = 0;

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

            FrameSize = GetFrameSize();

            //float OverHeadYOffset = ScreenWorldMeasuresConverter.FlatBodyBoundsToScreen(new Vector2(0, Entities.Entities.EntityManager.GetEntityByEntityDialogueId(Dialogue.AuthorEntityId).Model.Body.Height)).Y;
            float OverHeadYOffset = 100;

            Vector2 FramePosition = new Vector2(EntityScreenPosition.X - FrameSize.X/2f, EntityScreenPosition.Y - FrameSize.Y/2f + OverHeadYOffset);

            children[0] = new UIFrameComponent(-1, FramePosition, FrameSize);

            children[1] = new UITextStringComponent(-1, new Vector2(FramePosition.X + 8, FramePosition.Y + FrameSize.Y - 20 -4), Dialogue.AuthorName, 0, Vector2.One, Color.DarkGray);
            children[2] = new UITextAreaComponent(-1, new Vector2(FramePosition.X + 8, FramePosition.Y + FrameSize.Y - 20 -20 -4), Dialogue.Text, 0, new Vector2(FrameSize.X, FrameSize.Y - 20));
        }

        public Vector2 GetFrameSize()
        {
            const float Padding = 4f; //padding on each side
            const float VerticalSpacing = 20f; //space between author name and text area

            SpriteFont font = ResourceLoader.fonts[FontId].GetCurrentFont();

            float charWidth = font.MeasureString("i").X;
            float lineHeight = font.LineSpacing;

            //calculate text area width based on CHAR_PER_DIALOGUE_TEXTAREA_ROW
            float textAreaWidth = charWidth * CHAR_PER_DIALOGUE_TEXTAREA_ROW;

            int textLength = Dialogue.Text?.Length ?? 0;
            int rows = (int)Math.Ceiling((float)textLength / CHAR_PER_DIALOGUE_TEXTAREA_ROW);
            rows = Math.Max(1, rows);

            float textAreaHeight = rows * lineHeight;

            float frameWidth = Math.Max(textAreaWidth, lineHeight) + 2 * Padding;

            float frameHeight = lineHeight + VerticalSpacing + textAreaHeight + 2 * Padding;

            return new Vector2(frameWidth, frameHeight);
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
