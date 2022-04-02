using System;
using Project3_HT;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project3_HT.LoadBuffer;

public class MemoryUnit
{
    // takes dest and sourse regstrs
	static Dictionary<String, String> MemoryEntries = new Dictionary<String, String>();
	public static void AddToMemUnit(Instruction instruction)
    {
        MemoryEntries.Add(instruction.DestReg, instruction.Reg1);
    }
    public static void Clear()
    {
        MemoryEntries.Clear();
    }
    public static void SendToCDB()
    {
        //will be implemented when CDB gets done
    }
}
