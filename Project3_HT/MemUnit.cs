using Project3_HT;
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

        public static Cache.MissType ProcessCacheAccess ()
        {

            //uint currentInstAddressInMU = FuncUnitManager.Units[0].Instructions.Peek().Address;
            int[] tempPos;

            if(!FuncUnitManager.Units[0].Empty)
            {
                if (FuncUnitManager.Units[0].Instructions.Peek().OpCode == 1 || FuncUnitManager.Units[0].Instructions.Peek().OpCode == 3)
                { //loads (LOAD and LOADI)
                    Instruction temp = FuncUnitManager.Units[0].Instructions.Dequeue();
                    tempPos = Cache.Check(temp);
                    if (tempPos[0] == -1)
                    {
                        temp.Result = Memory.LoadInstr(temp.Address);
                        Cache.Add(temp);
                        DynamicSim.cacheForm.UpdateCache();
                        FuncUnitManager.Units[0].ExecTime *= 5;
                        //temp.MemoryCC *= 5;
                        FuncUnitManager.Units[0].Instructions.Enqueue(temp);
                        //Send miss type up to DynamicSim
                        return (Cache.MissType)tempPos[1];                          //tempPos contains our enum specifying the type of miss
                    }
                    else //hit on load
                    {
                        //get data from position
                        // update the 
                        int curData = Cache.CacheArray[tempPos[0], tempPos[1]].data;
                        FuncUnitManager.Units[0].ExecTime += 3;
                        //temp.MemoryCC += 3;
                        FuncUnitManager.Units[0].Instructions.Enqueue(temp);

                    }
                    FuncUnitManager.Units[0].Processed = true;
                }
            }
            if (!FuncUnitManager.Units[1].Empty)
            { 
                if (FuncUnitManager.Units[1].Instructions.Peek().OpCode == 2 || FuncUnitManager.Units[1].Instructions.Peek().OpCode == 4) //stores
                {
                    Instruction temp = FuncUnitManager.Units[1].Dequeue();

                    FuncUnitManager.Units[1].ExecTime *= 5;
                    //temp.MemoryCC *= 5;
                    tempPos = Cache.Check(temp);
                   
                    if (tempPos[0] == -1) //write miss
                    {
                        Memory.StoreInstr(temp.Address, RegisterFile.ReturnRegData(temp.DestReg));
                        FuncUnitManager.Units[1].Instructions.Enqueue(temp);
                        return (Cache.MissType)tempPos[1];                          //tempPos contains our enum specifying the type of miss
                    }
                    else
                    {
                        Cache.Add(temp);
                        Memory.StoreInstr(temp.Address, RegisterFile.ReturnRegData(temp.DestReg));
                    }
                    FuncUnitManager.Units[1].Processed = true;
                    FuncUnitManager.Units[1].Instructions.Enqueue(temp);
                    DynamicSim.cacheForm.UpdateCache();
                }
            }
            return 0;
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
