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
        public CacheEntry[,] CacheArray { get; set; }

        /// <summary>
        /// Non-default constructor if you want to specify properties
        /// </summary>
        public Cache(int setAssociativity, int totalSize)
        {
            SetAssociativity = setAssociativity;
            TotalSize = totalSize;
            CacheArray = new CacheEntry[totalSize / setAssociativity, setAssociativity];

            //initialize all cache entries as empty / invalid
            for (int i = 0; i < totalSize / setAssociativity; i++)
            {
                for (int j = 0; j < setAssociativity; j++)
                {
                    CacheArray[i,j] = new CacheEntry();
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
            CacheArray = new CacheEntry[TotalSize/SetAssociativity, SetAssociativity];

            //initialize all cache entries as empty / invalid
            for (int i = 0; i < TotalSize / SetAssociativity; i++)
            {
                for (int j = 0; j < SetAssociativity; j++)
                {
                    CacheArray[i, j] = new CacheEntry(); //Change to have invalid bit instead of int -1
                }
            }
        }//end Cache()

        /// <summary>
        /// FOR IMMEDIATE LOADS AND STORES -jfm
        /// Uses Reg1 to as the offset and Imm (16 bits) as the index and tag
        /// Hardcoded to be with a 4-way set associative cache (16 entries total)
        /// </summary>
        public CacheEntry DeconstructInstruction(Instruction instr)
        {
            //Use substring to move past "R" in instr properties
            uint offset = instr.Address;          //Offset 4 bits (taken from R1)

            uint index = instr.Address & 0x000F;   //Index is last two bits
            index = (index & 0b_0000_0000_0000_0011);

            uint tag = instr.Address & 0xFFFF;     //Tag is 3.5 nibbles
            tag = (tag & 0b_11_1111_1111_1111_00) >> 2;                   //Starts at 2nd least significant bit to accomodate index
            //instr.Address    we should use this
            CacheEntry ce = new CacheEntry(offset, index, tag);

            //Put all this info into the entry in the cache
            Console.WriteLine("Tag and Index x: " + UInt32.Parse(instr.Imm).ToString());
            Console.WriteLine("Offset: " + offset.ToString("X"));
            Console.WriteLine("Index: " + index.ToString("X"));
            Console.WriteLine("Tag x: " + tag.ToString("X"));
            Console.WriteLine("Tag d: " + tag);
			return ce;

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
                if (CacheArray[index,i].tag == tag)
                {
                    return true;
                }
            }

            return hit;
        }//end Check(int, int)
        //entry for storing cache data more easily
        //AM
        public struct CacheEntry
        {
            public uint offset;
            public uint index;
            public uint tag;
            public bool valid;
            public CacheEntry(uint offset, uint index, uint tag)
            {
                this.offset = offset;
                this.index = index;
                this.tag = tag;
                valid = false;
            }
        }

        public void Replace(int tag, int index)
        {
            return;
        }

    }//end class Cache
}
