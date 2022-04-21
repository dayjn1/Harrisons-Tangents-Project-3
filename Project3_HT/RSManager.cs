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
        public static List<ReservationStation> FPAddRS = new List<ReservationStation>();
        public static List<ReservationStation> FPMultRS = new List<ReservationStation>();
        public static List<ReservationStation> IntegerRS = new List<ReservationStation>();

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
            rs.ready = true;
            rs.currentInst = i;
            rs.mnemonic = i.Mnemonic;
            rs.destR = i.DestReg;

            if (i.useR1)
                rs.operand1 = i.Reg1;
            else
                rs.operand1 = null;

            if (i.useR2)
                rs.operand2 = i.Reg2;
            else
                rs.operand2 = null;

            if (i.useImm)
                rs.imm = i.Imm;
            else
                rs.imm = null;
            
            rs.lineNumOfInst = i.lineNum;
            UpdateStaleFlagsOnReciept(rs);
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
            rs.lineNumOfInst = -1;
        }

        //could use to keep count of dependency delays, just change to return bool ready
        //use this 
        public static void UpdateStaleFlagsOnReciept(ReservationStation rs) //send to functional unit from here???
        {
            //check registers used in inst
            // if stale registers found, then ready == false
            //  ^ also need to set waitOn flags
            //RegisterFile.RegTicket tempDR = RegisterFile.IsAvail(rs.destR);
            
            
            RegisterFile.RegTicket tempO1 = RegisterFile.IsAvail(rs.operand1);
            RegisterFile.RegTicket tempO2 = RegisterFile.IsAvail(rs.operand2);

            if(!rs.empty)
            {
                /*if (!tempDR.Avail && tempDR.LineNum != rs.lineNumOfInst)        //RegTicket's lineNum is set on the line that should have marked it as stale, don't want an infinite stall if waiting on itself
                {
                    rs.waitOnDR = true;
                    rs.ready = false;
                }*/

                if (!tempO1.Avail && tempO1.LineNum != rs.lineNumOfInst)
                {
                    rs.waitOnO1 = true;
                    rs.ready = false;
                }
                if (!tempO2.Avail && tempO2.LineNum != rs.lineNumOfInst)
                {
                    rs.waitOnO2 = true;
                    rs.ready = false;
                }
                
                if(!rs.waitOnO1 && !rs.waitOnO2)
                {
                    rs.ready = true;
                    if(rs.operand1 != null)
                        rs.currentInst.Reg1Data = RegisterFile.ReturnReg(rs.operand1);
                    if(rs.operand2 != null)
                        rs.currentInst.Reg2Data = RegisterFile.ReturnReg(rs.operand2);
                }

            }
        }//end ReadyForExe

        //check for CDB
        public static bool CheckCDB(ReservationStation rs)
        {
            //grab instruction.destReg and compare to registers waiting on something
            Instruction temp = CDBus.currentInstruction;
            if (temp != null)
            {
                if(!rs.ready)
                {
                    if (rs.waitOnO1 && temp.DestReg == rs.operand1)
                    {
                        rs.waitOnO1 = false;
                        rs.currentInst.Reg1Data = (int)temp.Result;
                    }
                    if (rs.waitOnO2 && temp.DestReg == rs.operand2)
                    {
                        rs.waitOnO2 = false;
                        rs.currentInst.Reg2Data = (int)temp.Result;
                    }


                }
            }

            return IsReady(rs);
        }//end checkCDB

        public static bool CheckRegFile(ReservationStation rs)
        {
            RegisterFile.RegTicket temp;
            
            //grab instruction.destReg and compare to registers waiting on something
            if (!rs.ready)
            {
                if (rs.waitOnO1)
                {
                    temp = RegisterFile.IsAvail(rs.currentInst.Reg1);
                    if (temp.Avail)
                    {   
                        rs.currentInst.Reg1Data = (int)temp.Data;
                        rs.waitOnO1 = false;
                    }
                }
                if (rs.waitOnO2)
                {
                    temp = RegisterFile.IsAvail(rs.currentInst.Reg2);
                    if (temp.Avail)
                    {
                        rs.currentInst.Reg2Data = (int)temp.Data;
                        rs.waitOnO2 = false;
                    }
                }


            }
            

            return IsReady(rs);
        }//end checkCDB

        public static bool IsReady(ReservationStation r)
        {
            if (r.waitOnDR || r.waitOnO1 || r.waitOnO2)
                r.ready = false;
            else
                r.ready = true;

            return r.ready;
        }



        //after each foreach loop, if we have found one that's not empty, then return without wasting more time/resources
        public static bool CheckAllRSEmpty()
        {
            bool allEmpty = true;

            foreach (ReservationStation rs in FPAddRS)
            {
                if (!rs.empty)
                {
                    allEmpty = false;
                    break;
                }
            }
            if (!allEmpty)
                return allEmpty;

            foreach (ReservationStation rs in FPMultRS)
            {
                if (!rs.empty)
                {
                    allEmpty = false;
                    break;
                }
            }
            if (!allEmpty)
                return allEmpty;

            foreach (ReservationStation rs in IntegerRS)
            {
                if (!rs.empty)
                {
                    allEmpty = false;
                    break;
                }
            }
            if (!allEmpty)
                return allEmpty;
            else
                return allEmpty;

        }

    }
}
