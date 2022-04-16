using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    internal class MemUnit : FuncUnit
    {

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

            Empty = false;
        }

    }
}
