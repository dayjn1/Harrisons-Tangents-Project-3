using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    public static class ALU
    {

        public static int MOV(int Imm)
        {
            return Imm;
        }

        public static int ADD(string Reg1, string Reg2)
        {
            return RegisterFile.ReturnReg(Reg1) + RegisterFile.ReturnReg(Reg2);
        }

        public static int ADDI(string Reg1, string Imm)
        {
            Int32.TryParse(Imm, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int tempImm);
            return RegisterFile.ReturnReg(Reg1) + tempImm;
        }

        public static int SUB(string Reg1, string Reg2)
        {
            return RegisterFile.ReturnReg(Reg1) - RegisterFile.ReturnReg(Reg2);
        }

        public static int SUBI(string Reg1, string Imm)
        {
            Int32.TryParse(Imm, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int tempImm);
            return RegisterFile.ReturnReg(Reg1) - tempImm;
        }

    }
}
