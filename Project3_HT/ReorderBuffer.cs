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

        /// <summary>
        /// Will be called by the CDB when data is available -JMid
        /// </summary>
        /// <param name="instr">Instruction being passed by CDB</param>
        internal static void OnNext(Instruction instr)
        {
            throw new NotImplementedException();
        }
    }
}
