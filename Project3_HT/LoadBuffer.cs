using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    public static class LoadBuffer
    {
        public static Queue<Instruction> LdBuffer = new Queue<Instruction>();
        
        public static void AddToLoadBuffer(Instruction i)
        {
            int counter = 0;
            int size = 5;//size of queue
            if (counter < size)
            {
                //Enqueue
                LdBuffer.Enqueue(i);
                counter++;
            }
            //AUTicket myAU = new AUTicket(i.Mnemonic,i.DestReg, i.Reg1);
             
        }
        /// <summary>
        /// Send instruction to the MemUnit and Dequeue it from the Load Buffer
        /// </summary>
        public static void SendToMemUnit()
        {
            if (FuncUnits.At(0).Instructions.Count == 0)
            {
                FuncUnits.At(0).Instructions.Enqueue(LdBuffer.Dequeue());
            }

        }

    }
}