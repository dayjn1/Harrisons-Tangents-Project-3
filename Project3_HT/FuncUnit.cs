// ---------------------------------------------------------------------------
// File name:                   FuncUnit.cs
// Project name:                Project 3 - Harrison's Tangents
// Developers:                  Jason Middlebrook, Hannah Taylor
// Course-Section:              CSCI 4717-201
// Creation Date:               04/02/2022
// 
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    internal class FuncUnit
    {
        public bool Executed { get; private set; }
        public Queue<Instruction> Instructions { get; set; }
        public string Name { get; set; }

        public int ExecTime { get; set; }

        public FuncUnit(string name, int execTime)
        {
            Instructions = new Queue<Instruction>();
            Executed = false;
            Name = name;
            ExecTime = execTime;
        }

        /// <summary>
        /// Execution takes one cycle for each instruction
        /// </summary>
        public void Cycle()
        {

        }

        /// <summary>
        /// Send an instruction from reservation station or load buffer into functional unit
        /// </summary>
        public bool Enqueue(Instruction instr)
        {
            if(Instructions.Count > 0)
            {
                throw new InvalidOperationException("Functional units can only execute one Instruction per cycle." +
                    "\nPlease Dequeue the instruction or stall execution.");
                return false;
            }
            Instructions.Enqueue(instr);
            return true;
        }

        /// <summary>
        /// Send results to the common data bus
        /// </summary>
        public Instruction Dequeue()
        {
            return Instructions.Dequeue();
        }

        public override string ToString()
        {
            return Name + ": " + Instructions.Peek().Mnemonic;
        }

    }
}
