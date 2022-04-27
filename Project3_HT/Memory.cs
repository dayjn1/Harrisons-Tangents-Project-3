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
    /**
    * Class Name:       Memory
    * Class Purpose:    Handles all memory operations and contains memory array
    *
    * <hr>
    * Date created:     04/14/2022
    * @Janine Day
    */
    public static class Memory
    {
        public static uint[] Mem = new uint[(uint)Math.Pow(2.0, 20.0)];

        public static int ReservedMem = 0;


        /**
        * Method Name:    MemPopulate(int)
        * Method Purpose: Populates the inputted program in memory
        *
        * <hr>
        * Date created: 04/14/2022
        * @Janine Day
        * <hr>
        * @param int, instruction hex code
        */
        public static void MemPopulate(int i)
        {
            uint byte0, byte1, byte2, byte3;

            byte0 = (uint)i & 0xFF000000;           // isolates most sig bits, shifts to least sig and saves it
            Mem[ReservedMem] = byte0 >>= 24;

            byte1 = (uint)i & 0x00FF0000;
            Mem[ReservedMem + 1] = byte1 >>= 16;    // continues isolating bits until none left

            byte2 = (uint)i & 0x0000FF00;
            Mem[ReservedMem + 2] = byte2 >>= 8;

            byte3 = (uint)i & 0x000000FF;
            Mem[ReservedMem + 3] = byte3;

            ReservedMem += 4;                       // increments so we don't overwrite something
        }//end MemPopulate(int)


        /**
        * Method Name:    LoadInstr(uint)
        * Method Purpose: Takes an address and returns the value saved at that location
        *
        * <hr>
        * Date created: 04/14/2022
        * @Janine Day
        * <hr>
        * @param uint, address
        * @return int, load result
        */
        public static int LoadInstr(uint addr)
        {
            uint temp = 0x00000000, load = 0x00000000;

            for(int i = 0; i < 4; i++)      // performs a load word
            {
                temp = Mem[addr + i];
                temp <<= (24 - i * 8);
                load += temp;
            }//end for

            return (int)load;
        }//end LoadInstr(uint)

        /**
        * Method Name:    StoreInstr(uint, int)
        * Method Purpose: Takes an address and  the value saved at that location
        *
        * <hr>
        * Date created: 04/14/2022
        * @Janine Day
        * <hr>
        * @param uint, address
        * @param int, data
        */
        public static void StoreInstr(uint addr, int data)
        {
            uint byte0, byte1, byte2, byte3;

            byte0 = (uint)data & 0xFF000000;        // performs a store word
            Mem[addr] = byte0 >>= 24;

            byte1 = (uint)data & 0x00FF0000;
            Mem[addr + 1] = byte1 >>= 16;

            byte2 = (uint)data & 0x0000FF00;
            Mem[addr + 2] = byte2 >>= 8;

            byte3 = (uint)data & 0x000000FF;
            Mem[addr + 3] = byte3;
        }//end StoreInstr(uint, int)
    }//end Memory
}//end Project3_HT
