using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
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
        static UITextSeverity()
        {
            None = new UITextSeverity(Color.Black);
            Read = new UITextSeverity(Color.Gray);
        }
    }

    public static class UITextSeverityService
    {

    }
}
