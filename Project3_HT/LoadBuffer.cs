using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MemoryUnit;
namespace Project3_HT
{
    public class LoadBuffer
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
            LdBuffer.Enqueue(i);
        }
        public static void SendToMemUnit()
        {
            
            AddToMemUnit(LdBuffer.Dequeue());
        }
    }
}