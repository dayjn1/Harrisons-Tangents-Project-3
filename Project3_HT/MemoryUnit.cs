using System;
using Project3_HT;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project3_HT.LoadBuffer;

public static class MemoryUnit
{
    // takes dest and sourse regstrs
    // One per cycle 
    // Getting data from memory in one cycle 
	static Dictionary<String, String> MemoryEntries = new Dictionary<String, String>();
	public static void AddToMemUnit(Instruction instruction)
    {
        MemoryEntries.Add(instruction.DestReg, instruction.Reg1);
    }
    public static void Clear()
    {
        MemoryEntries.Clear();
    }
    // We need an actual instruction 
    public static void SendToCDB(Instruction i)
    {
        CDBus.ReceiveResults();
    }

    public static void Dequeue()
    {
        return;
    }
}
