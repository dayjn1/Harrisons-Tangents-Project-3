///Created by Jason Middlebrook
///edited by Hannah Taylor & Nataliya Chibizova

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
        public int data;
        public bool empty;
        public CacheEntry(uint offset, uint index, uint tag, int data, bool isEmpty = true, bool emptyData = true)
        {
            this.offset = offset;
            this.index = index;
            this.tag = tag;
            valid = emptyData;
            this.data = data;
            empty = isEmpty;
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
        public enum MissType
        {
            Compulsory = 1,
            Conflict = 2,
            Capacity = 3
        }

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
                    CacheArray[i, j] = new CacheEntry(0,0,0,0,true,true);
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

            int data = Memory.LoadInstr(instr.Address);
                                                                //Put all this info into an entry that can go in the cache
            return new CacheEntry(offset, index, tag, data, false);  // move data to add 
                                                                        // take care of the data here, to be null?
        }//end DeconstructInstruction(Instruction)

      
        // Returns location in CacheArray that we hit, or (-1, x) if miss -jfm
        // Miss: x = 1 if compulsory, 2 if conflict, 3 if capacity (epic miss)
        public static int[] Check(Instruction instr)
        {
            CacheEntry ce = DeconstructInstruction(instr);
            int hit_entry = -1;
            bool any_entry_empty = false;

            for (int i = 0; i < SetAssociativity; i++)
            {
                if (CacheArray[ce.index, i].tag == ce.tag)
                {
                    hit_entry = i;
                    break;
                }
                if (CacheArray[ce.index, i].empty == true)
                {
                    any_entry_empty = true;
                    break;
                }
            }//end for(entry in set)
            if(hit_entry == -1 && any_entry_empty)              ///Compulsory if any entry is empty and we miss
            {
                return new int[] { (int)ce.index, (int)MissType.Compulsory };
            }
            else if(hit_entry != -1)                            //If we hit, return location of hit
            {
                return new int[] { (int)ce.index, hit_entry };
            }

            //Check every single entry in the cache to test Capacity miss
            bool capacity_miss = true;
            for (int j = 0; j < TotalSize / SetAssociativity; j++)
            {
                for (int k = 0; k < SetAssociativity; k++)
                {
                    if (CacheArray[j, k].empty == true || CacheArray[j, k].valid == false)
                    {
                        capacity_miss = false;
                    }
                }
            }//end for(every entry in the cache)

            if (capacity_miss)
            {
                return new int[] { -1, (int)MissType.Capacity };                     //Capacity miss
            }
            else
            {
                return new int[] { -1, (int)MissType.Conflict };                     //Conflict miss
            }

        }//end Check(Instruction)

        /// Adds a cache entry to the cache, calls replacement if necessary -jfm
        public static void Add(Instruction instr)
        {
            CacheEntry ce = DeconstructInstruction(instr);
            //int data = (int)instr.Result;                                      // not even used

            for (int i = 0; i < SetAssociativity; i++)          //find empty place in set
            {
                if (CacheArray[ce.index, i].valid == false)
                {
                    CacheArray[ce.index, i] = ce;
                    CacheArray[ce.index, i].empty = false;
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


    /*public static uint GetData()
    {
        //return the data from the entry in cache that was hit in check
    }*/
}

/*
 Cache Flow:
    0. Memory Unit calls cache to see if it holds data
        -- on hit, return the data to mem unit
        -- on miss
            --> go to main memory and find the data
            --> return it to the cache and update the cache
            --> then return the data back to the mem unit
    1. Update Cache When (Write Through Approach)
        -- 
    2. Replace (FIFO)
        -- keep track of order placed in cache
 
 
 */