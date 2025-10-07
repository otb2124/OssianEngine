using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utils.TupleObjectsHelper;

namespace Entities
{
    public class DialogueAnswer
    {

        public IntPair Data;

        public DialogueAnswer(int dialogueId, int optionId) 
        {
            Data = new IntPair(dialogueId, optionId);
        }

        public override string ToString()
        {
            return $"DialogueId: {Data.Item1}, OptionId: {Data.Item2}";
        }
    }
}
