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
        uint opcode;                            //holds the opcode of the instruction currently in the RS
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
            opcode = i.OpCode;
            destR = i.DestReg;
            operand1 = i.Reg1;
            operand2 = i.Reg2;
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
        public void populateEmptyRS(Instruction i)
        {
            opcode = i.OpCode;
            destR = i.DestReg;
            operand1 = i.Reg1;
            operand2 = i.Reg2;
        }
        
        //


    }

}
