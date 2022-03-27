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
        int linepos = 0;
        public Queue<IQueueEntry> IQueue = new Queue<IQueueEntry>();
        public void AddToIQueue(Instruction i)
        {

            IQueueEntry myEntry = new IQueueEntry(i, linepos);
            linepos++;
        }
    }

    public struct IQueueEntry
    {
        public Instruction i;
        public int linepos;

        public IQueueEntry(Instruction i, int linepos)
        {
            this.i = i;
            this.linepos = linepos;
        }
    }
}
