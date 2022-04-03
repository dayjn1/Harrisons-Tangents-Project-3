// ---------------------------------------------------------------------------
// File name:                   FuncUnit.cs
// Project name:                Project 2 - Harrison's Tangents
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

        public FuncUnit(string name)
        {
            Instructions = new Queue<Instruction>();
            Executed = false;
            Name = name;
        }

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

        public Instruction Dequeue()
        {
            return Instructions.Dequeue();
        }

        public override string ToString()
        {
            return Instructions.Peek().ToString();
        }

    }
}
