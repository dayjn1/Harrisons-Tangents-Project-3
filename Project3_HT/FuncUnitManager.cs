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
            new MemUnit("MemoryUnit"),
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

        public static bool checkAllEmpty()
        {
            bool allClear = true;

            for (int f = 0; f < Units.Count; f++)
            {
                if (Units[f].Empty)
                    allClear = true;
                else
                {
                    allClear = false;
                    break;
                }

            }
            return allClear;
        }
        /// <summary>
        /// Execution takes one cycle for each instruction
        /// </summary>
        public static void ExeCycle()
        {
            foreach (FuncUnit funcUnit in Units)
            {
                bool processed = false;     // used to check if mem instruction has been processed
                if (funcUnit.Instructions.Count > 0 && funcUnit.ExecTime > 0)
                {
                    funcUnit.ExecTime--;
                    funcUnit.Executed = false;

                    if (processed == false)
                    {
                        if (funcUnit.Instructions.Peek().OpCode == 1 || funcUnit.Instructions.Peek().OpCode == 3)   //Load
                        {
                            Instruction temp = funcUnit.Instructions.Dequeue();
                            if (Cache.Check(temp))       //If there is a cache hit
                            {
                                //temp.Result = Cache.LoadInstr(temp.Address);
                                funcUnit.Instructions.Enqueue(temp);
                            }
                            else                                                    //Cache miss; load from mem and put in cache -jfm
                            {
                                temp.Result = Memory.LoadInstr(temp.Address);
                                Cache.Add(temp);                                 //Attempt to put in the cache, including replacement if necessary -jfm
                                temp.ExecuteCC *= 5;
                                funcUnit.Instructions.Enqueue(temp);
                            }
                            processed = true;

                        }//end if load
                        else if (funcUnit.Instructions.Peek().OpCode == 2 || funcUnit.Instructions.Peek().OpCode == 4)  //Store
                        {
                            Instruction temp = funcUnit.Instructions.Dequeue();
                            if (Cache.Check(temp))       //If there is a cache hit, store to cache and mem, otherwise just mem -jfm
                            {
                                Cache.Add(temp);                                 //Store to cache and memory -jfm
                            }
                            Memory.StoreInstr(funcUnit.Instructions.Peek().Address, RegisterFile.ReturnReg(funcUnit.Instructions.Peek().DestReg));
                            // Need to make a method in reg file to return contents of given register
                            funcUnit.Instructions.Enqueue(temp);
                            processed = true;
                        }//end if store
                    }
                }
                else
                {
                    funcUnit.Executed = true;

                }

            }

        }//end ExeCycle()

        //step 4 in main sim
        public static void CheckStationsToPushToFuncUnits()
        {
            /*if (LoadBuffer.LdBuffer.Any())
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

        }//end CheckStationsToPushToFuncUnits

        /// <returns>total is the total amound of instructions in all functional units, waiting or otherwise</returns>
        public static int TotalInstrCount()
        {
            int total = 0;
            foreach (FuncUnit fu in Units)
            {
                total += fu.Instructions.Count;
            }
            return total;
        }//end TotalExecuting

    }
}
