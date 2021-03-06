// ---------------------------------------------------------------------------
// File name:                   ReorderBuffer.cs
// Project name:                Project 3 - Harrison's Tangents
// ---------------------------------------------------------------------------
// Creator’s name:              Janine Day
// Edited By:                   Janine Day
// Course-Section:              CSCI-4717
// Creation Date:               03/27/2022
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    /**
    * Class Name:       ReorderBuffer
    * Class Purpose:    Handle the reorder buffer
    *
    * <hr>
    * Date created:     03/27/2022
    * @Janine Day
    */
    static class ReorderBuffer
    {
        public static Queue<Instruction> ReorderBuf = new Queue<Instruction>();
        public static List<Instruction> Passed = new List<Instruction>();


        /**
        * Method Name:    AddToReorderBuf(Instruction)
        * Method Purpose: Adds to the reorder buffer queue
        *
        * <hr>
        * Date created: 03/27/2022
        * @Janine Day
        * <hr>
        * @param Instruction to enqueue
        */
        public static void AddToReorderBuf(Instruction i)
        {
            ReorderBuf.Enqueue(i);
        }//end AddToReorderBuf

        /**
        * Method Name:    IsReorderBufFree()
        * Method Purpose: Determines if there is room in the reorder buffer
        *
        * <hr>
        * Date created: 03/27/2022
        * @Janine Day
        * <hr>
        * @return bool, true if free - false if not
        */
        public static bool IsReorderBufFree()
        {
            if (ReorderBuf.Count > 4)
                return false;
            else
                return true;
        }//end IsReorderBufFree


        /**
        * Method Name:    IsReorderBufFree()
        * Method Purpose: Determines if there is room in the reorder buffer
        *
        * <hr>
        * Date created: 03/27/2022
        * @Janine Day
        * <hr>
        * @return bool, true if free - false if not
        */
        public static Instruction RemoveFromReorderBuf()
        {           
            if (ReorderBuf.Count > 0)
            {
                Instruction temp = ReorderBuf.Peek();
                if (Passed.Contains(temp))
                {
                    if(temp.OpCode == 2 || temp.OpCode == 4)
                        SendToMemUnit();
                    else
                        return ReorderBuf.Dequeue();
                }//end if
            }//end if

            return null;
        }//end RemoveFromReorderBuf()

        /// <summary>
        /// Send instruction to the MemUnit and Dequeue it from the Load Buffer
        /// </summary>
        public static void SendToMemUnit()
        {
            if (FuncUnitManager.At(1).Instructions.Count == 0)
            {
                FuncUnitManager.At(1).Enqueue(ReorderBuf.Dequeue());
            }//end if
        }//end SendToMemUnit()

        /**
        * Method Name:    PassedtoRB(Instruction)
        * Method Purpose: Adds instruction to Passed list, used for dequeue purposes
        *
        * <hr>
        * Date created: 03/27/2022
        * @Janine Day
        * <hr>
        * @param Instruction
        */
        public static void PassedtoRB(Instruction i)
        {
            Passed.Add(i);
        }//end PassedtoRB(Instruction)

        /**
        * Method Name:    GetArray()
        * Method Purpose: Converts ReorderBuf to an array, used for visuals
        *
        * <hr>
        * Date created: 03/27/2022
        * @Janine Day
        * <hr>
        * @return Instruction[]
        */
        public static Instruction[] GetArray()
        {
            return ReorderBuf.ToArray();
        }//end GetArray()
    }//end ReorderBuffer
}//end Project3_HT
