// ---------------------------------------------------------------------------
// File name:                   Memory.cs
// Project name:                Project 4 - Harrison's Tangents
// ---------------------------------------------------------------------------
// Creator’s name:              Janine Day
// Course-Section:              CSCI-4717
// Creation Date:               04/14/2022
// ---------------------------------------------------------------------------

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

        public static int ReservedMem = 0;

        public static void MemPopulate(int i)
        {
            uint byte0, byte1, byte2, byte3;

            byte0 = (uint)i & 0xFF000000;
            Mem[ReservedMem] = byte0 >>= 24;

            byte1 = (uint)i & 0x00FF0000;
            Mem[ReservedMem + 1] = byte1 >>= 16;

            byte2 = (uint)i & 0x0000FF00;
            Mem[ReservedMem + 2] = byte2 >>= 8;

            byte3 = (uint)i & 0x000000FF;
            Mem[ReservedMem + 3] = byte3;

            ReservedMem += 4;
        }

        public static int LoadInstr(uint addr)
        {
            return (int)Mem[addr];
        }

        public static void StoreInstr(uint addr, int data)
        {
            Mem[addr] = (uint)data;
        }


    }
}
