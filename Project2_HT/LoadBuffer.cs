using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    public class LoadBuffer
    {
        public static Queue<AUTicket> Buffer = new Queue<AUTicket>();
        public struct AUTicket
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

        }
        public static void AddToBuffer(Instruction i)
        {
            AUTicket myAU = new AUTicket(i.Mnemonic,i.DestReg, i.Reg1);
            Buffer.Enqueue(myAU);
        }
    }
}
