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
        public static List<Instruction> Passed = new List<Instruction>();

        public static void AddToReorderBuf(Instruction i)
        {
            ReorderBuf.Enqueue(i);
        }

        public static bool IsReorderBufFree()
        {
            if (ReorderBuf.Count > 5)
                return false;
            else
                return true;
        }

        public static Instruction RemoveFromReorderBuf()
        {
            Instruction temp = ReorderBuf.Peek();
            if(Passed.Contains(temp))
            {
                return ReorderBuf.Dequeue();
            }

            return null;
        }

        public static void PassedtoRB(Instruction i)
        {
            Passed.Add(i);
        }



        public static Instruction[] GetArray()
        {
            return ReorderBuf.ToArray();
        }
    }
}
