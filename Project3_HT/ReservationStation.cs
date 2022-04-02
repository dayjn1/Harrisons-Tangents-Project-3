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
        public static bool empty;
        public static bool ready;
        public static bool waitOnDR;
        public static bool waitOnO1;
        public static bool waitOnO2;
        public static string mnemonic, destR, operand1, operand2;


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

        public ReservationStation(Instruction i)
        {
            empty = false;
            //ReadyForExe(i); //does this need i?
            mnemonic = i.Mnemonic;
            destR = i.DestReg;
            operand1 = i.Reg1;
            operand2 = i.Reg2;
        }
    }
}
