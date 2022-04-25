/*using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    static class FPU
    {
        public static float? FInstructDecomp(Instruction ins)
        {
            switch (ins.OpCode)
            {
                case 128:
                    return FPAdd(ins);
                case 129:
                    return FPAddI(ins);
                case 130:
                    return FPSub(ins);
                case 131:
                    return FPSubI(ins);
                case 132:
                    return FPMult(ins);
                case 133:
                    return FPDiv(ins);
                default:
                    return null;
            }

        }

        public static float FPAdd(Instruction instr)
        {
            return instr.FReg1Data + instr.FReg2Data;
        }

        public static float FPAddI(Instruction instr)
        {
            uint num = uint.Parse(instr.Imm, System.Globalization.NumberStyles.AllowHexSpecifier);

            byte[] floatVals = BitConverter.GetBytes(num);
            float f = BitConverter.ToSingle(floatVals, 0);

            return instr.FReg1Data + f;
        }

        public static float FPSub(Instruction instr)
        {
            return instr.FReg1Data - instr.FReg2Data;
        }

        public static float FPSubI(Instruction instr)
        {
            uint num = uint.Parse(instr.Imm, System.Globalization.NumberStyles.AllowHexSpecifier);

            byte[] floatVals = BitConverter.GetBytes(num);
            float f = BitConverter.ToSingle(floatVals, 0);

            return instr.Reg1Data - f;
        }

        public static float FPMult(Instruction instr)
        {
            return instr.FReg1Data * instr.FReg2Data;
        }

        public static float FPDiv(Instruction instr)
        {
            return instr.FReg1Data / instr.FReg2Data;
        }
    }
}
*/