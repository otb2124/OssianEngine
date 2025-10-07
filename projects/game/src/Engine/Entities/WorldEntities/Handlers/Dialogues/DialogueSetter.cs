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

        public static DialogueSequence[] Sequences = new DialogueSequence[]
        {
                new DialogueSequence(0, 0, 0,
                    new Dialogue[]
                    {
                        new Dialogue(0, "Huh?", "Unknown Man", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(0, "*Leave*",
                                    new DialogueOptionAction[]
                                    {
                                        new ExitDialogueDOP()
                                    }
                                ),
                            }
                        )
                    }
                ),
                new DialogueSequence(1, 1, 1,
                    new Dialogue[]
                    {
                        new Dialogue(1, "Hi.", "Unknown Man", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(1, "Hi. Who are you?",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(2)
                                    }
                                ),
                                new DialogueOption(2, "*Be Silent*",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(2)
                                    }
                                ),
                                new DialogueOption(3, "I am little bit injured. Like bleeding a bit.",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(5)
                                    },
                                    new Requirement[]
                                    {
                                        new CurrentHPRequirement(50, 75)
                                    }
                                ) { UseOnlyOnce = true },
                                new DialogueOption(4, "I am injured. Like bleeding.",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(5)
                                    },
                                    new Requirement[]
                                    {
                                        new CurrentHPRequirement(10, 50)
                                    }
                                ) { UseOnlyOnce = true },
                            }
                        ),
                        new Dialogue(2, "My name is Vigo. But I think it's none of your business...", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(5, "My name is...",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(3)
                                    }
                                 ),
                                new DialogueOption(6, "*Remain Silent*",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(4)
                                    },
                                    new Requirement[]
                                    {
                                        new DialogueAnswerRequirement(1, 2)
                                    })
                            }
                        ),
                        new Dialogue(3, "Sorry, I do not wish to know the name of yours. Now leave me.", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(7, "*Continue*",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(6),
                                        new SetInitialDialogueForSequenceDOP(6, 0)
                                    }
                                ),
                            }
                        ),
                        new Dialogue(4, "Keep silent? Anyways...", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(8, "*Continue*",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(6),
                                        new SetInitialDialogueForSequenceDOP(6, 0)
                                    }
                                ),
                            }
                        ),
                        new Dialogue(5, "Yes, I see. Sorry, can't do anything about it. Leave me.", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(9, "*Continue*",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(6),
                                        new SetInitialDialogueForSequenceDOP(6, 0)
                                    }
                                ),
                                new DialogueOption(10, "I have a terrablade here",
                                    new DialogueOptionAction[]
                                    {
                                        new ExitDialogueDOP()
                                    },
                                    new Requirement[]
                                    {
                                        new CurrentInventoryItemKeyRequirement(new ItemKey(ItemLib.Weapons.TERRABLADE))
                                    }
                                ),
                            }
                        ),
                        new Dialogue(6, "Anything I can help you more with?", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(11, new IntPair(1, 1)),
                                new DialogueOption(12, new IntPair(1, 2)),
                                new DialogueOption(13, new IntPair(1, 3)),
                                new DialogueOption(14, new IntPair(1, 4)),
                                new DialogueOption(15, "*Leave*",
                                    new DialogueOptionAction[]
                                    {
                                        new ExitDialogueDOP()
                                    }
                                ),
                            }
                        )
                    },
                    new Requirement[]
                    {
                        new DialogueOptionTimesUsedRequirement(0, 0, 0)
                    }
                )
        };
    }
}
