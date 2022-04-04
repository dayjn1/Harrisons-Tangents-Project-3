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
            new FuncUnit("MemoryUnit"),
            new FuncUnit("FPAdder"),  // change needed
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

        /// <summary>
        /// Execution takes one cycle for each instruction
        /// </summary>
        public static void Cycle(int posInList)
        {
            foreach (FuncUnit funcUnit in Units)
            {
                if (funcUnit.Instructions.Count > 0 && funcUnit.ExecTime > 0)
                {
                    Units[posInList].ExecTime--;
                    funcUnit.Executed = false;

                }
                else
                    funcUnit.Executed = true;
            }
        }

    }
}
