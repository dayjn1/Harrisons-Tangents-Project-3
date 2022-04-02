using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    static class RSManager
    {
        //create list of reservation stations
        static List<ReservationStation> FPAddRS = new List<ReservationStation>();
        static List<ReservationStation> FPMultRS = new List<ReservationStation>();
        static List<ReservationStation> IntegerRS = new List<ReservationStation>();

        //Reservation station instances to add to list
        //ReservationStation FPAdd1 = new ReservationStation();
        //FPAddRS.Add()

        //methods to control reservation station functions
        public static void PopulateEmptyRS(Instruction i, ReservationStation rs)
        {
            rs.empty = false;
            rs.ReadyForExe(i);
            mnemonic = i.Mnemonic;
            destR = i.DestReg;
            operand1 = i.Reg1;
            operand2 = i.Reg2;
        }


    }
}
