// File name:                   Form1.cs
// Project name:                Project 2 - Harrison's Tangents
// ---------------------------------------------------------------------------
// Creator’s name:              Janine Day
// Edited By:                   Janine Day, 
// Course-Section:              CSCI-4717
// Creation Date:               02/17/2022
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Project2_HT
{
    /**
    * Class Name:       Tangents
    * Class Purpose:    GUI code for fetching input, processing input, running simulation, and (hopefully) save info into textfile
    *
    * <hr>
    * Date created: 02/17/2022
    * @Janine Day
    */
    public partial class Tangents : Form
    {
        List<Instruction> Input_Instructions = new List<Instruction>();         // Creates a list of Instruction class types -JND
        List<Instruction> Save_Stats = new List<Instruction>();
        Stack<Instruction> Fetch = new Stack<Instruction>();                    // Creates the stacks for pipeline process -JND
        Stack<Instruction> Decode = new Stack<Instruction>();
        Stack<Instruction> Execute = new Stack<Instruction>();
        Stack<Instruction> Memory = new Stack<Instruction>();
        Stack<Instruction> Register = new Stack<Instruction>();
        int cycleCount = 0;                                                     // Counts the number of cycles
        List<String> usedRegisters = new List<string>();
        int hazardCount = 0;

        /**
        * Method Name: Tangents()
        * Method Purpose: Automatically Generated code to initialize GUI
        *
        * <hr>
        * Date created: 02/17/2022
        *
        * <hr>
        */
        public Tangents()
        {
            InitializeComponent();
        }

        /**
        * Method Name: openToolStripMenuItem_Click(object, EventArgs)
        * Method Purpose: To use an open file dialog which allows users which text file they would like to process
        *
        * <hr>
        * Date created: 02/17/2022
        * @Janine Day
        * <hr>
        * @param object - sender
        * @param EventArgs - e
        */
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
                        Save_Stats.Add(new Instruction(input));
                    }
                    else
                        Console.WriteLine("Invalid parse");

                }//end while
                label8.Text = "Loaded";
            }//end if

        }

        /**
        * Method Name: StartButton_Click(object, EventArgs)
        * Method Purpose: Starts simulation once event occurs
        *
        * <hr>
        * Date created: 02/17/2022
        * @Janine Day
        * <hr>
        * @param object - sender
        * @param EventArgs - e
        */
        private void StartButton_Click(object sender, EventArgs e)
        {
            label8.Text = "Processing...";
            Simulation();
            label8.Text = "Finished";
        }

        /**
        * Method Name: Simulation()
        * Method Purpose: Algorithm to run pipeline process. Uses global stacks and user input
        *
        * <hr>
        * Date created: 02/17/2022
        * @Janine Day
        * <hr>
        */
        public void Simulation()
        {
            for (int i = 0; i < this.Input_Instructions.Count; i++)
            {

                if (this.Register.Count > 0)
                {
                    this.Register.Pop();
                    this.RegisterBox.Text = "";
                }

                if (this.Memory.Count > 0)
                {
                    ProcessRegister();
                }

                if (this.Execute.Count > 0)
                {
                    ProcessMemory();
                }

                if (this.Decode.Count > 0)
                {
                    ProcessExecute();
                }

                if (this.Fetch.Count > 0)
                {
                    ProcessDecode();
                }

                PushFetch(this.Input_Instructions[i]);

                CountUpdate();
                UpdateAndDelay();

            }

            // clean up pipeline

            while (this.Fetch.Count != 0 || this.Decode.Count != 0 || this.Execute.Count != 0 || this.Memory.Count != 0 || this.Register.Count != 0)
            {
                if (this.Register.Count > 0)
                {
                    this.Register.Pop();
                    this.RegisterBox.Text = "";
                }

                if (this.Memory.Count > 0)
                {
                    ProcessRegister();
                }

                if (this.Execute.Count > 0)
                {
                    ProcessMemory();
                }

                if (this.Decode.Count > 0)
                {
                    ProcessExecute();
                }

                if (this.Fetch.Count > 0)
                {
                    ProcessDecode();
                }

                cycleLabel.Text = cycleCount.ToString();
                UpdateAndDelay();

            }
        }


        public void ProcessDecode()
        {
            Instruction i = this.Fetch.Pop();
            this.FetchBox.Text = "";
            CheckRegisters(i);
            if (i.DecodeCC != 0)
            {
                PushDecode(i);

                //CountUpdate();
                UpdateAndDelay();

                while (i.DecodeCC > 0)
                {
                    i.DecodeCC--;

                    CountUpdate();
                    UpdateAndDelay();
                }
            }
        }

        public void ProcessExecute()
        {
            Instruction i = this.Decode.Pop();
            this.DecodeBox.Text = "";
            if (i.ExecuteCC != 0)
            {
                PushExecute(i);

                //CountUpdate();
                UpdateAndDelay();


                while (i.ExecuteCC > 0)
                {
                    i.ExecuteCC--;

                    CountUpdate();
                    UpdateAndDelay();
                }
            }
        }

        public void ProcessMemory()
        {
            Instruction i = this.Execute.Pop();
            this.ExecuteBox.Text = "";

            if (i.MemoryCC != 0)
            {
                PushMemory(i);

                //CountUpdate();
                UpdateAndDelay();

                while (i.MemoryCC > 0)
                {
                    i.MemoryCC--;

                    CountUpdate();
                    UpdateAndDelay();
                }
            }
            else if(i.RegisterCC != 0)
            {
                PushRegister(i);

                //CountUpdate();
                UpdateAndDelay();


                while (i.RegisterCC > 0)
                {
                    i.RegisterCC--;

                    CountUpdate();
                    UpdateAndDelay();
                }
            }
        }

        public void ProcessRegister()
        {
            Instruction i = this.Memory.Pop();
            this.MemoryBox.Text = "";
            
            if (i.RegisterCC != 0)
            {
                PushRegister(i);

                UpdateAndDelay();


                while (i.RegisterCC > 0)
                {
                    i.RegisterCC--;

                    CountUpdate();
                    UpdateAndDelay();
                }
                usedRegisters.Clear();  //clear registers when no longer in use
            }
          
        }
        /// <summary>Accepts an instruction and checks if its registers are available.
        /// On fail it waits until the registers are available. Used to get the registers ready to push.</summary>
        /// AM
        /// <param name="i">Instruction passed in from Process Registers</param>
        public void CheckRegisters(Instruction i)
        {
            //see if register is available by checking the usedRegisters list
            if (!(usedRegisters.Contains(i.DestReg)))
            {
                usedRegisters.Add(i.DestReg);
            }
            else//if the register is in use already, wait until it is not.
            {
                hazardCount++;
                label7.Text = hazardCount.ToString();
                Task.Delay(1000).Wait();
                usedRegisters.Clear();  //clear registers when no longer in use
                CheckRegisters(i);
            }

        }
        public void CountUpdate()
        {
            this.cycleCount++;
            cycleLabel.Text = cycleCount.ToString();
        }

        public void UpdateAndDelay()
        {
            Update();
            Task.Delay(500).Wait();
        }

        public void PushFetch(Instruction i)
        {
            this.Fetch.Push(i);
            i.FetchCC--;
            FetchText(i);
        }

        public void PushDecode(Instruction i)
        {
            this.Decode.Push(i);
            i.DecodeCC--;
            DecodeText(i);
        }

        public void PushExecute(Instruction i)
        {
            this.Execute.Push(i);
            i.ExecuteCC--;
            ExecuteText(i);
        }

        public void PushMemory(Instruction i)
        {
            this.Memory.Push(i);
            i.MemoryCC--;
            MemoryText(i);
        }

        public void PushRegister(Instruction i)
        {
            this.Register.Push(i);
            i.RegisterCC--;
            RegisterText(i);
        }


        /**
        * Method Name: RegisterText(Instruction)
        * Method Purpose: Updates RegisterText content
        *
        * <hr>
        * Date created: 02/17/2022
        * @Janine Day
        * <hr>
        * @param Instruction - provides info for visuals
        */
        public void RegisterText(Instruction i)
        {
            RegisterBox.Text = i.Mnemonic;
        }

        /**
        * Method Name: MemoryText(Instruction)
        * Method Purpose: Updates MemoryText content
        *
        * <hr>
        * Date created: 02/17/2022
        * @Janine Day
        * <hr>
        * @param Instruction - provides info for visuals
        */
        public void MemoryText(Instruction i)
        {
            MemoryBox.Text = i.Mnemonic;
        }

        /**
        * Method Name: ExecuteText(Instruction)
        * Method Purpose: Updates ExecuteText content
        *
        * <hr>
        * Date created: 02/17/2022
        * @Janine Day
        * <hr>
        * @param Instruction - provides info for visuals
        */
        public void ExecuteText(Instruction i)
        {
            ExecuteBox.Text = i.Mnemonic;
        }

        /**
        * Method Name: DecodeText(Instruction)
        * Method Purpose: Updates DecodeText content
        *
        * <hr>
        * Date created: 02/17/2022
        * @Janine Day
        * <hr>
        * @param Instruction - provides info for visuals
        */
        public void DecodeText(Instruction i)
        {
            DecodeBox.Text = i.Mnemonic;
        }

        /**
        * Method Name: FetchText(Instruction)
        * Method Purpose: Updates FetchText content
        *
        * <hr>
        * Date created: 02/17/2022
        * @Janine Day
        * <hr>
        * @param Instruction - provides info for visuals
        */
        public void FetchText(Instruction i)
        {
            FetchBox.Text = i.Mnemonic;
        }

        public static string dirParameter = AppDomain.CurrentDomain.BaseDirectory + @"\saveOut.txt";
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OpenFileDialog SaveDlg = new OpenFileDialog();
            //string fileN = SaveDlg.FileName;
            FileStream fParameter = new FileStream(dirParameter, FileMode.Create, FileAccess.Write);
            StreamWriter filewrite = new StreamWriter(fParameter);

            filewrite.WriteLine("   Instruction    |    Fetch    |    Decode    |    Execute    |    Memory    |    WriteBack ");
            filewrite.WriteLine("______________________________________________________________________");

            int f, d, exe, m, w;
            int fetch = 0, 
                decodeS = 0, 
                decodeE = 0, 
                executeS = 0, 
                executeE = 0, 
                memS = 0, 
                memE = 0, 
                writeB = 0;
            //int lastHit = 0;
            bool memUsedBefore = false;
            bool decodeStall = false;

            string decode, exec, memo = "", stringWB;
            //int decodeStallE = -1, executeStallE = -1, memStallE = -1;

            for (int i = 0; i < Save_Stats.Count; i++)
            {
                f = Save_Stats[i].FetchCC;
                d = Save_Stats[i].DecodeCC;
                exe = Save_Stats[i].ExecuteCC;
                m = Save_Stats[i].MemoryCC;
                w = Save_Stats[i].RegisterCC;

                //in every case, fetch is calculated the same
                fetch = f + i;

                //decode
                if(i == 0)
                {
                    decodeS = fetch + 1;
                    decodeE = fetch + d;

                    if (d > 1)
                    {
                        decode = decodeS + " - " + decodeE;
                        decodeStall = true;
                    }
                    else
                        decode = decodeS.ToString();
                }
                else
                {
                    decodeS = decodeE + 1;  //decodeE should still contain the value from the prev iteration
                    decodeE += 1; //assume one cycle to decode

                    if (d > 1)
                    {
                        decodeE = (d - 1) + decodeS; //update if actually takes more than one cycle
                        decode = decodeS + " - " + decodeE;
                        decodeStall = true;
                    }
                    else
                    {
                        decode = decodeS.ToString();
                    }
                }

                //execute
                if (i == 0 || decodeStall == true)
                {
                    executeS = decodeE + 1;
                    executeE = decodeE + 1; //assume one cycle

                    if (exe > 1)
                    {
                        executeE = (exe - 1) + executeS; //update if more than one
                        exec = executeS + " - " + executeE;
                    }
                    else
                        exec = executeS.ToString();

                    decodeStall = false; //reset for next decode calculation
                }
                else
                {
                    executeS = executeE + 1; //can safely calc from prev execute end cycle
                    executeE += 1; //assume one cycle to execute

                    if (exe > 1)
                    {
                        executeE = (exe - 1) + executeS; //update if more than one
                        exec = executeS + " - " + executeE;
                    }
                    else
                        exec = executeS.ToString();
                }
                

                //memory
                if (m != 0) //if we need to access memory
                {
                    if (i == 0)
                    {
                        memS = executeE + 1;
                        memE = (m - 1) + memS;
                        memo = memS + " - " + memE;
                        memUsedBefore = true;
                    }
                    else
                    {
                        if (memUsedBefore)
                        {
                            if(memE < executeE)
                            {
                                memS = executeE + 1;
                                memE = (m - 1) + memS;
                                memo = memS + " - " + memE;
                                memUsedBefore = true;
                            }
                            else
                            {
                                memS = memE + 1;
                                memE = (m - 1) + memS;
                                memo = memS + " - " + memE;
                                memUsedBefore = true;
                            }
                        }
                        else
                        {
                            memS = executeE + 1;
                            memE = (m - 1) + memS;
                            memo = memS + " - " + memE;
                            memUsedBefore = true;
                        }
                    }
                }
                else
                    memo = " ";

                //^end memory section

                //register writeback -- assuming never takes longer than one cycle
                if (w != 0)
                {
                    if (m != 0)
                    {
                        writeB = memE + 1;
                        stringWB = writeB.ToString();
                    }
                    else
                    {
                        writeB = executeE + 1; //increment counter from execute stage if memory was not touched
                        stringWB = writeB.ToString();
                    }
                }
                else
                {
                    stringWB = " ";
                }


                filewrite.WriteLine("     " + Save_Stats[i].Mnemonic.PadRight(5, ' ') + "              " + 
                                   (fetch) + "                 " +
                                   (decode.PadRight(9, ' ')) + "              " +
                                   (exec) + "               " +
                                   (memo.PadRight(9, ' ')) + "               " +
                                   (stringWB) + "          ");


                
                


            }//end for loop for printing Instruction log


            filewrite.WriteLine();
            filewrite.WriteLine();
            filewrite.WriteLine("________________________________________________________________");
            filewrite.WriteLine("Cycle count: " + cycleCount);
            filewrite.WriteLine("Hazard count: " + hazardCount);


            filewrite.Flush();
            filewrite.Close();

        }
    }
}
