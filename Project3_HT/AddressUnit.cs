using Project3_HT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Project3_HT
{
    public class AddressUnit
    {
        public static Queue<Instruction> AddressUnitQueue = new Queue<Instruction>();

        public static void AddToAddressUnitQueue(Instruction i)
        {
            int counter = 0;
            int size = 1;           //size of queue
            if (counter < size)
            {
                //i.Address = i.Reg1 + i.Imm;
                //Enqueue
                AddressUnitQueue.Enqueue(i);
                counter++;
            }

        }
        public static void ProcessAU()
        {
            Instruction i = AddressUnitQueue.Peek();
            if (i.OpCode == 1)
            {
                // check if there is a space on the 
                LoadBuffer.AddToLoadBuffer(i);
                AddressUnitQueue.Dequeue();

            }
            else
            {
                ReorderBuffer.PassedtoRB(i);
                AddressUnitQueue.Dequeue();
            }
        }
    }
}