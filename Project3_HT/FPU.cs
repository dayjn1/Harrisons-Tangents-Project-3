using Project3_HT;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_HT
{
    class FPU
    {
        public static float? FInstructDecomp(Instruction ins)
        {
        switch(ins.OpCode)
            {
                case 128:
                    return FADD(ins);
                case 129:
                    return FADDI(ins);
                case 130:
                    return FSUB(ins);
                case 131:
                    return FSUBI(ins);
                case 132:
                    return FMULT(ins);
                case 133:
                    return FDIV(ins);
                default:
                    return null;
               }
                
        }
        public static float FADD(Instruction ins)
        {
            return ins.FReg1Data + ins.FReg2Data;
        }
        public static float FADDI(Instruction instr)
        {
            float.TryParse(instr.Imm, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out float tempImm);
            return instr.FReg1Data + tempImm;
        }
        public static float FSUB(Instruction ins)
        {
            return ins.FReg1Data - ins.FReg2Data;
        }
        public static float FSUBI(Instruction ins)
        {
            float.TryParse(ins.Imm, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out float tempImm);
            return ins.Reg1Data - tempImm;
        }
        public static float FMULT(Instruction ins)
        {
            return ins.FReg1Data * ins.FReg2Data;

        }
        public static float FDIV(Instruction ins)
        {
            return ins.FReg1Data * ins.FReg2Data;
        }
    }
}
