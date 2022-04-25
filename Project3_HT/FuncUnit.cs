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
        public bool Processed { get; set; }
        public Queue<Instruction> Instructions { get; set; }
        public string Name { get; set; }

        public int ExecTime { get; set; }

        public FuncUnit(string name)
        {
            Instructions = new Queue<Instruction>();
            Executed = false;
            Name = name;
            Empty = true;
            Processed = false;
        }        

        /// <summary>
        /// Send an instruction from reservation station or load buffer into functional unit
        /// </summary>
        public void Enqueue(Instruction instr)
        {
            if(Instructions.Count > 0)
            {
                throw new InvalidOperationException("Functional units can only execute one Instruction per cycle." +
                    "\nPlease Dequeue the instruction or stall execution.");
                //return false;
            }
            Instructions.Enqueue(instr);
            ExecTime = CalcExecutionTime(instr);
            Empty = false;
            Executed = false;
            Processed = false;
            //return true;
        }

        /// <summary>
        /// Send results to the common data bus
        /// </summary>
        public Instruction Dequeue()
        {
            Executed = false;
            Empty = true;
            Processed = false;
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
