﻿// ---------------------------------------------------------------------------
// File name:                   InstrucionQueue.cs
// Project name:                Project 3 - Harrison's Tangents
// ---------------------------------------------------------------------------
// Creator’s name:              Avery Marlow
// Edited By:                   Avery Marlow, Nataliya Chibizova
// Course-Section:              CSCI-4717
// Creation Date:               03/25/2022
// ---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    class InstructionQueue
    {

        public Queue<Instruction> IQueue = new Queue<Instruction>();
        public void AddToIQueue(Instruction i)
        {
            IQueue.Enqueue(i);
        }

        public void DecueueTheInstruction(Instruction i)
        {
            if (i.OpCode == 404) // invelid instruction, stop execution 
            {
                IQueue.Dequeue();
            }
            else if(i.OpCode == 0) // HALT do not decueue anything after
            {
                //call our halt method, wherever that is
            }
            else if(i.OpCode == 1) // LOAD -- send it to the address unit --> LOAD buffer
            {
                AddressUnit.ProcessAU(i); // go to address unit
                IQueue.Dequeue();
            }
            else if (i.OpCode == 2) // STORE -- send it to the address unit --> RO buffer --> memory unit
            {
                AddressUnit.ProcessAU(i); //go to address unit
                IQueue.Dequeue();
            }
            else if(i.OpCode >=3 || i.OpCode <= 20) // goes to the int 
            {
                //go to op bus/res station
            }
            else if(i.OpCode >= 128 ||i.OpCode <= 131) // goes to floating point
            {
                //go to op bus// fp res station
            }
            else if(i.OpCode == 132 || i.OpCode == 133)
            {
                //go to op bus// fp res station
            }
            IQueue.Dequeue();
        }


    }

   
}
