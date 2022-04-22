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
                return ADD(instr);
            else if (instr.OpCode == 6)
                return ADDI(instr);
            else if (instr.OpCode == 7)
                return SUB(instr);
            else if (instr.OpCode == 8)
                return SUBI(instr);
            else if (instr.OpCode == 16)
                return AND(instr);
            else if (instr.OpCode == 17)
                return OR(instr);
            else if (instr.OpCode == 18)
                return NEG(instr);
            else if (instr.OpCode == 19)
                return ASL(instr);
            else if (instr.OpCode == 20)
                return ASR(instr);
            else if (instr.OpCode == 21)
                return MOV(instr);

            return null;
        }

        public static int MOV(Instruction instr)
        {
            Int32.TryParse(instr.Imm, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int tempImm);
            return tempImm;
        }

        public static int ADD(Instruction instr)
        {
            return instr.Reg1Data + instr.Reg2Data;
        }

        public static int ADDI(Instruction instr)
        {
            Int32.TryParse(instr.Imm, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int tempImm);
            return instr.Reg1Data + tempImm;
        }

        public static int SUB(Instruction instr)
        {
            return instr.Reg1Data - instr.Reg2Data;
        }

        public static int SUBI(Instruction instr)
        {
            Int32.TryParse(instr.Imm, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int tempImm);
            return instr.Reg1Data - tempImm;
        }

        public static int AND(Instruction instr)
        {
            return instr.Reg1Data & instr.Reg2Data;
        }

        public static int OR(Instruction instr)
        {
            return instr.Reg1Data | instr.Reg2Data;
        }


        public static int NEG(Instruction instr)
        {
            return ~instr.Reg1Data;
        }

        public static int ASL(Instruction instr)
        {
            return instr.Reg1Data << instr.Reg2Data;
        }

        public static int ASR(Instruction instr)
        {
            return instr.Reg1Data >> instr.Reg2Data;
        }

    }
}
