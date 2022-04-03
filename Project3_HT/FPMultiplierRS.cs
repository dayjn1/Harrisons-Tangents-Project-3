using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    static class FPMultiplierRS
    {

        /*
            0. list all attributes -d
            1. create constructor to build empty reservation station -d
            2. create constructor that takes in an Instruction just in case (doubt we'll use it) -d
            3. create the populate empty rs -d
            4. create ready method
                - check stale register flags 
                - update the waitOn flags
                - call this method for each cycle until everything is ready
            5. create ClearRS method 
            
         */

        //attributes
        static bool empty;
        static bool ready;
        static bool waitOnDR;
        static bool waitOnO1;
        static bool waitOnO2;
        static string mnemonic, destR, operand1, operand2;

        /*public FPMultiplierRS()
        {
            //initialize empty and ready to true and waits to false
            empty = true;
            ready = true;
            waitOnDR = false;
            waitOnO1 = false;
            waitOnO2 = false;
        }*/

        /*public FPMultiplierRS(Instruction i)
        {
            empty = false;
            ReadyForExe(i); //does this need i?
            mnemonic = i.Mnemonic;
            destR = i.DestReg;
            operand1 = i.Reg1;
            operand2 = i.Reg2;
        }*/

        public static void PopulateEmptyRS(Instruction i)
        {
            empty = false;
            ReadyForExe(i);
            mnemonic = i.Mnemonic;
            destR = i.DestReg;
            operand1 = i.Reg1;
            operand2 = i.Reg2;
        }

        public static void ClearRS()
        {
            empty = true;
            ready = true;
            waitOnDR = false;
            waitOnO1 = false;
            waitOnO2 = false;

            //reset the values of Instruction based attributes
            mnemonic = "";
            destR = "";
            operand1 = "";
            operand2 = "";
        }

        //could use to keep count of dependency delays, just change to return bool ready
        public static void ReadyForExe(Instruction inst) //send to functional unit from here???
        {
            //check registers used in inst
            // if stale registers found, then ready == false
            //  ^ also need to set waitOn flags
        }


        //check for CDB
        public static void CheckCDB(Instruction i)
        {
            //grab instruction.destReg and compare to registers waiting on something
        }



        internal static void PlaceInstruction(Instruction i)
        {
            throw new NotImplementedException();
        }
    }
}
