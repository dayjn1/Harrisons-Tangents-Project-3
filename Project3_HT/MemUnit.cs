using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    internal class MemUnit : FuncUnit
    {
        public int Set { get; set; }                            //Tag used by cache for memory partitioning
        public int Tag { get; set; }                            //Tag used by cache for memory accesses

        public MemUnit(string name) : base(name)
        {
        }

        public override void Enqueue(Instruction instr)
        {
            if (Instructions.Count > 0)
            {
                throw new InvalidOperationException("Functional units can only execute one Instruction per cycle." +
                    "\nPlease Dequeue the instruction or stall execution.");
            }
            Instructions.Enqueue(instr);
            ExecTime = CalcExecutionTime(instr);

            Set = CalcSet(instr);
            Tag = CalcTag(instr);

            Empty = false;
        }

        public int CalcSet(Instruction instr)
        {
            throw new NotImplementedException();
        }
        public int CalcTag(Instruction instr)
        {
            throw new NotImplementedException();
        }

    }
}
