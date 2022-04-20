///Created by Jason Middlebrook

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
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
            // for future working address unit, if Address does not return 0x67890
            //instr.Address = uint.Parse(instr.Address.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier); 

            // for testing DELETE LATER
            instr.Address = 13579;
            instr.Address = uint.Parse(instr.Address.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
           
            uint offset = (instr.Address & 0x0000F);            //Offset 4 bits 
            
            uint index = instr.Address & 0x000F0;               //Index is two bits after the offset
            index = (index & 0b_0000_0000_0000_0011_0000) >> 4;

            uint tag = instr.Address & 0xFFFF0;                 //Tag is 3.5 nibbles
            tag = (tag & 0b_1111_1111_1111_1100_0000) >> 6;     //Starts at 6th least significant bit to accomodate for offset and index
            
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

        /// Returns whether there is a hit in the cache or not
        public bool Check(Instruction instr)
        {
            CacheEntry ce = DeconstructInstruction(instr);

            bool hit = false;
            for (int i = 0; i < SetAssociativity; i++)
            {
                if (CacheArray[ce.index, i].tag == ce.tag)
                {
                    return true;
                }
            }

            return hit;
        }//end Check(Instruction)

        //Adds a cache entry to the cache, calls replacement if necessary
        public void Add(Instruction instr)
        {
            CacheEntry ce = DeconstructInstruction(instr);

            for (int i = 0; i < SetAssociativity; i++)
            {
                if (CacheArray[ce.index, i].valid == false)
                {
                    CacheArray[ce.index, i] = ce;
                }
                else
                {
                    Replace(ce);
                }
            }
        }

        /// <summary>
        /// Method for replacement algorithm
        /// </summary>
        /// <param name="ce"></param>
        public void Replace(CacheEntry ce)
        {
            return;
        }

    }//end class Cache
}
