﻿using Project3_HT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3_HT
{
    internal class MemUnit : FuncUnit
    {

        public MemUnit(string name) : base(name)
        {
            // call the cache form -- moved this to the main simulation
            /*CacheFourWay cacheForm = new CacheFourWay();
            cacheForm.Show();*/
        }
       
        public static CacheEntry AddressToLookUp(Instruction instruction)
        {
            CacheEntry temp = Cache.DeconstructInstruction(instruction);
            return temp;
        }

        public static void ProcessCacheAccess (Instruction instr)
        {
            bool processed = false;     // used to check if mem instruction has been processed

            //uint currentInstAddressInMU = FuncUnitManager.Units[0].Instructions.Peek().Address;

            if(!processed)
            {
                if(FuncUnitManager.Units[0].Instructions.Peek().OpCode == 1 || FuncUnitManager.Units[0].Instructions.Peek().OpCode == 3)
                { //loads (LOAD and LOADI)
                    Instruction temp = FuncUnitManager.Units[0].Instructions.Dequeue();
                    int[] tempPos = Cache.Check(instr);
                    if(tempPos[0] == -1)
                    {
                        //missed  --- need to add in conditions for diff types of misses (and update
                        temp.Result = Memory.LoadInstr(temp.Address);
                        Cache.Add(temp);
                        temp.MemoryCC *= 5;
                        FuncUnitManager.Units[0].Instructions.Enqueue(temp);
                        processed = true;
                        //will need to update the labels for the FourWayCacheForm labels
                        //return to DynamicSim and then call a method in CacheFourWay to update labels, pause runtime, then hide cache form again
                    }
                    else //hit on load
                    {
                        //get data from position
                        int curData = Cache.CacheArray[tempPos[0], tempPos[1]].data;
                        temp.MemoryCC += 3;
                        FuncUnitManager.Units[0].Instructions.Enqueue(temp);
                        processed = true;

                    }
                }
                else if (FuncUnitManager.Units[1].Instructions.Peek().OpCode == 2 || FuncUnitManager.Units[0].Instructions.Peek().OpCode == 4) //stores
                {

                }
            }
        }

        /* public override void Enqueue(Instruction instr)
         {
             if (Instructions.Count > 0)
             {
                 throw new InvalidOperationException("Functional units can only execute one Instruction per cycle." +
                     "\nPlease Dequeue the instruction or stall execution.");
             }
             Instructions.Enqueue(instr);
             ExecTime = CalcExecutionTime(instr);

             Empty = false;
         }*/




    }
}
