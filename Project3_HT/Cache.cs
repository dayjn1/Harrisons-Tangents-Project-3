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
                    CacheArray[i, j] = -1;
                }
            }
        }//end Cache()

        /// <summary>
        /// Checks to see if entry with the given tag and index is in the cache
        /// </summary>
        public bool Check(int tag, int index)
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
