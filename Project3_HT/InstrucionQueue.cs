using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    static class InstrucionQueue
    {

        public static Queue<Instruction> IQueue = new Queue<Instruction>();
        public static int LineNum = 0;
        public static void AddToIQueue(Instruction i)
        {
            LineNum++;
            i.lineNum = LineNum;
            IQueue.Enqueue(i);
        }


    }

    // check where to go
    // 1) Invalid 404
    // 2) Halt    0  
    // 3) Floating point - everything > 128
    // 4) Int (3-20 included)
}
