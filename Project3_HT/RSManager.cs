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

            if (i.OpCode >= 128 && i.OpCode <= 133)
                rs.IsFloat = true;
            else
                rs.IsFloat = false;
            
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

            RegisterFile.RegTicket tempO1 = new RegisterFile.RegTicket(true, -1, 0);
            RegisterFile.RegTicket tempO2 = new RegisterFile.RegTicket(true, -1, 0);
            RegisterFile.FRegTicket tempFO1 = new RegisterFile.FRegTicket(true, -1, 0);
            RegisterFile.FRegTicket tempFO2 = new RegisterFile.FRegTicket(true, -1, 0);

            if(rs.IsFloat == false)
            {
                tempO1 = RegisterFile.IsAvail(rs.operand1);
                tempO2 = RegisterFile.IsAvail(rs.operand2);
            }
            else
            {
                tempFO1 = RegisterFile.FIsAvail(rs.operand1);
                tempFO2 = RegisterFile.FIsAvail(rs.operand2);
            }


            if(!rs.empty)
            {

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
                if (!tempFO1.Avail && tempFO1.LineNum != rs.lineNumOfInst)
                {
                    rs.waitOnO1 = true;
                    rs.ready = false;
                }
                if (!tempFO2.Avail && tempFO2.LineNum != rs.lineNumOfInst)
                {
                    rs.waitOnO2 = true;
                    rs.ready = false;
                }

                if (!rs.waitOnO1 && !rs.waitOnO2)
                {
                    rs.ready = true;
                    if(rs.operand1 != null && !rs.IsFloat)
                        rs.currentInst.Reg1Data = RegisterFile.ReturnRegData(rs.operand1);
                    if(rs.operand2 != null && !rs.IsFloat)
                        rs.currentInst.Reg2Data = RegisterFile.ReturnRegData(rs.operand2);
                    if(rs.operand1 != null && rs.IsFloat)
                        rs.currentInst.FReg1Data = RegisterFile.FReturnRegData(rs.operand1);
                    if (rs.operand2 != null && rs.IsFloat)
                        rs.currentInst.FReg2Data = RegisterFile.FReturnRegData(rs.operand2);
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
                        if(rs.IsFloat)
                            rs.currentInst.FReg1Data = (float)temp.FResult;
                        else
                            rs.currentInst.Reg1Data = (int)temp.Result;
                    }
                    if (rs.waitOnO2 && temp.DestReg == rs.operand2)
                    {
                        rs.waitOnO2 = false;
                        if (rs.IsFloat)
                            rs.currentInst.FReg2Data = (float)temp.FResult;
                        else
                            rs.currentInst.Reg2Data = (int)temp.Result;
                    }


                }
            }

            return IsReady(rs);
        }//end checkCDB

        public static bool CheckRegFile(ReservationStation rs)
        {
            RegisterFile.RegTicket temp;
            RegisterFile.FRegTicket Ftemp;
            
            //grab instruction.destReg and compare to registers waiting on something
            if (!rs.ready)
            {
                if (rs.waitOnO1 && !rs.IsFloat)
                {
                    temp = RegisterFile.IsAvail(rs.currentInst.Reg1);
                    if (temp.Avail)
                    {   
                        rs.currentInst.Reg1Data = (int)temp.Data;
                        rs.waitOnO1 = false;
                    }
                }
                if (rs.waitOnO1 && rs.IsFloat)
                {
                    Ftemp = RegisterFile.FIsAvail(rs.currentInst.Reg1);
                    if (Ftemp.Avail)
                    {
                        rs.currentInst.FReg1Data = (float)Ftemp.Data;
                        rs.waitOnO1 = false;
                    }
                }
                if (rs.waitOnO2 && !rs.IsFloat)
                {
                    temp = RegisterFile.IsAvail(rs.currentInst.Reg2);
                    if (temp.Avail)
                    {
                        rs.currentInst.Reg2Data = (int)temp.Data;
                        rs.waitOnO2 = false;
                    }
                }
                if (rs.waitOnO2 && rs.IsFloat)
                {
                    Ftemp = RegisterFile.FIsAvail(rs.currentInst.Reg2);
                    if (Ftemp.Avail)
                    {
                        rs.currentInst.FReg2Data = (float)Ftemp.Data;
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
