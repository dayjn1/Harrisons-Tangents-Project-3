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

        public static string[] UpdateRegister(Instruction instr)
        {
            string[] temp = instr.DestReg.Split(' ');
            int i = Convert.ToInt32(temp[1], 16);

            if(temp[0].Equals("R"))
            {
                RegInfo[i] = instr.Mnemonic;
            }
            else
            {
                RegInfo[i + 16] = instr.Mnemonic;
            }

            return RegInfo;
        }
    }
}
