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
    static class InstructionQueue
    {
        public static bool haltNotFound = true; 
        public static Queue<Instruction> IQueue = new Queue<Instruction>();
        public static int LineNum = 0;
       
        public static void AddToIQueue(Instruction i)
        {
            int counter = 0;
            int size = 6;//size of queue
            if (counter < size)
            {
                LineNum++;
                i.lineNum = LineNum;
                //Enqueue
                IQueue.Enqueue(i);
                counter++;
            }
           
        }
        //TO DO: 
        //1) make sure that queue has appropriate size

        public static void DecueueTheInstruction()
        {
            Instruction i = IQueue.Peek();
            if (i.OpCode == 0) // HALT ---> chnge the bool haltFound to true
            {
                haltNotFound = false;
            }
            else if (i.OpCode == 1) // LOAD -- send it to the address unit --> LOAD buffer
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

                //FPAdderRS.populateEmptyRS(i);  
                IQueue.Dequeue();
                
            }
            else if(i.OpCode == 132 || i.OpCode == 133) // FPMultiplierRS for multiply and divide 
            {
                
                //go to op bus// fp res station
                // check if there is a free space on the int RS
                // check if thre is a free space on the RO

                /*if (FPMultiplierRS.PopulateEmptyRS = t, RO.Empty = t)
                {
                    IQueue.Dequeue();
                }*/
                FPMultiplierRS.PopulateEmptyRS(i);

                IQueue.Dequeue();
            }
            IQueue.Dequeue();
        }


    }

   
}
