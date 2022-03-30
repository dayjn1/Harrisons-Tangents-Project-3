using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    class ReorderBuffer
    {
        public Queue<Instruction> ReorderBuf = new Queue<Instruction>();
        
        public void AddToReorderBuf(Instruction i)
        {
            ReorderBuf.Enqueue(i);
        }
    }
}
