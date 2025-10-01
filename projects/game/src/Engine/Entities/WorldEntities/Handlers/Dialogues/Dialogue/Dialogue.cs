using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class Dialogue
    {
        public int Id;
        public string Text;
        public string AuthorName;

        public int AuthorEntityId;
        public Vector2 CameraPosition;

        public DialogueOption[] Options;

        public int TimesRead;

        public Dialogue(int id, string text, string authorName, int authorId)
        {
            Id = id;

            Text = text;
            AuthorName = authorName;

            //AuthorEntityId = authorId;
            //CameraPosition = Vector2.Zero; //get authorid pos

            TimesRead = 0;
        }

        public void SetOptions()
        {
            Options = Entities.DialogueManager.GetAllowedOptions(Id);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Id: {Id}, Text: {Text}, AuthorId: {AuthorEntityId}, AuthorName: {AuthorName}, CameraPos: {CameraPosition}\nOptions: ");

            if (Options == null || Options.Length == 0)
            {
                sb.Append("None");
            }
            else
            {
                for (int i = 0; i < Options.Length; i++)
                {
                    sb.Append($"\n  Option {i + 1}: {Options[i].ToString()}");
                }
            }

            return sb.ToString();
        }
    }
}
