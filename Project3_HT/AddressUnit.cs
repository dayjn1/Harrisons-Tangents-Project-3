using Project3_HT;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                bool valid1 = Int32.TryParse(i.Reg1, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int tempDR);
                bool valid2 = Int32.TryParse(i.Imm, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int tempImm);

                uint a = RegisterFile.ReturnReg(i.Reg1);
                

                if (valid2)
                {
                    i.Address = a + (uint)tempImm;
                }

                //Enqueue
                AddressUnitQueue.Enqueue(i);
                counter++;
            }

        }
        public static void ProcessAU()
        {
            Instruction i = AddressUnitQueue.Peek();
            if (i.OpCode == 1 || i.OpCode == 3)
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