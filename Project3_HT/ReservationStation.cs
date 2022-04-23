using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    public class ReservationStation
    {
        //attributes
        public bool empty;
        public Instruction currentInst;
        public bool ready;
        public bool waitOnDR;
        public bool waitOnO1;
        public bool waitOnO2;
        public string mnemonic, destR, operand1, operand2, imm;
        public int lineNumOfInst;


        //constructors
        public ReservationStation()
        {
            //initialize empty and ready to true and waits to false bc nothing has been populated yet
            empty = true;
            ready = true;
            waitOnDR = false;
            waitOnO1 = false;
            waitOnO2 = false;
        }//end default constructor

        /*public ReservationStation(Instruction i)
        {
            empty = false;
            //currentInst = i;
            //ReadyForExe(i); //does this need i?
            mnemonic = i.Mnemonic;
            destR = i.DestReg;
            operand1 = i.Reg1;
            operand2 = i.Reg2;
        }*/
    }
}
