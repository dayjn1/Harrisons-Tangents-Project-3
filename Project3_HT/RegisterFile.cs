// ---------------------------------------------------------------------------
// File name:                   RegisterFile.cs
// Project name:                Project 3 - Harrison's Tangents
// ---------------------------------------------------------------------------
// Creator’s name:              Janine Day
// Course-Section:              CSCI-4717
// Creation Date:               03/27/2022
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    /**
    * Class Name:       RegisterFile
    * Class Purpose:    Handle the register file, most methods are used in reservation stations
    *
    * <hr>
    * Date created:     03/27/2022
    * @Janine Day
    */
    public static class RegisterFile
    {
        public static string[] RegInfo = new string[32];
        public static RegTicket[] RegAvail =
        {
            new RegTicket(true, -1, 5), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0),
            new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0),
            new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0),
            new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0),
            new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0),
            new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0),
            new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0),
            new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0), new RegTicket(true, -1, 0)
        };

        /**
        * Struct Name:       RegTicket
        * Struct Purpose:    To provide use with the reservation stations
        *                    Specifically to mark flags stale as an instruction comes out of the Instruction Queue
        *                    and allow the instruction to move through even though flags are stale
        *
        * <hr>
        * Date created:     03/27/2022
        * @Janine Day
        */
        public struct RegTicket
        {
            public bool Avail;
            public int LineNum;
            public uint Data;
            public RegTicket(bool Avail, int LineNum, uint Data)
            {
                this.Avail = Avail;
                this.LineNum = LineNum;
                this.Data = Data;
            }//end RegTicket(bool, int)
        }//end RegTicket


        /**
        * Method Name:    UpdateRegister(Instruction)
        * Method Purpose: Takes an instruction and updates the destReg, if there is one
        *
        * <hr>
        * Date created: 03/27/2022
        * @Janine Day
        * <hr>
        * @param Instruction
        * @return string[] array with updated mnemonic added
        */
        public static string[] UpdateRegister(Instruction instr)
        {
            string[] temp = instr.DestReg.Split(' ');
            int i = Convert.ToInt32(temp[1], 16);

            if (temp[0].Equals("R"))
            {
                RegInfo[i] = instr.Mnemonic;
                RegAvail[i].Avail = true;
            }//end if
            else
            {
                RegInfo[i + 16] = instr.Mnemonic;
                RegAvail[i + 16].Avail = true;
            }//end else

            return RegInfo;
        }//end UpdateRegister(Instruction)

        /**
        * Method Name:    MarkUnavail(string, int)
        * Method Purpose: Marks the specified register unavailable, along with the linenum of the instruction
        *
        * <hr>
        * Date created: 03/27/2022
        * @Janine Day
        * <hr>
        * @param string reg, destination register
        * @param int LineNum, Line number of instruction
        */
        public static void MarkUnavail(string reg, int LineNum)
        {
            string[] temp = reg.Split(' ');
            int i = Convert.ToInt32(temp[1], 16);

            if (temp[0].Equals("R"))
            {
                RegAvail[i].Avail = false;
                RegAvail[i].LineNum = LineNum;
            }//end if
            else
            {
                RegAvail[i + 16].Avail = false;
                RegAvail[i + 16].LineNum = LineNum;
            }//end else
        }//end MarkUnavail(string, int)


        /**
        * Method Name:    IsAvail(string)
        * Method Purpose: Finds if the register supplied is stale or not
        *
        * <hr>
        * Date created: 03/27/2022
        * @Janine Day
        * <hr>
        * @param string reg, destination register
        * @return RegTicket, supplies bool and lineNum
        */
        public static RegTicket IsAvail(string reg)
        {
            if(reg != null)
            {
                string[] temp = reg.Split(' ');
                int i = Convert.ToInt32(temp[1], 16);

                if (temp[0].Equals("R"))
                {
                    return RegAvail[i];
                }//end if
                else
                {
                    return RegAvail[i + 16];
                }//end else
            }//end if

            return new RegTicket(true, -1, 0);
            
        }//end IsAvail(string)

        public static uint ReturnReg(string reg)
        {
            if (reg != null)
            {
                string[] temp = reg.Split(' ');
                int i = Convert.ToInt32(temp[1], 16);

                if (temp[0].Equals("R"))
                {
                    return RegAvail[i].Data;
                }//end if
                else
                {
                    return RegAvail[i + 16].Data;
                }//end else
            }//end if
            else
                return 0;

        }
    }//end RegisterFile
}//end Project3_HT