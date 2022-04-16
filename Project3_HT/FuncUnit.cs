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
        public bool Executed { get; set; }
        public bool Empty;
        public Queue<Instruction> Instructions { get; set; }
        public string Name { get; set; }

        public int ExecTime { get; set; }

        public FuncUnit(string name)
        {
            Instructions = new Queue<Instruction>();
            Executed = false;
            Name = name;
            Empty = true;
        }        

        /// <summary>
        /// Send an instruction from reservation station or load buffer into functional unit
        /// </summary>
        public virtual void Enqueue(Instruction instr)
        {
            if(Instructions.Count > 0)
            {
                throw new InvalidOperationException("Functional units can only execute one Instruction per cycle." +
                    "\nPlease Dequeue the instruction or stall execution.");
            }
            Instructions.Enqueue(instr);
            ExecTime = CalcExecutionTime(instr);
            Empty = false;
        }

        /// <summary>
        /// Send results to the common data bus
        /// </summary>
        public Instruction Dequeue()
        {
            Executed = false;
            Empty = true;
            return Instructions.Dequeue();
        }

        public override string ToString()
        {
            return Name + ": " + Instructions.Peek().Mnemonic;
        }

        public int CalcExecutionTime(Instruction inst)
        {
            if (inst.MemoryCC > 0)
                return inst.MemoryCC;
            
            return inst.ExecuteCC;
        }

    }
}
