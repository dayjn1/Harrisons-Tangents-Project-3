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
    // 1) Invalid 404
    // 2) Halt    0  
    // 3) Floating point - everything > 128
    // 4) Int (3-20 included)
}
