using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    public static class RegisterFile
    {
        public static string[] RegInfo = new string[32];
        public static RegTicket[] RegAvail =
        {
            new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1), 
            new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1),
            new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1), 
            new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1),
            new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1), 
            new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1),
            new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1), 
            new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1), new RegTicket(true, -1)
        };

        public struct RegTicket
          {
             public bool Avail;
             public int LineNum;
             public RegTicket(bool Avail, int LineNum)
             {
                 this.Avail = Avail;
                 this.LineNum = LineNum;
             }
        }

        public static string[] UpdateRegister(Instruction instr)
        {
            string[] temp = instr.DestReg.Split(' ');
            int i = Convert.ToInt32(temp[1], 16);

            if (temp[0].Equals("R"))
            {
                RegInfo[i] = instr.Mnemonic;
                RegAvail[i].Avail = true;
            }
            else
            {
                RegInfo[i + 16] = instr.Mnemonic;
                RegAvail[i + 16].Avail = true;

            }

            return RegInfo;
        }

        public static void MarkUnavail(string reg, int LineNum)
        {
            string[] temp = reg.Split(' ');
            int i = Convert.ToInt32(temp[1], 16);

            if (temp[0].Equals("R"))
            {
                RegAvail[i].Avail = false;
                RegAvail[i].LineNum = LineNum;
            }
            else
            {
                RegAvail[i + 16].Avail = false;
                RegAvail[i + 16].LineNum = LineNum;
            }
        }

        public static RegTicket IsAvail(string reg)
        {
            string[] temp = reg.Split(' ');
            int i = Convert.ToInt32(temp[1], 16);

            if (temp[0].Equals("R"))
            {
                return RegAvail[i];
            }
            else
            {
                return RegAvail[i + 16];
            }
        }
    }
}
