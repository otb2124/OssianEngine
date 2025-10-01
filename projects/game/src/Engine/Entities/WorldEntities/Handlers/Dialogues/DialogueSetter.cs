using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utils.TupleObjectsHelper;

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
                    new DialogueOption(2, "I am little bit injured. Like bleeding a bit.", 4, -1, new Requirement[] { new CurrentHPRequirement(50, 75) }) { UseOnlyOnce = true },
                    new DialogueOption(3, "I am injured. Like bleeding.", 4, -1, new Requirement[] { new CurrentHPRequirement(10, 50) }) { UseOnlyOnce = true },
                }
            },
            {
                1,
                new[]
                {
                    new DialogueOption(0, "My name is...", 2),
                    new DialogueOption(1, "*Remain Silent*", 3, -1, new Requirement[] { new DialogueAnswerRequirement(0, 1) })
                }
            },
            {
                2,
                new[]
                {
                    new DialogueOption(0, "*Continue*", 5),
                }
            },
            {
                3,
                new[]
                {
                    new DialogueOption(0, "*Continue*", 5),
                }
            },
            {
                4,
                new[]
                {
                    new DialogueOption(0, "*Continue*", 5),
                    new DialogueOption(1, "I have a terrablade here", -1, -1, new Requirement[]{ new CurrentInventoryItemKeyRequirement(new ItemKey(ItemLib.Weapons.TERRABLADE)) }),
                }
            },
            {
                5,
                new[]
                {
                    new DialogueOption(0, new IntPair(0, 0)),
                    new DialogueOption(1, new IntPair(0, 1)),
                    new DialogueOption(2, new IntPair(0, 2)),
                    new DialogueOption(3, new IntPair(0, 3)),
                    new DialogueOption(4, "*Leave*"),
                }
            },
        };

        public static Dictionary<int, Dialogue[]> AllDialogues = new()
        {
            {
                0,
                new Dialogue[]
                {
                    new Dialogue(0, "Hi.", "Unknown Man", 0),
                    new Dialogue(1, "My name is Vigo. But I think it's none of your business...", "Vigo", 0),
                    new Dialogue(2, "Sorry, I do not wish to know the name of yours. Now leave me.", "Vigo", 0),
                    new Dialogue(3, "Keep silent? Anyways...", "Vigo", 0),
                    new Dialogue(4, "Yes, I see. Sorry, can't do anything about it. Leave me.", "Vigo", 0),
                    new Dialogue(5, "Anything I can help you more with?", "Vigo", 0)
                }
            }
            
        };


        public static DialogueSequence[] AllSequences = new DialogueSequence[]
        {
            new DialogueSequence(0),
            new DialogueSequence(1),
            new DialogueSequence(2),
            new DialogueSequence(3),
        };
    }
}
