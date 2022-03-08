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
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
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
        Stack<Instruction> Fetch = new Stack<Instruction>();                    // Creates the stacks for pipeline process -JND
        Stack<Instruction> Decode = new Stack<Instruction>();
        Stack<Instruction> Execute = new Stack<Instruction>();
        Stack<Instruction> Memory = new Stack<Instruction>();
        Stack<Instruction> Register = new Stack<Instruction>();
        int cycleCount = 0;                                                     // Counts the number of cycles
        List<String> usedRegisters = new List<string>(); //store stale registers  
        int hazardCount = 0;        //count hazards

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
                    }
                    else
                        Console.WriteLine("Invalid parse");

                }//end while
                label8.Text = "Loaded";
                cycleCount = 0;
                hazardCount = 0;
                cycleLabel.Text = cycleCount.ToString();
                label7.Text = hazardCount.ToString();
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
                Instruction wb;
                CountUpdate();
                UpdateAndDelay();

                if (this.Register.Count > 0)
                {

                   wb = this.Register.Pop();
                    usedRegisters.Remove(wb.DestReg);
                    
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



            }

            // clean up pipeline
            
            while (this.Fetch.Count != 0 || this.Decode.Count != 0 || this.Execute.Count != 0 || this.Memory.Count != 0 || this.Register.Count != 0)
            {
                CountUpdate();
                UpdateAndDelay();
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
                


            }
        }
        // See if an operand register in an instruction is being used already, and stall until it is free.
        //Avery 
        public void CompareOpRegisters(Instruction i)
        {
            //check if its in the stale registers
            if (usedRegisters.Contains(i.Reg1) && i.Reg1 != null || usedRegisters.Contains(i.Reg2) && i.Reg2 != null)
            {
                CountUpdate(); //go stall
            }
            else
            {
                return; //if not, leave
            }
            CompareOpRegisters(i); //try again otherwise
        }

        public void ProcessDecode()
        {
            Instruction i = this.Fetch.Pop();
            this.FetchBox.Text = "";
            CompareOpRegisters(i);
            if(i.writeBack == true)
            {
                CheckRegisters(i);
            }
            if (i.DecodeCC != 0)
            {
                PushDecode(i);

                CountUpdate();
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



               // CountUpdate();
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

                CountUpdate();
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

                CountUpdate();
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
                CountUpdate();
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
            if (!usedRegisters.Contains(i.DestReg))
            {
                usedRegisters.Add(i.DestReg);
            }
            else//if the register is in use already, wait until it is not.
            {
                hazardCount++;
                label7.Text = hazardCount.ToString();
                Task.Delay(1000).Wait();
                usedRegisters.Remove(i.DestReg);
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
            Task.Delay(1000).Wait();
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

    }
}
