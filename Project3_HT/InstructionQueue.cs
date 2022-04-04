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
        
        public static void DecueueTheInstruction()
        {
            Instruction i = IQueue.Peek();
            if (i.OpCode == 0) // HALT ---> chnge the bool haltFound to true
            {
                haltNotFound = false;
            }
            else if (i.OpCode == 1) // LOAD -- send it to the address unit --> LOAD buffer
            {
                if (ReorderBuffer.IsReorderBufFree().Equals(true) && LoadBuffer.LdBuffer.Count() < 5)
                {
                    AddressUnit.ProcessAU(i); // go to address unit --> check if there is space available on the LOAD buffer
                                              // --> check if there is space available on reoder buffer
                                              // stull, if there is no space available
                    IQueue.Dequeue(); 
                }
            }
            else if (i.OpCode == 2) // STORE -- send it to the address unit --> (check if there is space available on RO)
                                    // --> place it on RO
                                    // --> memory unit
            {

                if (ReorderBuffer.IsReorderBufFree().Equals(true))  // check for space of RB
                {
                    AddressUnit.ProcessAU(i); //go to address unit
                    IQueue.Dequeue();
                }
                
            }
            else if(i.OpCode >=3 || i.OpCode <= 20) // goes to the intRS 
            {
                // Check for space on the RB and RS and dequeue
                if (ReorderBuffer.IsReorderBufFree().Equals(true))
                {
                    for (int j = 0; j < RSManager.IntegerRS.Count(); j++)
                    {
                        if (RSManager.IntegerRS[j].Equals(true))
                        {
                            ReorderBuffer.AddToReorderBuf(i);
                            RSManager.PopulateEmptyRS(i, RSManager.IntegerRS[j]);
                            IQueue.Dequeue();
                        }
                    } 
                }
            }
            else if(i.OpCode >= 128 ||i.OpCode <= 131) // goes to floating point adder
            {
                // Check for space on the RB and RS and dequeue
                if (ReorderBuffer.IsReorderBufFree().Equals(true))
                {
                    for (int j = 0; j < RSManager.FPAddRS.Count(); j++)
                    {
                        if (RSManager.FPAddRS[j].Equals(true))
                        {
                            ReorderBuffer.AddToReorderBuf(i);
                            RSManager.PopulateEmptyRS(i, RSManager.FPAddRS[j]);
                            IQueue.Dequeue();
                        }
                    } 
                }

            }
            else if(i.OpCode == 132 || i.OpCode == 133) // FPMultiplierRS for multiply and divide 
            {
                // Check for space on the RB and RS and dequeue
                if (ReorderBuffer.IsReorderBufFree().Equals(true))
                {
                    for (int j = 0; j < RSManager.FPMultRS.Count(); j++)
                    {
                        if (RSManager.FPMultRS[j].Equals(true))
                        {
                            ReorderBuffer.AddToReorderBuf(i);
                            RSManager.PopulateEmptyRS(i, RSManager.FPMultRS[j]);
                            IQueue.Dequeue();
                        }
                    } 
                }
                
            }// end of else if
            
        } // end of DequeueTheInstruction


    }

   
}
