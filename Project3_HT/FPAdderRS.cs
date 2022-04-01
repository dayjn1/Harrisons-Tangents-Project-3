using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    class FPAdderRS
    {
        //attributes
        bool empty;                      //tells whether or not this reservation station is occupied
        bool ready;
        bool waitOnDR;
        bool waitOnO1;
        bool waitOnO2;
        string mnemonic;                            //holds the opcode of the instruction currently in the RS
        string destR;                           //holds the destination register of the current instruction
        string operand1;
        string operand2;


        /**
         * Method Name: FPAdderRS <br>
         * Method Purpose: instantiation of a new empty reservation station that will be populated later 
         *                  default constructor<br>
         * 
         * <hr>
         * Date created:  3/30/2022 <br>
         * 
         * <hr>
         * Notes on specifications, special algorithms, and assumptions: N/A
         * 
         * <hr>
         *   @param 
         *   @return
         */
        public FPAdderRS()
        {
            empty = true;
        }

        /**
         * Method Name: FPAdderRS <br>
         * Method Purpose: create and populate a new reservation station <br>
         * 
         * <hr>
         * Date created:  <br>
         * 
         * <hr>
         * Notes on specifications, special algorithms, and assumptions: N/A
         * 
         * <hr>
         *   @param Instruction
         *   
         */
        public FPAdderRS (Instruction i)
        {
            empty = false;
            //ReadyForExe
            //if staleFlag for destReg == 1 waitOnDR == true
            //if staleFlag for opnd1 == 1 waitOnO1 == true
            //if staleFlag for opnd2 == 1 waitOnO2 == true
            mnemonic = i.Mnemonic;
            destR = i.DestReg;
            operand1 = i.Reg1;
            operand2 = i.Reg2;
        }


        /**
         * Method Name: ReadyForExe <br>
         * Method Purpose: <br>
         * 
         * <hr>
         * Date created:  <br>
         * 
         * <hr>
         * Notes on specifications, special algorithms, and assumptions: N/A
         * 
         * <hr>
         *   @param 
         *   @return
         */
        public void ReadyForExe()
        {
            //need to check the stale register flags and return if ready or not ready to execute
        }


        /**
         * Method Name: populateEmptyRS <br>
         * Method Purpose: populate an empty reservation station <br>
         * 
         * <hr>
         * Date created: 03/30/2022 <br>
         * 
         * <hr>
         * Notes on specifications, special algorithms, and assumptions: N/A
         * 
         * <hr>
         *   @param Instruction
         *   
         */
        public void populateEmptyRS(Instruction i) //may need to return something here
        {
            empty = false;
            //if staleFlag for destReg == 1 waitOnDR == true
            //if staleFlag for opnd1 == 1 waitOnO1 == true
            //if staleFlag for opnd2 == 1 waitOnO2 == true
            mnemonic = i.Mnemonic;
            destR = i.DestReg;
            operand1 = i.Reg1;
            operand2 = i.Reg2;
        }

        //update text
        public String[] updateRSText() 
        {
            string[] text = new string[] { this.mnemonic, this.destR.ToString(), this.operand1.ToString(), this.operand2.ToString() };

            return text;
        }


        //listen to CDB
        //if(CDB.destReg == operand1) waitOnOp1 == false;
        // ^ about the same for all the wait flags
        // ^ update flags when data is no longer stale


        

        internal static void PlaceInstruction(Instruction i)
        {
            throw new NotImplementedException();
        }
    }

}
