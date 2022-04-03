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
using static Project3_HT.InstructionQueue;

namespace Project3_HT
{
    public partial class DynamicSim : Form
    {
        List<Instruction> Input_Instructions = new List<Instruction>();         // Creates a list of Instruction class types -JND
        public int cycleSpeed = 500;                                            //Defined so we can change the real time waiting period between cycles

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
                


                // check the size of queue 
                foreach (var item in Input_Instructions)
                {
                    InstructionQueue.AddToIQueue(item);
                    // remove the incstuction from the list
                    // ERROR: we can not change Input_Instructions of foreach 
                    Input_Instructions.Remove(item);

                }

                ChangeInstrQueue(IQueue.ToArray());

                //move to the SingleCycle 
               

                // Check if we have place for another instuction

                ChangeLoadBuffer(LoadBuffer.LdBuffer.ToArray());
                //ChangeInstrQueue(IQueue.ToArray());
                /*
                label8.Text = "Loaded";
                cycleCount = 0;
                dataHazardCount = 0;
                cycleLabel.Text = cycleCount.ToString();
                DHLabel.Text = dataHazardCount.ToString();
                */

            }//end if

            ChangeInstrQueue(IQueue.ToArray());

            //dequeue

            //ChangeLoadBuffer(LoadBuffer.LdBuffer.ToArray());
        }

        public void SingleCycle()
        {
            Instruction instr;
            Instruction[] text;

            /*  work backwards, like static pipeline - ideally most of this should be handled in each class
                
                Displaying everything will be tough since labels are non-static, aka can't change from outside the class
                see below method changeReorderBuf for my idea on how to handle it, not too bad imo
                
                1. Check and see if value can be popped from reorder buffer
                    check to see if instruction has come through CDB or store path
                    if yes, pop and push to the reg file or memory unit, if needed
                    if no, wait - do nothing
            */
            instr = ReorderBuffer.RemoveFromReorderBuf();
            text = ReorderBuffer.GetArray();
            ChangeReorderBuf(text);

            // pass instr to register file
            // TODO: Create reg file 

            if (instr != null)
            {
                ChangeRegisterFile(RegisterFile.UpdateRegister(instr));
            }


            /*

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
            */
            InstructionQueue.AddToIQueue(instr);

            // display
            // load

            //DecueueTheInstruction();
            /*

                Clock cycle - instead of setting up a loop like before, i think just running a single clock cycle method
                                over and over until last instruction goes through reorder buffer

                this way we could set up a run sim at clock count = 1 sec
                or allow user to click through clock cycles
                we might need to rearrange visually so that it looks nicer

            */
            // if there is an instuction on the list, try dequeue it
            //TODO: Check for the RS and RB
            if (IQueue.Any())
            {
                InstructionQueue.DecueueTheInstruction();
            }
            // check if our Instruction Queue is full
            AddInstructionsToIQueue();
            // display updated list of the instruction
            ChangeInstrQueue(IQueue.ToArray());

           
        }

        /// <summary>
        /// As long as there is no HALT instruction, keep adding 
        /// instruction to the list if needed
        /// </summary>
        public void AddInstructionsToIQueue()
        {
            while (Input_Instructions.Any() && IQueue.Count < 6 && haltNotFound.Equals(true))
            {
                foreach (var item in Input_Instructions)
                {
                    InstructionQueue.AddToIQueue(item);
                    // remove the incstuction from the list
                    Input_Instructions.Remove(item);

                }
            }
        }
        /// <summary>
        /// Display the message for invalid instruction
        /// When HALT instruction is detected, do not display anything after it
        /// </summary>
        /// <param name="array"></param>
        public void ChangeInstrQueue(Instruction[] array)
        {
            List<Label> Labels = new List<Label>()
            { InstructQueue1, InstructQueue2, InstructQueue3, InstructQueue4, InstructQueue5, InstructQueue6  };

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].OpCode == 404)
                {
                    //Labels[i].Text = array[i].Mnemonic;
                    MessageBox.Show("The pipeline encountered an invalid instruction. Check your code! The program will now restart.",
                                    "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    
                    Application.Restart();

                }
                if(array[i].OpCode == 0)
                {
                    Labels[i].Text = array[i].Mnemonic;
                    break;
                }
                Labels[i].Text = array[i].Mnemonic;
            }//end of for
        }
       
        public void ChangeLoadBuffer(Instruction[] array)
        {
            List<Label> Labels = new List<Label>()
            { LoadBuf1, LoadBuf2, LoadBuf3, LoadBuf4, LoadBuf5};

            for (int i = 0; i < array.Length; i++)
            {
               Labels[i].Text = array[i].Mnemonic;
            }
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
        public void ChangeRegisterFile(string[] array)
        {
            List<Label> Labels = new List<Label>()
            {  R0_Data,  R1_Data,   R2_Data,   R3_Data,   R4_Data,   R5_Data,   R6_Data,   R7_Data,
               R8_Data,  R9_Data,  R10_Data,  R11_Data,  R12_Data,  R13_Data,  R14_Data,  R15_Data,
              FP0_Data, FP1_Data,  FP2_Data,  FP3_Data,  FP4_Data,  FP5_Data,  FP6_Data,  FP7_Data,
              FP8_Data, FP9_Data, FP10_Data, FP11_Data, FP12_Data, FP13_Data, FP14_Data, FP15_Data };

            for (int i = 0; i < array.Length; i++)
            {
                Labels[i].Text = array[i];
            }
        }

        //assume starting with one reservation station for each and one functional unit
        //when add more, can add a label/index attribute to the rs classes and just populate the labels based on which station we're in (ie, FPaddMnem1 label or something)
        public void UpdateFPARS(String[] text)
        {
            List<Label> Labels = new List<Label>()
            { FPAddMnem1, FPAddDestReg1, FPAddOperand1, FPAddOpTwo1};

            for(int i = 0; i < text.Length; i++)
            {
                Labels[i].Text = text[i];
            }
        }
        private void cycleSpeedNUD_ValueChanged(object sender, EventArgs e)
        {
            this.cycleSpeed = (int)cycleSpeedNUD.Value;
        }

    }
}
