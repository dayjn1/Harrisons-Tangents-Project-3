﻿///Created by Jason Middlebrook

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    //entry for storing cache data more easily -AM
    public struct CacheEntry
    {
        public uint offset;
        public uint index;
        public uint tag;
        public bool valid;
        public CacheEntry(uint offset, uint index, uint tag, bool empty = true)
        {
            this.offset = offset;
            this.index = index;
            this.tag = tag;
            valid = !empty;
        }

        public override string ToString()
        {
            string retString = "";
            retString += "Offset: " + offset.ToString("X");
            retString += "Index: " + index.ToString("X");
            retString += "Tag x: " + tag.ToString("X");
            retString += "Tag d: " + tag;
            return retString;
        }
    }

    internal static class Cache
    {
        public static int SetAssociativity { get; set; }
        public static int TotalSize { get; set; }
        public static CacheEntry[,] CacheArray { get; set; }

        static Cache()
        {
            SetAssociativity = 4;
            TotalSize = 16;

            //Create a 2d array; first level is the rows (indices), second is the columns (sets)
            //Cache will have as many sets per row as the set associativity
            CacheArray = new CacheEntry[TotalSize / SetAssociativity, SetAssociativity];

            //initialize all cache entries as empty / invalid
            for (int i = 0; i < TotalSize / SetAssociativity; i++)
            {
                for (int j = 0; j < SetAssociativity; j++)
                {
                    CacheArray[i, j] = new CacheEntry();
                }
            }
        }//end Cache()

        /// <summary>
        /// FOR IMMEDIATE LOADS AND STORES -jfm
        /// Uses Reg1 to as the offset and Imm (16 bits) as the index and tag
        /// Hardcoded to be with a 4-way set associative cache (16 entries total)
        /// </summary>
        public static CacheEntry DeconstructInstruction(Instruction instr)
        {
            uint offset = (instr.Address & 0x0000F);            //Offset 4 bits 
            
            uint index = instr.Address & 0x000F0;               //Index is two bits after the offset
            index = (index & 0b_0000_0000_0000_0011_0000) >> 4;

            uint tag = instr.Address & 0xFFFF0;                 //Tag is 3.5 nibbles
            tag = (tag & 0b_1111_1111_1111_1100_0000) >> 6;     //Starts at 6th least significant bit to accomodate for offset and index

            //Put all this info into an entry that can go in the cache
			return new CacheEntry(offset, index, tag, false);
        }//end DeconstructInstruction(Instruction)

        /// Checks to see if entry with the given tag and index is in the cache -jfm
        public static bool Check(int index, int tag)
        {
            for (int i = 0; i < SetAssociativity; i++)
            {
                if (CacheArray[index,i].tag == tag)
                {
                    return true;
                }
            }

            return false;
        }//end Check(int, int)

        /// Returns whether there is a hit in the cache or not for an instruction -jfm
        public static bool Check(Instruction instr)
        {
            CacheEntry ce = DeconstructInstruction(instr);

            for (int i = 0; i < SetAssociativity; i++)
            {
                if (CacheArray[ce.index, i].tag == ce.tag)
                {
                    return true;
                }
            }

            return false;
        }//end Check(Instruction)

        /// Adds a cache entry to the cache, calls replacement if necessary -jfm
        public static void Add(Instruction instr)
        {
            CacheEntry ce = DeconstructInstruction(instr);

            for (int i = 0; i < SetAssociativity; i++)          //find empty place in set
            {
                if (CacheArray[ce.index, i].valid == false)
                {
                    CacheArray[ce.index, i] = ce;
                    return;
                }
            }
            //If all entries in the set are valid, we need to replace an entry
            Console.WriteLine("Replacing");
            Replace(ce);
        }

        /// <summary>
        /// Method for FIFO replacement algorithm -jfm
        /// </summary>
        /// <param name="ce">Cache entry that we need to put into the Cache</param>
        public static void Replace(CacheEntry ce)
        {
            for (int i = 0; i < SetAssociativity - 1; i++)              //Kick out oldest entry and move all entries left
            {
                CacheArray[ce.index, i] = CacheArray[ce.index, i + 1];
            }
            CacheArray[ce.index, SetAssociativity - 1] = ce;            //replace end of set with new entry
        }

        public static string ToString()
        {
            string retString = "";
            for (int i = 0; i < (TotalSize / SetAssociativity); i++)
            {
                retString += "Index " + i + ":";
                for (int j = 0; j < SetAssociativity; j++)
                {
                    retString += " " + CacheArray[i, j].tag.ToString("X");
                }
                retString += "\n";
            }
            return retString;
        }

    }//end class Cache
}
