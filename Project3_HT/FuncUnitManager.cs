// ---------------------------------------------------------------------------
// File name:                   FuncUnits.cs
// Project name:                Project 3 - Harrison's Tangents
// Developers:                  Jason Middlebrook, Hannah Taylor
// Course-Section:              CSCI 4717-201
// Creation Date:               04/02/2022
// 
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    internal static class FuncUnitManager
    {
        public static List<FuncUnit> Units = new List<FuncUnit>()
        {
            new FuncUnit("MemoryUnit", 1),
            new FuncUnit("FPAdder", 1),  // change needed
            new FuncUnit("FPAdder", 1),
            new FuncUnit("FPAdder", 1),
            new FuncUnit("FPMultiplier", 1),
            new FuncUnit("FPMultiplier", 1),
            new FuncUnit("FPMultiplier", 1),
            new FuncUnit("IntegerUnit", 1),
            new FuncUnit("IntegerUnit", 1),
            new FuncUnit("IntegerUnit", 1)
        };

        public static int Count {
            get { return Units.Count; }
        }

        public static FuncUnit At(int index)
        {
            return Units[index];
        }

    }
}
