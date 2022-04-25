// -------------------------------------------------------------------------------
// File name:                   DynamicSim.cs
// Project name:                Project 3 - Harrison's Tangents
// -------------------------------------------------------------------------------
// Edited By:                   Nataliya Chibizova, Janine Day, Jason Middlebrook,
//                              Avery Marlow, Hannah Taylor
// Course-Section:              CSCI-4717
// Creation Date:               03/27/2022
// -------------------------------------------------------------------------------

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
using static Project3_HT.LoadBuffer;
using static Project3_HT.ReorderBuffer;


namespace Project3_HT
{
    public partial class DynamicSim : Form
    {
        public static List<Instruction> Input_Instructions = new List<Instruction>();
        public static int cycleSpeed = 500, CycleCount = 0, ListCounter = 0;                                            
        public static string ProgramType = "Continuous";
        bool FirstInstruction = true, invalid = false;        

        public DynamicSim()
        {
            InitializeComponent();
            CacheFourWay cacheForm = new CacheFourWay();
            cacheForm.Show();
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
                    bool valid = Int32.TryParse(inputData, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out int input);
                    if (valid)
                    {
                        Input_Instructions.Add(new Instruction(input));         // Creates instructions and adds them to list -JND
                        Memory.MemPopulate(input);
                    }
                    else
                        Console.WriteLine("Invalid parse");

                }//end while

                // add to the instuction queue in the beginning of the program, before the first cycle & display it
                AddInstructionsToIQueue();
                RSManager.PopulateLists();

            }//end if
        }

        public static void Reset()
        {
            Application.Restart();
        }//end Reset()

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (ProgramType == "Continuous")
                ContinuousSim();
            else
            {
                SingleCycle();
                if (IsFinished())
                {
                    NextButton.Enabled = false;
                }//end if
            }//end else
        }//end NextButton_Click(object, EventArgs)


        public void ContinuousSim()
        {
            bool areWeDone = false;
            while (areWeDone == false)
            {
                // delay by cyclespeed
                Task.Delay(cycleSpeed).Wait();  //hannah
                SingleCycle();
                areWeDone = IsFinished();
            }//end while
        }//end ContinuousSim()

        /// <summary>
        /// If the entire system is empty, end the simulation
        /// </summary>
        /// <returns></returns>
        public bool IsFinished()
        {
            bool fin = false; //hannah

            if (invalid || ((InstructionQueue.IQueue.Count == 0 || InstructionQueue.haltNotFound == false) &&
                AddressUnit.AddressUnitQueue.Count == 0 &&
                RSManager.CheckAllRSEmpty() &&
                LoadBuffer.LdBuffer.Count == 0 &&
                FuncUnitManager.checkAllEmpty() &&
                CDBus.currentInstruction == null &&
                ReorderBuffer.ReorderBuf.Count == 0))
            {
                fin = true;
                NextButton.Enabled = false;
            }//end if
            else
                fin = false;

            return fin;
        }//end IsFinished()

        public void SingleCycle()
        {
            Instruction instr;
            Instruction[] text;
            CycleCount++;
            this.CycleCountLabel.Text = CycleCount.ToString();

            bool FARS1Ready, FARS2Ready, FARS3Ready, FMRS1Ready, FMRS2Ready, FMRS3Ready, IRS1Ready, IRS2Ready, IRS3Ready;

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
                RegisterFile.UpdateRegister(instr);
                ChangeRegisterFile();

                //ChangeRegisterFile(RegisterFile.UpdateRegister(instr));
            }//end if


            ChangeLoadBuffer(LdBuffer.ToArray());   // display updated queue of instructions
            if (LdBuffer.Any())
            {
                LoadBuffer.SendToMemUnit();                        // dequeue from the LdBuffer
            }//end if

            if (AddressUnit.AddressUnitQueue.Any())
            {
                AddressUnit.ProcessAU();                // send to LB or to pass to RO

            }//end if

            /*

                2. Check if value on CDB
                    if yes, check res. stations one by one if they need the data before pushing to reorder buf
                    if no, do nothing
            */

            if (CDBus.currentInstruction != null)
            {
                FARS1Ready = RSManager.CheckCDB(RSManager.FPAddRS[0]);
                FARS2Ready = RSManager.CheckCDB(RSManager.FPAddRS[1]);
                FARS3Ready = RSManager.CheckCDB(RSManager.FPAddRS[2]);

                FMRS1Ready = RSManager.CheckCDB(RSManager.FPMultRS[0]);
                FMRS2Ready = RSManager.CheckCDB(RSManager.FPMultRS[1]);
                FMRS3Ready = RSManager.CheckCDB(RSManager.FPMultRS[2]);

                IRS1Ready = RSManager.CheckCDB(RSManager.IntegerRS[0]);
                IRS2Ready = RSManager.CheckCDB(RSManager.IntegerRS[1]);
                IRS3Ready = RSManager.CheckCDB(RSManager.IntegerRS[2]);

                

                //TODO: push instruction on CDB to ROB CheckRegFile
                CDBus.SendResults();

            }//end if

            RSManager.CheckRegFile(RSManager.FPAddRS[0]);
            RSManager.CheckRegFile(RSManager.FPAddRS[1]);
            RSManager.CheckRegFile(RSManager.FPAddRS[2]);

            RSManager.CheckRegFile(RSManager.FPMultRS[0]);
            RSManager.CheckRegFile(RSManager.FPMultRS[1]);
            RSManager.CheckRegFile(RSManager.FPMultRS[2]);

            RSManager.CheckRegFile(RSManager.IntegerRS[0]);
            RSManager.CheckRegFile(RSManager.IntegerRS[1]);
            RSManager.CheckRegFile(RSManager.IntegerRS[2]);



            /*
                3. Check if functional units are finished executing - fpadd, fpmult, int, load memory
                    if yes, push ONLY ONE, set up so that each unit dequeues or checks if dequeue is ready before going back to beg
                    if none are finished, then wait
            */
            CDBus.ReceiveResults(); //checks in a



            /*
                4. Check res stations and load buffer
                    if nothing in a given section is in the functional unit executing, queue it
                    if something, wait
            */
            FuncUnitManager.CheckStationsToPushToFuncUnits();
            FuncUnitManager.ExeCycle();


            /*
                5. Check Instruction Queue
                    'decode' instruction enough to check needed res station/memory and reorder buffer
                    
                    check reorder buffer first since every instrction will need it
                    if both are free, dequeue from IQ and enqueue to specified sections
                    if not free, wait
            */


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
            if (IQueue.Any() && FirstInstruction == false)
            {
                DecueueTheInstruction();                // dequeue the instruction
                //ChangeLoadBuffer(LdBuffer.ToArray());   // display updated queue of instructions in LB
                ChangeInstrQueue(IQueue.ToArray());
                // TODO: change the reservation station and RB
                
            }//end if

            AddInstructionsToIQueue();                  // add new instructions to the queue          
            ChangeInstrQueue(IQueue.ToArray());         // display updated queue of instructions 

            if (FirstInstruction)
                FirstInstruction = false;

            UpdateFPAddRS();
            UpdateFPMultRS();
            UpdateIntegerRS();

            Update();
                      
        }//end SingleCycle()

        /// <summary>
        /// As long as there is no HALT instruction, keep adding 
        /// instruction to the Instruction queue if needed
        /// </summary> -- NC
        public void AddInstructionsToIQueue()
        {
            while ((ListCounter < Input_Instructions.Count) && IQueue.Count < 6 && haltNotFound.Equals(true))
            {
                int size = Input_Instructions.Count();
                
                for (int j = ListCounter; j < size; j++)              // check the size of queue 
                {
                    if (IQueue.Count() < 6 && Input_Instructions.Any())
                    {
                        AddToIQueue(Input_Instructions[j]);
                        ListCounter++;
                    }//end if
                }//end for
                
                ChangeInstrQueue(IQueue.ToArray());         //display that are currently on the the queue

            }//end while
        }//end AddInstructionsToIQueue()


        /// <summary>
        /// Display the message for invalid instruction
        /// When HALT instruction is detected, do not display anything after it
        /// </summary>
        /// <param name="array"></param>
        public void ChangeInstrQueue(Instruction[] array)
        {
            List<Label> Labels = new List<Label>()
            { InstructQueue1, InstructQueue2, InstructQueue3, InstructQueue4, InstructQueue5, InstructQueue6  };
            
            foreach (var lable in Labels)               //update the value of the lable
            {
                lable.Text = " ";
            }//end foreach

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].OpCode == 404)
                {
                    MessageBox.Show("The pipeline encountered an invalid instruction. Check your code! The program will now restart.", "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    invalid = true;
                    Close();
                    Reset();
                    break;
                }//end if
                if(array[i].OpCode == 0)                // do not add any values after halt
                {
                    Labels[i].Text = array[i].Mnemonic;
                    break;
                }//end if
                Labels[i].Text = array[i].Mnemonic;
            }//end of for
        }//end ChangeInstrQueue(Instruction[])

        /// <summary>
        /// Update the text value of the LB
        /// </summary>
        /// <param name="array"></param>
        public void ChangeLoadBuffer(Instruction[] array)
        {
            List<Label> Labels = new List<Label>()
            { LoadBuf1, LoadBuf2, LoadBuf3, LoadBuf4, LoadBuf5}; 
            foreach (var lable in Labels)               //update the value of the lable
            {
                lable.Text = " ";
            }//end foreach
            for (int i = 0; i < array.Length; i++)
            {
               Labels[i].Text = array[i].Mnemonic;
            }//end for
        }//end ChangeLoadBuffer(Instruction[])

        public void ChangeReorderBuf(Instruction[] array)
        {
            List<Label> Labels = new List<Label>()
            { ReorderBuf1, ReorderBuf2, ReorderBuf3, ReorderBuf4, ReorderBuf5  };

            foreach (var label in Labels)               //update the value of the label
            {
                label.Text = " ";
            }//end foreach

            for (int i = 0; i < array.Length; i++)
            {
                Labels[i].Text = array[i].Mnemonic;
            }//end for
        }//end ChangeReorderBuf(Instruction[])
        
        public void ChangeRegisterFile()
        {
            List<int> RegData = RegisterFile.ReturnReg();
            List<float> FRegData = RegisterFile.ReturnFReg();


            List<Label> Labels = new List<Label>()
            {  R0_Data,  R1_Data,   R2_Data,   R3_Data,   R4_Data,   R5_Data,   R6_Data,   R7_Data,
               R8_Data,  R9_Data,  R10_Data,  R11_Data,  R12_Data,  R13_Data,  R14_Data,  R15_Data,
              FP0_Data, FP1_Data,  FP2_Data,  FP3_Data,  FP4_Data,  FP5_Data,  FP6_Data,  FP7_Data,
              FP8_Data, FP9_Data, FP10_Data, FP11_Data, FP12_Data, FP13_Data, FP14_Data, FP15_Data };

            foreach (var label in Labels)               //update the value of the label
            {
                label.Text = " ";
            }//end foreach

            for (int i = 0; i < RegData.Count; i++)
            {
                Labels[i].Text = RegData[i].ToString();
            }//end for

            for (int i = 0; i < FRegData.Count; i++)
            {
                Labels[i + 16].Text = FRegData[i].ToString();
            }//end for
        }//end ChangeRegisterFile(string[])

        //assume starting with one reservation station for each and one functional unit
        //when add more, can add a label/index attribute to the rs classes and just populate the labels based on which station we're in (ie, FPaddMnem1 label or something)
        public void UpdateFPAddRS()
        {
            List<Label> Labels = new List<Label>()
            {
                FPAddMnem1, FPAddDestReg1, FPAddOp1_1, FPAddOp2_1,
                FPAddMnem2, FPAddDestReg2, FPAddOp1_2, FPAddOp2_2,
                FPAddMnem3, FPAddDestReg3, FPAddOp1_3, FPAddOp2_3
            };

            foreach (var label in Labels)               //update the value of the label
            {
                label.Text = " ";
            }//end foreach

            if (RSManager.FPAddRS[0] != null)
            {
                Labels[0].Text = RSManager.FPAddRS[0].mnemonic;
                Labels[1].Text = RSManager.FPAddRS[0].destR;
                Labels[2].Text = RSManager.FPAddRS[0].operand1;
                Labels[3].Text = RSManager.FPAddRS[0].operand2;
            }//end if
            if (RSManager.FPAddRS[1] != null)
            {
                Labels[4].Text = RSManager.FPAddRS[1].mnemonic;
                Labels[5].Text = RSManager.FPAddRS[1].destR;
                Labels[6].Text = RSManager.FPAddRS[1].operand1;
                Labels[7].Text = RSManager.FPAddRS[1].operand2;
            }//end if
            if (RSManager.FPAddRS[2] != null)
            {
                Labels[8].Text = RSManager.FPAddRS[2].mnemonic;
                Labels[9].Text = RSManager.FPAddRS[2].destR;
                Labels[10].Text = RSManager.FPAddRS[2].operand1;
                Labels[11].Text = RSManager.FPAddRS[2].operand2;
            }//end if
        }//end UpdateFPAddRS()

        public void UpdateFPMultRS()
        {
            List<Label> Labels = new List<Label>()
            {
                FPMultMnem1, FPMultDestReg1, FPMultOp1_1, FPMultOp2_1,
                FPMultMnem2, FPMultDestReg2, FPMultOp1_2, FPMultOp2_2,
                FPMultMnem3, FPMultDestReg3, FPMultOp1_3, FPMultOp2_3
            };

            foreach (var label in Labels)               //update the value of the label
            {
                label.Text = " ";
            }//end foreach

            if (RSManager.FPMultRS[0] != null)
            {
                Labels[0].Text = RSManager.FPMultRS[0].mnemonic;
                Labels[1].Text = RSManager.FPMultRS[0].destR;
                Labels[2].Text = RSManager.FPMultRS[0].operand1;
                Labels[3].Text = RSManager.FPMultRS[0].operand2;
            }//end if
            if (RSManager.FPMultRS[1] != null)
            {
                Labels[4].Text = RSManager.FPMultRS[1].mnemonic;
                Labels[5].Text = RSManager.FPMultRS[1].destR;
                Labels[6].Text = RSManager.FPMultRS[1].operand1;
                Labels[7].Text = RSManager.FPMultRS[1].operand2;
            }//end if
            if (RSManager.FPMultRS[2] != null)
            {
                Labels[8].Text = RSManager.FPMultRS[2].mnemonic;
                Labels[9].Text = RSManager.FPMultRS[2].destR;
                Labels[10].Text = RSManager.FPMultRS[2].operand1;
                Labels[11].Text = RSManager.FPMultRS[2].operand2;
            }//end if
        }//end UpdateFPMultRS()

        public void UpdateIntegerRS()
        {
            List<Label> Labels = new List<Label>()
            {
                IntegerMnem1, IntegerDestReg1, IntegerOp1_1, IntegerOp2_1,
                IntegerMnem2, IntegerDestReg2, IntegerOp1_2, IntegerOp2_2,
                IntegerMnem3, IntegerDestReg3, IntegerOp1_3, IntegerOp2_3
            };

            foreach (var label in Labels)               //update the value of the label
            {
                label.Text = " ";
            }//end foreach

            if (RSManager.IntegerRS[0] != null)
            {
                Labels[0].Text = RSManager.IntegerRS[0].mnemonic;
                Labels[1].Text = RSManager.IntegerRS[0].destR;
                Labels[2].Text = RSManager.IntegerRS[0].operand1;
                Labels[3].Text = RSManager.IntegerRS[0].operand2;
            }//end if
            if (RSManager.IntegerRS[1] != null)
            {
                Labels[4].Text = RSManager.IntegerRS[1].mnemonic;
                Labels[5].Text = RSManager.IntegerRS[1].destR;
                Labels[6].Text = RSManager.IntegerRS[1].operand1;
                Labels[7].Text = RSManager.IntegerRS[1].operand2;
            }//end if
            if (RSManager.IntegerRS[2] != null)
            {
                Labels[8].Text = RSManager.IntegerRS[2].mnemonic;
                Labels[9].Text = RSManager.IntegerRS[2].destR;
                Labels[10].Text = RSManager.IntegerRS[2].operand1;
                Labels[11].Text = RSManager.IntegerRS[2].operand2;
            }//end if
        }//end UpdateIntegerRS()


        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }//end resetToolStripMenuItem_Click(object, EventArgs)
    }//end DynamicSim
}//end Project3_HT
