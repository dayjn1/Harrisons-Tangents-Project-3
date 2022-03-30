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
        public int cycleSpeed = 600;                                            //Defined so we can change the real time waiting period between cycles

        public DynamicSim()
        {
            InitializeComponent();
            cycleSpeedNUD.Value = cycleSpeed;
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
            /*  work backwards, like static pipeline - ideally most of this should be handled in each class
                
                Displaying everything will be tough since labels are non-static, aka can't change from outside the class
                see below method changeReorderBuf for my idea on how to handle it, not too bad imo
                
                1. Check and see if value can be popped from reorder buffer
                    check flag to see if instruction has come through CDB or store path
                    if yes, pop and push to the reg file or memory unit, if needed
                    if no, wait - do nothing

                2. Check if value on CDB
                    if yes, check res. stations one by one if they need the data before pushing to reorder buf
                    if no, do nothing

                3. Check if functional units are finished executing - fpadd, fpmult, int, load memory
                    if yes, push ONLY ONE, set up so that each unit dequeues or checks if dequeue is ready before going back to beg
                    if none are finished, then wait

                4. Check res stations and load buffer
                    if nothing in a given section is in the functional unit executing, queue it
                    if something, wait

                5. Check Instruction Queue
                    'decode' instruction enough to check needed res station/memory and reorder buffer
                    check reorder buffer first since every instrction will need it
                    if both are free, dequeue from IQ and enqueue to specified sections
                    if not free, wait

                Clock cycle - instead of setting up a loop like before, i think just running a single clock cycle method
                                over and over until last instruction goes through reorder buffer

                this way we could set up a run sim at clock count = 1 sec 
                or allow user to click through clock cycles
                we might need to rearrange visually so that it looks nicer

            */
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

        private void cycleSpeedNUD_ValueChanged(object sender, EventArgs e)
        {
            this.cycleSpeed = (int)cycleSpeedNUD.Value;
        }

    }
}
