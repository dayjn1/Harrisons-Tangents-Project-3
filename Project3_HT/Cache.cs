///Created by Jason Middlebrook

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    internal class Cache
    {
        public int SetAssociativity { get; set; }
        public int TotalSize { get; set; }
        public int[,] CacheArray { get; set; }

        /// <summary>
        /// Non-default constructor if you want to specify properties
        /// </summary>
        public Cache(int setAssociativity, int totalSize)
        {
            SetAssociativity = setAssociativity;
            TotalSize = totalSize;
            CacheArray = new int[totalSize / setAssociativity, setAssociativity];

            //initialize all cache entries as empty / invalid
            for (int i = 0; i < totalSize / setAssociativity; i++)
            {
                for (int j = 0; j < setAssociativity; j++)
                {
                    CacheArray[i,j] = -1;
                }
            }
        }//end Cache(int, int)

        /// <summary>
        /// Default property values assigned based on what kind of cache we decided on
        /// </summary>
        public Cache()
        {
            SetAssociativity = 4;
            TotalSize = 16;

            //Create a 2d array; first level is the rows (indices), second is the columns (sets)
            //Cache will have as many sets per row as the set associativity
            CacheArray = new int[TotalSize/SetAssociativity, SetAssociativity];

            //initialize all cache entries as empty / invalid
            for (int i = 0; i < TotalSize / SetAssociativity; i++)
            {
                for (int j = 0; j < SetAssociativity; j++)
                {
                    CacheArray[i, j] = -1; //Change to have invalid bit instead of int -1
                }
            }
        }//end Cache()

        /// <summary>
        /// FOR IMMEDIATE LOADS AND STORES -jfm
        /// Uses Reg1 to as the offset and Imm (16 bits) as the index and tag
        /// Hardcoded to be with a 4-way set associative cache (16 entries total)
        /// </summary>
        public void DeconstructInstruction(Instruction instr)
        {
            //Use substring to move past "R" in instr properties
            uint offset = UInt32.Parse(instr.Reg1.Substring(1));          //Offset is least significant 4 bits

            uint index = UInt32.Parse(instr.Imm.Substring(1)) & 0x000F;           //Index is last two bits
            index = index >> 2;

            uint tag = UInt32.Parse(instr.Imm.Substring(1)) & 0xFFFF;             //Tag is 3.5 nibbles
            tag = (tag & 0b1111111111111100) >> 2;          //Starts at 2nd least significant bit to accomodate index

            //Put all this info into the entry in the cache
            Console.WriteLine("Offset: " + offset.ToString("X"));
            Console.WriteLine("Index: " + index.ToString("X"));
            Console.WriteLine("Tag: " + tag.ToString("X"));


        }//end DeconstructInstruction(Instruction)

        /// <summary>
        /// Checks to see if entry with the given tag and index is in the cache
        /// WIP -jfm
        /// </summary>
        public bool Check(int index, int tag)
        {

            bool hit = false;
            for (int i = 0; i < SetAssociativity; i++)
            {
                if (CacheArray[index, i] == tag)
                {
                    return true;
                }
            }

            return hit;
        }//end Check(int, int)

        public void Replace(int tag, int index)
        {
            return;
        }

    }//end class Cache
}
