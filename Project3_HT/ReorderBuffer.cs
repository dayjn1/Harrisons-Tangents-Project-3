using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    static class ReorderBuffer
    {
        public static Queue<Instruction> ReorderBuf = new Queue<Instruction>();
        
        public static void AddToReorderBuf(Instruction i)
        {
            ReorderBuf.Enqueue(i);
        }

        public static Instruction[] GetArray()
        {
            return ReorderBuf.ToArray();
        }
    }
}
