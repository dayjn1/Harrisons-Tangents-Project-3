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
        Stack<Instruction> Fetch = new Stack<Instruction>();                    // Creates the stacks for pipeline process -JND
        Stack<Instruction> Decode = new Stack<Instruction>();
        Stack<Instruction> Execute = new Stack<Instruction>();
        Stack<Instruction> Memory = new Stack<Instruction>();
        Stack<Instruction> Register = new Stack<Instruction>();
        int cycleCount = 0;                                                     // Counts the number of cycles

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
            this.Fetch.Clear();                                             //Clear previous Simulation -JM
            cycleCount = 0;
            Simulation();
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
            bool halt = false;
            Instruction temp;

            for (int i = 0; i < this.Input_Instructions.Count + 4; i++)     //Keep going after last fetch -JM
            {
                //Clear old text -JM
                RegisterBox.Text = "";
                MemoryBox.Text = "";
                ExecuteBox.Text = "";
                DecodeBox.Text = "";

                if (this.Register.Count > 0 && this.Memory.Count < 1)
                {
                    this.Register.Pop();
                }


                if (this.Memory.Count > 0)
                {
                    temp = this.Memory.Pop();
                    PushRegister(temp);
                }

                if (this.Execute.Count > 0)
                {
                    temp = this.Execute.Pop();

                    bool skipMem = !PushMemory(temp);                   //If our instr accesses memory, we cannot write to registers yet; do not skip memory -JM
                    if (skipMem)
                    {
                        PushRegister(temp);                             //If no access to mem, skip mem stack straight to register -JM (still need data hazard detection)
                    }

                    if (i > this.Input_Instructions.Count)              //if last instruction, end -JM (this is not a good solution)
                        i = this.Input_Instructions.Count + 4;
                }

                if (this.Decode.Count > 0)
                {
                    temp = this.Decode.Pop();
                    this.Execute.Push(temp);
                    ExecuteText(temp);
                }

                if (this.Fetch.Count > 0)
                {
                    temp = this.Fetch.Pop();
                    if(temp.Mnemonic == "HALT")
                    {
                        halt = true;
                        this.Fetch.Push(temp);
                    }
                    else
                    {
                        this.Decode.Push(temp);
                        DecodeText(temp);
                    }
                }

                if(i < this.Input_Instructions.Count && !halt)               //if there are more instructions coming, fetch -JM
                {
                    this.Fetch.Push(this.Input_Instructions[i]);
                    FetchText(this.Input_Instructions[i]);
                }

                //debugging
                foreach(Instruction inst in this.Register)
                {
                    Console.WriteLine(inst.Mnemonic);
                }


                //Set clock cycle count -JM
                System.Threading.Thread.Sleep(1000);
                cycleCount++;
                cycleLabel.Text = cycleCount.ToString();
                Update();
            }
        }//end Simulation

        public bool PushRegister(Instruction i)
        {
            this.Register.Push(i);
            RegisterText(i);
            return true;

            //if (Instruction.RegInstructions.Contains(i.Mnemonic))
            //{
            //    this.Register.Push(i);
            //    RegisterText(i);

            //    return true;
            //}
            //else return false;
        }


        public bool PushMemory(Instruction i)
        {
            if (Instruction.MemInstructions.Contains(i.Mnemonic))
            {
                this.Memory.Push(i);
                MemoryText(i);

                return true;
            }
            else return false;
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
