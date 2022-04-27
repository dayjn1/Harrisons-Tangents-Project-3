using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3_HT
{
    public partial class MemoryDump : Form
    {
        public MemoryDump()
        {
            InitializeComponent();
            StringBuilder Mem = new StringBuilder();

            for (int i = 0; i < 100; i++)
            {
                Mem.Append(Convert.ToString(Memory.Mem[i], 16));
            }

            this.MemDump.Text = Mem.ToString();

            Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            StringBuilder Mem = new StringBuilder();

            for(int i = 0; i < 10; i++)
            {
                Mem.Append(Memory.Mem[i].ToString());
            }

            this.MemDump.Text = Mem.ToString();
        }
    }
}
