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
using System.Linq;
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
        public static void ExeCycle()
        {
            foreach (FuncUnit funcUnit in Units)
            {
                if (funcUnit.Instructions.Count > 0 && funcUnit.ExecTime > 0)
                {
                    funcUnit.ExecTime--;
                    funcUnit.Executed = false;

                }
                else
                    funcUnit.Executed = true;
            }
        }

        //step 4 in main sim
        public static void CheckStationsToPushToFuncUnits()
        {/*
            if (LoadBuffer.LdBuffer.Any())
            {
                LoadBuffer.SendToMemUnit();
            }*/

            for (int i=0; i < 3; i++) //check for FPAdders
            {
                ReservationStation rs = RSManager.FPAddRS[i];
                if(!rs.empty && rs.ready && Units[i+1].Instructions.Count == 0)
                {
                    Units[i + 1].Enqueue(rs.currentInst);
                    RSManager.ClearRS(rs);
                }   
            }

            for (int i = 0; i < 3; i++) //check for FPMults
            {
                ReservationStation rs = RSManager.FPMultRS[i];
                if (!rs.empty && rs.ready && Units[i + 4].Instructions.Count == 0)
                {
                    Units[i + 4].Enqueue(rs.currentInst);
                    RSManager.ClearRS(rs);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                ReservationStation rs = RSManager.IntegerRS[i];
                if (!rs.empty && rs.ready && Units[i + 7].Instructions.Count == 0)
                {
                    Units[i + 7].Enqueue(rs.currentInst);
                    RSManager.ClearRS(rs);
                }
            }

        }

    }
}
