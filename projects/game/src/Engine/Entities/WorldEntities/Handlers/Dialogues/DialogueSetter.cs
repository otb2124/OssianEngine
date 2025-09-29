using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class DialogueSetter
    {

        public static Dictionary<int, DialogueOption[]> AllDialogueOptions = new()
        {
            {
                0,
                new[]
                {
                    new DialogueOption(0, "Hi. Who are you?", 1),
                    new DialogueOption(1, "*Be Silent*", 1),
                    new DialogueOption(2, "I am little bit injured. Like bleeding a bit.", 4, new Requirement[] { new CurrentHPRequirement(50, 75) }),
                    new DialogueOption(3, "I am injured. Like bleeding.", 4, new Requirement[] { new CurrentHPRequirement(10, 50) }),
                }
            },
            {
                1,
                new[]
                {
                    new DialogueOption(0, "My name is...", 2),
                    new DialogueOption(1, "*Remain Silent*", 3, new Requirement[] { new DialogueAnswerRequirement(0, 1) }),
                }
            },
            {
                2,
                new[]
                {
                    new DialogueOption(0, "*Leave*"),
                }
            },
            {
                3,
                new[]
                {
                    new DialogueOption(0, "*Leave*"),
                }
            },
            {
                4,
                new[]
                {
                    new DialogueOption(0, "*Leave*"),
                }
            }
        };

        public static Dialogue[] AllDialogues = new Dialogue[]
        {
            new Dialogue(0, "Hi.", "Unknown Man", 0),
            new Dialogue(1, "My name is Vigo. But I think it's none of your business...", "Vigo", 0),
            new Dialogue(2, "Sorry, I do not wish to know the name of yours. Now leave me.", "Vigo", 0),
            new Dialogue(3, "Keep silent? Anyways...", "Vigo", 0),
            new Dialogue(4, "Yes, I see. Sorry, can't do anything about it. Leave me.", "Vigo", 0),
        };


        
    }
}
