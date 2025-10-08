using Graphics;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static UI.UIFramePartComponent;
using Resources;
using SharpDX.DirectWrite;

namespace UI
{
    public class UITextAreaComponent : UIComponent
    {

        public Vector2 AreaSize;
        public List<string> TextRows;


        public UITextAreaComponent(int id, Vector2 position, string text, int fontId, Vector2 areaSize) : base(id)
        {
            Text = text;
            Font = ResourceLoader.fonts[fontId];

            IsStickToCameraState = true;
            IsStickToZoomState = true;
            IsAppliedHalfScreenOriginState = true;

            Position = position;
            Scale = Vector2.One;
            Color = Color.Black;

            AreaSize = areaSize;

            type = UIComponentTypes.TEXT_AREA;

            CalculateTextRows();

            children = new UIComponent[TextRows.Count];

            for (int i = 0; i < children.Length; i++)
            {
                children[i] = new UITextStringComponent(-1, new Vector2(Position.X, Position.Y - 20 * i), TextRows[i], fontId, Scale, Color);
            }
        }

        private void CalculateTextRows()
        {
            TextRows = new List<string>();

            Vector2 charSize = Font.GetCurrentFont().MeasureString("i");
            float scaledCharWidth = charSize.X * Scale.X;
            float scaledLineHeight = Font.GetCurrentFont().LineSpacing * Scale.Y;

            int maxCols = (int)Math.Floor(AreaSize.X / scaledCharWidth);
            maxCols = Math.Max(1, maxCols);

            int maxRows = (int)Math.Floor(AreaSize.Y / scaledLineHeight);
            maxRows = Math.Max(1, maxRows);

            if (string.IsNullOrEmpty(Text))
            {
                TextRows.Add("");
                Console.WriteLine("TextRows: [empty]");
                return;
            }

            string[] words = Text.Split(new[] { ' ' }, StringSplitOptions.None);
            StringBuilder currentLine = new StringBuilder();
            float currentLineWidth = 0f;

            foreach (string word in words)
            {
                string testText = currentLine.Length > 0 ? currentLine.ToString() + " " + word : word;
                float testWidth = Font.GetCurrentFont().MeasureString(testText).X * Scale.X;

                if (testWidth <= AreaSize.X && currentLine.Length + word.Length + (currentLine.Length > 0 ? 1 : 0) <= maxCols)
                {
                    if (currentLine.Length > 0)
                        currentLine.Append(" ");
                    currentLine.Append(word);
                    currentLineWidth = testWidth;
                }
                else
                {
                    if (currentLine.Length > 0)
                    {
                        TextRows.Add(currentLine.ToString());
                        currentLine.Clear();
                    }

                    if (Font.GetCurrentFont().MeasureString(word).X * Scale.X > AreaSize.X)
                    {
                        string truncatedWord = word;
                        while (Font.GetCurrentFont().MeasureString(truncatedWord).X * Scale.X > AreaSize.X && truncatedWord.Length > 0)
                        {
                            truncatedWord = truncatedWord.Substring(0, truncatedWord.Length - 1);
                        }
                        TextRows.Add(truncatedWord);
                    }
                    else
                    {
                        currentLine.Append(word);
                        currentLineWidth = Font.GetCurrentFont().MeasureString(word).X * Scale.X;
                    }
                }

                if (TextRows.Count >= maxRows)
                    break;
            }

            if (currentLine.Length > 0 && TextRows.Count < maxRows)
            {
                TextRows.Add(currentLine.ToString());
            }
        }


        public override void Update()
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] != null)
                {
                    children[i].Update();
                }
            }
        }

        public override void Draw()
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
