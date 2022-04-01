using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    class FPMultiplierRS
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
        bool empty;
        bool ready;
        bool waitOnDR;
        bool waitOnO1;
        bool waitOnO2;
        string mnemonic, destR, operand1, operand2;

        public FPMultiplierRS()
        {
            empty = true;
        }

        public FPMultiplierRS(Instruction i)
        {
            empty = false;
            ReadyForExe(i); //does this need i?
            mnemonic = i.Mnemonic;
            destR = i.DestReg;
            operand1 = i.Reg1;
            operand2 = i.Reg2;
        }

        public void PopulateEmptyRS(Instruction i)
        {
            empty = false;
            ReadyForExe(i);
            mnemonic = i.Mnemonic;
            destR = i.DestReg;
            operand1 = i.Reg1;
            operand2 = i.Reg2;
        }

        public void ClearRS()
        {

        }


        public void ReadyForExe(Instruction inst)
        {
            //check registers used in inst
            // if stale registers found, then ready == false
            //  ^ also need to set waitOn flags
        }




        internal static void PlaceInstruction(Instruction i)
        {
            throw new NotImplementedException();
        }
    }
}
