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
        //public static string[] RegInfo = new string[32];
        public static RegTicket[] Registers =
        {
            new RegTicket(true, -1, 0), new RegTicket(true, -1, 1), new RegTicket(true, -1, 2), new RegTicket(true, -1, 0),
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
            public int Data;
            public RegTicket(bool Avail, int LineNum, int Data)
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
        public static List<int> UpdateRegister(Instruction instr)
        {
            List<int> RegData = new List<int>();
            string[] temp = instr.DestReg.Split(' ');
            int i = Convert.ToInt32(temp[1], 16);

            if (temp[0].Equals("R"))
            {
                //RegInfo[i] = instr.Result.ToString();
                if(instr.Result != null)
                    Registers[i].Data = (int)instr.Result;
                Registers[i].LineNum = instr.lineNum;
                Registers[i].Avail = true;
            }//end if
            else
            {
                //RegInfo[i + 16] = instr.Result.ToString();
                if (instr.Result != null)
                    Registers[i + 16].Data = (int)instr.Result;
                Registers[i + 16].LineNum = instr.lineNum;
                Registers[i + 16].Avail = true;
            }//end else

            foreach(RegTicket RT in Registers)
            {
                RegData.Add(RT.Data);
            }

            return RegData;
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
                Registers[i].Avail = false;
                Registers[i].LineNum = LineNum;
            }//end if
            else
            {
                Registers[i + 16].Avail = false;
                Registers[i + 16].LineNum = LineNum;
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
                    return Registers[i];
                }//end if
                else
                {
                    return Registers[i + 16];
                }//end else
            }//end if

            return new RegTicket(true, -1, 0);
            
        }//end IsAvail(string)

        public static int ReturnReg(string reg)
        {
            if (reg != null)
            {
                string[] temp = reg.Split(' ');
                int i = Convert.ToInt32(temp[1], 16);

                if (temp[0].Equals("R"))
                {
                    return Registers[i].Data;
                }//end if
                else
                {
                    return Registers[i + 16].Data;
                }//end else
            }//end if
            else
                return 0;
        }

    }//end RegisterFile
}//end Project3_HT