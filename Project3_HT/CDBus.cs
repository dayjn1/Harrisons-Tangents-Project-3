// ---------------------------------------------------------------------------
// File name:                   CDBus.cs
// Project name:                Project 3 - Harrison's Tangents
// Developers:                  Jason Middlebrook
// Course-Section:              CSCI 4717-201
// Creation Date:               03/28/2022
// 
// To do: Only one functional unit can send results over the CDB in a cycle. Need some way of keeping track of this
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    ///Check each functional unit to see if it's done executing, receive results
    ///from left to right on the pipeline and make sure that you're iterating
    ///through which functional units we receive from every cycle
    /// 
    /// <summary>
    /// The common data bus passes info from functional units back to reservation stations and to the ROB
    /// </summary>
    internal static class CDBus
    {
        private static int iNextFuncUnit { get; set; }
        public static Instruction currentInstruction { get; set; }

        /// <summary>
        /// Called every cycle:
        /// Check if value on CDB
        ///     if yes, check res.stations one by one if they need the data (from res stations)
        ///     before pushing to reorder buf
        ///     if no, do nothing
        /// </summary>
        public static void Cycle()
        {
            ReceiveResults();
            SendResults();
            //After this returns, main simulation should call res stations to get results from CDB
        }

        /// <summary>
        /// Send instruction execution results to the ReorderBuffer
        /// </summary>
        public static void SendResults()
        {
            if (currentInstruction != null)
            {
                ReorderBuffer.PassedtoRB(currentInstruction);
            }
        }

        /// <summary>
        /// The CDB must take results from functional units based on if they're ready and in order.
        /// The goal is to send these results, including the rd info, to res stations and ROB.
        /// </summary>
        public static void ReceiveResults()
        {
            int j = iNextFuncUnit;
            int Count = FuncUnitManager.Count;

            for (int i = 0; i < Count; i++)           //j is where we are in the array
            {
                if (FuncUnitManager.At(j).Instructions.Count > 0 && FuncUnitManager.At(j).Executed)                           //If func unit is ready to send results
                {
                    Instruction temp = FuncUnitManager.At(j).Instructions.Peek();
                    if (temp.OpCode != 2 && temp.OpCode != 4)
                    {
                        currentInstruction = FuncUnitManager.At(j).Dequeue();
                        iNextFuncUnit = j + 1;                                //Iterates nextFuncUnit to after the one that was ready
                        return;
                    }
                    else
                        FuncUnitManager.At(j).Dequeue();    // store ends here
                                        
                }

                j++;

                if (j == Count)
                    j = 0;

            }//end for j

            //If j has reached the end of the physical array, circle around to the beginning

           

            currentInstruction = null;                                      //No results ready

        }//end ReceiveResults(Instruction)


    }//end CDBus class
}