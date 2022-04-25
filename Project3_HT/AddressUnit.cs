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
                int Reg1 = RegisterFile.ReturnRegData(i.Reg1);
                
                if (i.useImm && Int32.TryParse(i.Imm, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int tempImm))
                {
                    i.Address = (uint)Reg1 + (uint)tempImm;
                    i.Address &= 0x000FFFFF;
                }
                else if(i.useR2)
                {
                    int Reg2 = RegisterFile.ReturnRegData(i.Reg2);
                    i.Address = (uint)Reg1 + (uint)Reg2;
                    i.Address &= 0x000FFFFF;
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