// ---------------------------------------------------------------------------
// File name:                   FuncUnits.cs
// Project name:                Project 2 - Harrison's Tangents
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
    internal static class FuncUnits
    {
        public static List<FuncUnit> Units = new List<FuncUnit>()
        {
            new FuncUnit("MemoryUnit"),
            new FuncUnit("FPAdder"),
            new FuncUnit("FPAdder"),
            new FuncUnit("FPAdder"),
            new FuncUnit("FPMultiplier"),
            new FuncUnit("FPMultiplier"),
            new FuncUnit("FPMultiplier"),
            new FuncUnit("IntegerUnit"),
            new FuncUnit("IntegerUnit"),
            new FuncUnit("IntegerUnit")
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
