using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MemoryUnit;
namespace Project3_HT
{
    public class LoadBuffer
    {
        public static Queue<Instruction> LdBuffer = new Queue<Instruction>();
        /*  public struct AUTicket
          {
              public string Name;
              public String DestReg;
              public String SourceReg;
              public AUTicket(String name,String dr, String sr)
              {
                  Name = name;
                  DestReg = dr;
                  SourceReg = sr;
              }
          }*/
        public static void AddToLoadBuffer(Instruction i)
        {
            //AUTicket myAU = new AUTicket(i.Mnemonic,i.DestReg, i.Reg1);
            LdBuffer.Enqueue(i);
        }
        public static void SendToMemUnit()
        {
            AddToMemUnit(LdBuffer.Dequeue());
        }
    }
}