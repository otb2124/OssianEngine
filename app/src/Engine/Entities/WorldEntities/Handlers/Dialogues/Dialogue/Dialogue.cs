using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.Design.AxImporter;

namespace Entities
{
    public class Dialogue
    {
        public int Id;
        public string Text;
        public string AuthorName;

        public int AuthorEntityId;
        public Vector2 DialogueScreenPos;

        public DialogueOption[] Options;

        public Requirement[] Requirements;

        public int TimesRead;

        public bool IsOnlyContinueDialogue;

        public Dialogue(int id, string text, string authorName, int authorId, DialogueOption[] options, Requirement[] requirements = null)
        {
            AuthorEntityId = authorId;
            Init(id, text, authorName, options, requirements);
        }

        public Dialogue(int id, string text, string authorName, Vector2 dialogueScreenPos, DialogueOption[] options, Requirement[] requirements = null)
        {
            DialogueScreenPos = dialogueScreenPos;
            Init(id, text, authorName, options, requirements);
        }

        public void Init(int id, string text, string authorName, DialogueOption[] options, Requirement[] requirements)
        {
            Id = id;

            Text = text;
            AuthorName = authorName;

            TimesRead = 0;

            Options = options;

            Requirements = requirements;

            IsOnlyContinueDialogue = false;
        }

        public DialogueOption[] GetAllowedOptions()
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

            if (allowedOptions.Count == 1)
            {
                if (allowedOptions[0].Text == "*Continue*")
                {
                    IsOnlyContinueDialogue = true;
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
            sb.Append($"Id: {Id}, Text: {Text}, AuthorId: {AuthorEntityId}, AuthorName: {AuthorName}, DialogueScreenPos: {DialogueScreenPos}\nOptions: ");

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
