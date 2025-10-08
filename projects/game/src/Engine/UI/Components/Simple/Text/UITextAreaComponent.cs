using Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Resources;
using static UI.UIFramePartComponent;
using Microsoft.Xna.Framework.Graphics;

namespace UI
{
    public class UITextAreaComponent : UIComponent
    {
        public int FontId;
        public Vector2 AreaSize;

        public UITextAreaComponent(int id, Vector2 position, string text, int fontId, Vector2 areaSize) : base(id)
        {
            Text = text;
            FontId = fontId;
            Font = ResourceLoader.fonts[FontId];

            IsStickToCameraState = true;
            IsStickToZoomState = true;
            IsAppliedHalfScreenOriginState = true;

            Position = position;
            Scale = Vector2.One;
            Color = Color.Black;

            AreaSize = areaSize;

            type = UIComponentTypes.TEXT_AREA;

            CalculateWordComponents();
        }

        private void CalculateWordComponents()
        {
            var tempChildren = new List<UIComponent>();

            SpriteFont spriteFont = Font.GetCurrentFont();

            float scaledLineHeight = spriteFont.LineSpacing * Scale.Y;
            int maxRows = (int)Math.Floor(AreaSize.Y / scaledLineHeight);
            maxRows = Math.Max(1, maxRows);

            float spaceWidth = spriteFont.MeasureString(" ").X * Scale.X;

            if (string.IsNullOrEmpty(Text))
            {
                tempChildren.Add(new UITextStringComponent(-1, Position, "", FontId, Scale, Color));
            }
            else
            {
                var wordsAndTags = SplitWordsAndTags(Text);
                float currentX = Position.X;
                float currentY = Position.Y;
                int currentRow = 0;

                foreach (var item in wordsAndTags)
                {
                    string displayWord = item.Text;
                    string measureText = item.IsTag ? item.InnerText : item.Text;

                    float wordWidth = spriteFont.MeasureString(measureText).X * Scale.X;

                    if (wordWidth > AreaSize.X)
                    {
                        while (spriteFont.MeasureString(displayWord).X * Scale.X > AreaSize.X && displayWord.Length > 0)
                        {
                            displayWord = displayWord.Substring(0, displayWord.Length - 1);
                            if (item.IsTag)
                            {
                                measureText = displayWord.StartsWith("<colored_severity=") && displayWord.EndsWith("</colored>")
                                    ? Regex.Match(displayWord, @"<colored_severity=""[^""]+"">(.*?)</colored>").Groups[1].Value
                                    : measureText.Substring(0, measureText.Length - 1);
                            }
                            else
                            {
                                measureText = displayWord;
                            }
                        }

                        wordWidth = spriteFont.MeasureString(displayWord).X * Scale.X;
                    }

                    if (currentX + wordWidth <= Position.X + AreaSize.X)
                    {
                        tempChildren.Add(new UITextStringComponent(-1, new Vector2(currentX, currentY), displayWord, FontId, Scale, Color));
                        currentX += wordWidth + spaceWidth;
                    }
                    else
                    {
                        currentRow++;
                        if (currentRow >= maxRows)
                            break;

                        currentX = Position.X;
                        currentY -= scaledLineHeight;
                        tempChildren.Add(new UITextStringComponent(-1, new Vector2(currentX, currentY), displayWord, FontId, Scale, Color));
                        currentX += wordWidth + (item.IsTag ? spriteFont.MeasureString(measureText).X * Scale.X : spaceWidth);
                    }
                }
            }

            children = tempChildren.ToArray();

            for (int i = 0; i < children.Length; i++)
            {
                var child = (UITextStringComponent)children[i];
            }
        }

        private List<(string Text, string InnerText, bool IsTag)> SplitWordsAndTags(string input)
        {
            var result = new List<(string Text, string InnerText, bool IsTag)>();
            if (string.IsNullOrEmpty(input))
                return result;

            string pattern = @"<colored_severity=""[^""]+"">.*?</colored>";
            var regex = new Regex(pattern);
            int lastIndex = 0;

            foreach (Match match in regex.Matches(input))
            {
                if (match.Index > lastIndex)
                {
                    string beforeText = input.Substring(lastIndex, match.Index - lastIndex);
                    string[] words = beforeText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in words)
                    {
                        if (!string.IsNullOrEmpty(word))
                            result.Add((word, word, false));
                    }
                }

                string fullTag = match.Value;

                string innerText = Regex.Match(fullTag, @"<colored_severity=""[^""]+"">(.*?)</colored>").Groups[1].Value;
                result.Add((fullTag, innerText, true));
                lastIndex = match.Index + match.Length;
            }

            if (lastIndex < input.Length)
            {
                string afterText = input.Substring(lastIndex);
                string[] words = afterText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                        result.Add((word, word, false));
                }
            }

            return result;
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