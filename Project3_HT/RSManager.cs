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

        //method to populate the Reservation station lists
        public static void PopulateLists()
        {
            ReservationStation FPAdd1 = new ReservationStation();
            ReservationStation FPAdd2 = new ReservationStation();
            ReservationStation FPAdd3 = new ReservationStation();

            ReservationStation FPMult1 = new ReservationStation();
            ReservationStation FPMult2 = new ReservationStation();
            ReservationStation FPMult3 = new ReservationStation();

            ReservationStation Integer1 = new ReservationStation();
            ReservationStation Integer2 = new ReservationStation();
            ReservationStation Integer3 = new ReservationStation();

            FPAddRS.Add(FPAdd1);
            FPAddRS.Add(FPAdd2);
            FPAddRS.Add(FPAdd3);

            FPMultRS.Add(FPMult1);
            FPMultRS.Add(FPMult2);
            FPMultRS.Add(FPMult3);

            IntegerRS.Add(Integer1);
            IntegerRS.Add(Integer2);
            IntegerRS.Add(Integer3);
        }

        //methods to control reservation station functions
        public static void PopulateEmptyRS(Instruction i, ReservationStation rs)
        {
            rs.empty = false;
            //rs.ReadyForExe(i);
            //rs.currentInst = i;
            rs.mnemonic = i.Mnemonic;
            rs.destR = i.DestReg;
            rs.operand1 = i.Reg1;
            rs.operand2 = i.Reg2;
        }

        public static void ClearRS(ReservationStation rs)
        {
            rs.empty = true;
            rs.ready = true;
            rs.waitOnDR = false;
            rs.waitOnO1 = false;
            rs.waitOnO2 = false;

            //reset the values of Instruction based attributes
            rs.mnemonic = "";
            rs.destR = "";
            rs.operand1 = "";
            rs.operand2 = "";
        }

        //could use to keep count of dependency delays, just change to return bool ready
        public static void ReadyForExe(ReservationStation rs) //send to functional unit from here???
        {
            //check registers used in inst
            // if stale registers found, then ready == false
            //  ^ also need to set waitOn flags


            if(!rs.empty)
            {
                if (/*rs.destR == stale*/true)
                {
                    rs.waitOnDR = true;
                    rs.ready = false;
                }
                if (/*rs.operand1 == stale*/true)
                {
                    rs.waitOnO1 = true;
                    rs.ready = false;
                }
                if (/*rs.operand2 == stale*/true)
                {
                    rs.waitOnO2 = true;
                    rs.ready = false;
                }
                else
                    rs.ready = true;
            }
        }

        //check for CDB
        public static void CheckCDB(Instruction i)
        {
            //grab instruction.destReg and compare to registers waiting on something
        }


    }
}
