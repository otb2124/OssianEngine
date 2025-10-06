using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text;

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
        public DialogueOption[] CurrentOptions;

        public int TimesRead;


        public Dialogue(int id, string text, string authorName, int authorId, DialogueOption[] options)
        {
            Id = id;

            Text = text;
            AuthorName = authorName;

            //AuthorEntityId = authorId;
            //CameraPosition = Vector2.Zero; //get authorid pos

            TimesRead = 0;

            Options = options;
            CurrentOptions = new DialogueOption[Options.Length];
        }

        public void SetCurrentOptions()
        {
            CurrentOptions = GetCurrentOptions();
        }

        public DialogueOption[] GetCurrentOptions()
        {
            List<DialogueOption> allowedOptions = new List<DialogueOption>();

            foreach (DialogueOption option in Options)
            {
                bool passedChecks = DialogueManager.IsOptionMeetsRequirements(option) && DialogueManager.IsOptionOneTimeUsed(option) && DialogueManager.IsOptionPassedCopyDependency(option);

                if (passedChecks)
                {
                    allowedOptions.Add(option);
                }
            }

            return allowedOptions.ToArray();
        }

        public DialogueOption GetOptionById(int id)
        {
            foreach (DialogueOption option in Options)
            {
                if (option.Id == id)
                {
                    return option;
                }
            }

            return null;
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
