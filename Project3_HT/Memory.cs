using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    public static class Memory
    {
        public static uint[] Mem = new uint[(uint)Math.Pow(2.0, 20.0)];

        public static int ProgramCounter = 0;

        public static void MemPopulate()
        {
            uint byte0, byte1, byte2, byte3;

            for (int i = 0, mc = 0; i < DynamicSim.Actual_Input.Count; i++, mc+= 4)
            {
                byte0 = (uint)DynamicSim.Actual_Input[i] & 0xFF000000;
                Mem[mc] = byte0 >>= 24;

                byte1 = (uint)DynamicSim.Actual_Input[i] & 0x00FF0000;
                Mem[mc + 1] = byte1 >>= 16;

                byte2 = (uint)DynamicSim.Actual_Input[i] & 0x0000FF00;
                Mem[mc + 2] = byte2 >>= 8;

                byte3 = (uint)DynamicSim.Actual_Input[i] & 0x000000FF;
                Mem[mc + 3] = byte3;
            }

            for(int j = 0; j < 30; j++)
            {
                Console.WriteLine(Mem[j]);
            }
        }


    }
}
