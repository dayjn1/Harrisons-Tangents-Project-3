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
        public static int? InstructDecomp(Instruction instr)
        {
            if (instr.OpCode == 5)
                return ADD(instr.Reg1, instr.Reg2);
            else if (instr.OpCode == 6)
                return ADDI(instr.Reg1, instr.Imm);
            else if (instr.OpCode == 7)
                return SUB(instr.Reg1, instr.Reg2);
            else if (instr.OpCode == 8)
                return SUBI(instr.Reg1, instr.Imm);
            else if (instr.OpCode == 22)
                return MOV(instr.Imm);

            return null;
        }

        public static int MOV(string Imm)
        {
            Int32.TryParse(Imm, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int tempImm);
            return tempImm;
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
