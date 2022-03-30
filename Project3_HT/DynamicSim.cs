using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3_HT
{
    public partial class DynamicSim : Form
    {
        List<Instruction> Input_Instructions = new List<Instruction>();         // Creates a list of Instruction class types -JND

        public DynamicSim()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDlg = new OpenFileDialog();                      // Declares and initializes OpenFileDialog
            if (DialogResult.Cancel != OpenDlg.ShowDialog())                    // If file was successfully opened
            {
                string fileName = OpenDlg.FileName;                             // Get file name
                StreamReader f = new StreamReader(fileName);                    // Declares and initializes Streamreader using file name

                while (f.Peek() != -1)                                          // While there is more to read for the file
                {
                    string inputData = f.ReadLine();                            //Declares inputData so lines can be read from input

                    //try to parse one line of input, converting hexadecimal to int and sending to disassembler -H, JM
                    int input;
                    bool valid = Int32.TryParse(inputData, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out input);
                    if (valid)
                    {
                        Input_Instructions.Add(new Instruction(input));         // Creates instructions and adds them to list -JND
                        //Save_Stats.Add(new Instruction(input));
                    }
                    else
                        Console.WriteLine("Invalid parse");

                }//end while
                
                /*
                label8.Text = "Loaded";
                cycleCount = 0;
                dataHazardCount = 0;
                cycleLabel.Text = cycleCount.ToString();
                DHLabel.Text = dataHazardCount.ToString();
                */

            }//end if
        }

        public void Simulation()
        {

        }

        public void ChangeReorderBuf(Instruction[] array)
        {
            List<Label> Labels = new List<Label>()
            { ReorderBuf1, ReorderBuf2, ReorderBuf3, ReorderBuf4, ReorderBuf5  };

            for(int i = 0; i < array.Length; i++)
            {
                Labels[i].Text = array[i].Mnemonic;
            }
        }
    }
}
