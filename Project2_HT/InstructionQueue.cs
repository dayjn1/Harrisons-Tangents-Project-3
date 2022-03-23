using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_HT
{
    public class InstructionQueue
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
