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
using System.Text.RegularExpressions;
using System.Reflection;

namespace UI
{
    public class UITextStringComponent : UIComponent
    {

        public class UITextSeverity
        {
            public Color TextColor;

            public UITextSeverity(Color textColor)
            {
                TextColor = textColor;
            }

            public static UITextSeverity None { get; private set; }
            public static UITextSeverity Read { get; private set; }
            public static UITextSeverity Danger { get; private set; }
            public static UITextSeverity Mystery { get; private set; }


            static UITextSeverity()
            {
                None = new UITextSeverity(Color.Black);
                Read = new UITextSeverity(Color.Gray);
                Danger = new UITextSeverity(Color.OrangeRed);
                Mystery = new UITextSeverity(Color.Purple);
            }
        }


        private static readonly Dictionary<string, UITextSeverity> SeverityMap;

        static UITextStringComponent()
        {
            SeverityMap = new Dictionary<string, UITextSeverity>(StringComparer.OrdinalIgnoreCase);
            var properties = typeof(UITextSeverity).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var prop in properties)
            {
                if (prop.PropertyType == typeof(UITextSeverity))
                {
                    var severity = (UITextSeverity)prop.GetValue(null);
                    if (severity != null)
                    {
                        SeverityMap[prop.Name.ToLower()] = severity;
                    }
                }
            }
        }


        public UITextStringComponent(int id, Vector2 position, string text, int fontId, Vector2 scale, Color color) : base(id)
        {
            this.Text = text;
            this.Font = ResourceLoader.fonts[fontId];

            IsStickToCameraState = true;
            IsStickToZoomState = true;
            IsAppliedHalfScreenOriginState = true;

            Position = position;
            Scale = scale;
            Color = color;

            type = UIComponentTypes.TEXT;

            ParsePseudoHtml(text);
        }


        private void ParsePseudoHtml(string inputText)
        {
            Text = inputText;

            string pattern = @"<colored_severity=""([^""]+)"">\s*(.*?)</colored>";
            var matches = Regex.Matches(inputText, pattern, RegexOptions.Singleline);

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    string severity = match.Groups[1].Value.ToLower();
                    string taggedText = match.Groups[2].Value;

                    if (SeverityMap.ContainsKey(severity))
                    {
                        Color = SeverityMap[severity].TextColor;
                        Text = taggedText;
                        break;
                    }
                }

                if (Text == inputText)
                {
                    Text = Regex.Replace(inputText, @"<colored_severity=""[^""]+"">\s*(.*?)</colored>", "$1");
                }
            }
        }


        public override void Update()
        {
            //

            base.Update();

        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
