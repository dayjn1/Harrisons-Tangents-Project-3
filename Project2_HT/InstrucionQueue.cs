// ---------------------------------------------------------------------------
// File name:                   InstrucionQueue.cs
// Project name:                Project 3 - Harrison's Tangents
// ---------------------------------------------------------------------------
// Creator’s name:              Avery Marlow
// Edited By:                   Avery Marlow
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
    class InstrucionQueue
    {

        public Queue<Instruction> IQueue = new Queue<Instruction>();
        public void AddToIQueue(Instruction i)
        {
            IQueue.Enqueue(i);
        }
    }

    // check where to go
    // 1) Invalid 
    // 2) Halt
    // Other
    //  
}
