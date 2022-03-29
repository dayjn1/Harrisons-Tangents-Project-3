using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    class ReorderBuffer
    {
        public Queue<Instruction> ReorderBuf = new Queue<Instruction>();
        public List<System.Windows.Forms.Label> Labels = new List<System.Windows.Forms.Label>();
        public void AddToReorderBuf(Instruction i)
        {
            ReorderBuf.Enqueue(i);
        }

        public void ChangeText()
        {
            Instruction[] temp = ReorderBuf.ToArray();
            
            for(int i = 0; i < temp.Length; i++)
            {

            }
        }
    }
}
