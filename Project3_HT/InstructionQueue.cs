// ---------------------------------------------------------------------------
// File name:                   InstrucionQueue.cs
// Project name:                Project 3 - Harrison's Tangents
// ---------------------------------------------------------------------------
// Creator’s name:              Avery Marlow
// Edited By:                   Avery Marlow, Nataliya Chibizova
// Course-Section:              CSCI-4717
// Creation Date:               03/25/2022
// ---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    class InstructionQueue
    {

        public Queue<Instruction> IQueue = new Queue<Instruction>();
        public void AddToIQueue(Instruction i)
        {
            IQueue.Enqueue(i);
        }
        //TO DO: 
        //1) make sure that queue has appropriate size

        public void DecueueTheInstruction(Instruction i)
        {
            if (i.OpCode == 404) // invelid instruction, stop execution 
            {
                // stop the execution!
            }
            else if(i.OpCode == 0) // HALT do not decueue anything after
            {
                IQueue.Dequeue();
                //call our halt method, wherever that is
            }
            else if(i.OpCode == 1) // LOAD -- send it to the address unit --> LOAD buffer
            {
                AddressUnit.ProcessAU(i); // go to address unit --> check if there is space available on the LOAD buffer
                                                               // --> check if there is space available on reoder buffer
                                                               // stull, if there is no space available
                IQueue.Dequeue();
            }
            else if (i.OpCode == 2) // STORE -- send it to the address unit --> (check if there is space available on RO)
                                    // --> place it on RO
                                    // --> memory unit
            {
                AddressUnit.ProcessAU(i); //go to address unit
                IQueue.Dequeue();
            }


            // TODO: create a method "Operation Bus" that will take care of next instructions
            else if(i.OpCode >=3 || i.OpCode <= 20) // goes to the int 
            {
                // check if there is a free space on the int RS
                // check if thre is a free space on the RO

                IntegerRS.PlaceInstruction(i);
                IQueue.Dequeue();
                //go to op bus/res station
            }
            else if(i.OpCode >= 128 ||i.OpCode <= 131) // goes to floating point adder
            {
                //go to op bus// fp res station

                // check if there is a free space on the int RS
                // check if thre is a free space on the RO

                FPAdderRS.PlaceInstruction(i);
                IQueue.Dequeue();
                
            }
            else if(i.OpCode == 132 || i.OpCode == 133) // FPMultiplierRS for multiply and divide 
            {
                //go to op bus// fp res station
                // check if there is a free space on the int RS
                // check if thre is a free space on the RO

                FPMultiplierRS.PlaceInstruction(i);
                IQueue.Dequeue();
            }
            IQueue.Dequeue();
        }


    }

   
}
