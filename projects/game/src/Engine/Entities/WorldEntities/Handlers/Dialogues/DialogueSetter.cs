using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static Utils.TupleObjectsHelper;

namespace Entities
{
    public static class DialogueSetter
    {

        public static DialogueSequence[] Sequences = new DialogueSequence[]
        {


                //vigo
                new DialogueSequence(0, 0, 0,
                    new Dialogue[]
                    {
                        new Dialogue(0, "Hey", "Unknown Man", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(0, "Hey.",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(1)
                                    }
                                ),
                                new DialogueOption(1, "*Leave*",
                                    new DialogueOptionAction[]
                                    {
                                        new ExitDialogueDOP()
                                    }
                                ),
                            }
                        ),
                        new Dialogue(1, "Name`s <colored_severity=\"read\">Vigo</colored>. Happen to care about keeping things <colored_severity=\"danger\">secret?</colored>", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(2, "*Continue*",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(2)
                                    }
                                ),
                            }
                        ),
                        new Dialogue(2, "I have a thing that I would fancy to share somebody with. Do you happen to be that `somebody` by the chance? Hmm... Let me take a closer look on you...", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(3, "*Continue*",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(3, 
                                            new Requirement[]
                                            {
                                                new CurrentHPRequirement(50, 100)
                                            }
                                        ),
                                        new NextDialogueDOP(4,
                                            new Requirement[]
                                            {
                                                new CurrentHPRequirement(0, 50)
                                            }
                                        )
                                    }
                                ),
                            }
                        ),
                        new Dialogue(3, "Hmm... You do have a  posture of a handsome gentleman that may be the one I seek for.", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(4, "*Continue*",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(5)
                                    }
                                ),
                            }
                        ),
                        new Dialogue(4, "Hmm... You seem to bleed a bit... Try healing your wounds any further rather than walking and spilling your blood over ground like that. But anyways...", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(5, "*Continue*",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(5)
                                    }
                                ),
                            }
                        ),
                        new Dialogue(5, "Recently I have received a good omen from the skies. Once Ive been looking at the stars and at one moment I recognized that they started moving", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(6, "*Continue*",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(6)
                                    }
                                ),
                            }
                        ),
                        new Dialogue(6, "They combined into letters and later words right like it sounds. After few seconds gazing into that wonderful process I got their extraterrestrial sign addressed to me: `FIND WANEGRO`.", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(7, "*Continue*",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(7)
                                    }
                                ),
                            }
                        ),
                        new Dialogue(7, "Not a single idea about what that is supposed to mean though.", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(8, "*Continue*",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(8)
                                    }
                                ),
                            }
                        ),
                        new Dialogue(8, "Mind giving me a hand in resolving that mystery?", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(9, "Yes, I will lend you a hand with that.",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(9)
                                    }
                                ),
                                new DialogueOption(10, "Sorry but I have more stuff to work around with rather than going into your `mysteries`",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(10)
                                    }
                                ),
                            }
                        ),
                        new Dialogue(9, "Perfect! Look after a somebody or something named Wanegro. They should be able to help us proceed further with this message...", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(11, "*Leave*",
                                    new DialogueOptionAction[]
                                    {
                                        new SetInitialDialogueForSequenceDOP(11, 0),
                                        new ExitDialogueDOP()
                                    }
                                ),
                            }
                        ),
                        new Dialogue(10, "No? Hmm... Maybe come later when you would be more courageous about this...", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(12, "*Leave*",
                                    new DialogueOptionAction[]
                                    {
                                        new SetInitialDialogueForSequenceDOP(12, 0),
                                        new ExitDialogueDOP()
                                    }
                                ),
                            }
                        ),
                        new Dialogue(11, "Forgot about our mission? Go and find the `Wanegro`, whatever that means.", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(13, "*Leave*",
                                    new DialogueOptionAction[]
                                    {
                                        new ExitDialogueDOP()
                                    }
                                ),
                            }
                        ),
                        new Dialogue(12, "What? Got interested in this yet?", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(14, "What was that again?",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(5),
                                    }
                                ),
                                new DialogueOption(15, "Yes.",
                                    new DialogueOptionAction[]
                                    {
                                        new NextDialogueDOP(9),
                                    }
                                ),
                                new DialogueOption(16, "No. *Leave*",
                                    new DialogueOptionAction[]
                                    {
                                        new ExitDialogueDOP(),
                                    }
                                ),
                            }
                        )
                    }
                ),

                new DialogueSequence(1, 13, 1,
                    new Dialogue[]
                    {
                        new Dialogue(13, "Sequence 1", "Vigo", 0,
                            new DialogueOption[]
                            {
                                new DialogueOption(17, "*Leave*",
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
                        new CurrentInventoryItemKeyRequirement(new EquatableKey(ItemLib.Weapons.TERRABLADE))
                    }
                ),



                //wangegro
                new DialogueSequence(100, 100, 0,
                    new Dialogue[]
                    {
                        new Dialogue(100, "...", "Unknown Man", 1,
                            new DialogueOption[]
                            {
                                new DialogueOption(100, "*Leave*",
                                    new DialogueOptionAction[]
                                    {
                                        new ExitDialogueDOP()
                                    }
                                ),
                                new DialogueOption(101, "Happen to know Wanegro?",
                                    new DialogueOptionAction[]
                                    {
                                        new ExitDialogueDOP()
                                    },
                                    new Requirement[]
                                    {
                                        new OrRequirement
                                        (
                                            new Requirement[]
                                            {
                                                new DialogueAnswerRequirement(8, 9),
                                                new DialogueAnswerRequirement(12, 15)
                                            }
                                        )
                                    }
                                ),
                            }
                        )
                    }
                ),
        };
    }
}
