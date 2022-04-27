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
            char temp;
            string ascii = "";
            for (int i = 0; i < 1000; i+= 10)
            {
                Mem.Append(Convert.ToString(i, 16).PadLeft(5, '0') + "\t");

                for (int j = 0; j < 10; j++)
                {
                    Mem.Append(Convert.ToString(Memory.Mem[i + j], 16).PadLeft(3) + " ");

                    temp = Convert.ToChar(Memory.Mem[i + j]);
                    if (temp.ToString() == "" || temp.Equals('\0'))
                        ascii += ".";
                    else
                        ascii += temp;

                    //temp.Append(Encoding.ASCII.GetString(new byte[] { (byte)Memory.Mem[i + j] }));
                }

                Mem.Append("\t" + ascii);
                ascii = "";
                Mem.Append(Environment.NewLine);
            }

            this.MemDump.Text = Mem.ToString();

            Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string MD = textBox1.Text;

            int i = Int32.Parse(MD, System.Globalization.NumberStyles.AllowHexSpecifier);
            int Max = i + 1000;
            if (Max > Memory.Mem.Length)
                Max = Memory.Mem.Length - 10;


            StringBuilder Mem = new StringBuilder();
            char temp;
            string ascii = "";
            for (; i < Max; i += 10)
            {
                Mem.Append(Convert.ToString(i, 16).PadLeft(5, '0') + "\t");

                for (int j = 0; j < 10; j++)
                {
                    Mem.Append(Convert.ToString(Memory.Mem[i + j], 16).PadLeft(3) + " ");

                    temp = Convert.ToChar(Memory.Mem[i + j]);
                    if (temp.ToString() == "" || temp.Equals('\0'))
                        ascii += ".";
                    else
                        ascii += temp;

                    //temp.Append(Encoding.ASCII.GetString(new byte[] { (byte)Memory.Mem[i + j] }));
                }

                Mem.Append("\t" + ascii);
                ascii = "";
                Mem.Append(Environment.NewLine);
            }

            this.MemDump.Text = Mem.ToString();
        }
    }
}
