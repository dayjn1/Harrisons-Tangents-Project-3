using Project3_HT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Project3_HT
{
    public class AddressUnit
    {
        public static void ProcessAU(Instruction i)
        {
            if (i.OpCode == 1)
            {
                LoadBuffer.AddToBuffer(i);
            }
            else
            {
                //send to reorder buffer whenever that gets done.
            }
        }
    }
}
