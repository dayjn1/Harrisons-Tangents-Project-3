// ---------------------------------------------------------------------------
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
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Project2_HT
{
    /**
    * Class Name:       Tangents
    * Class Purpose:    Main form design where pipeline process takes place
    *
    * <hr>
    * Date created: 02/17/2022
    * @Janine Day
    */
    public partial class Tangents : Form
    {
        List<Instruction> Input_Instructions = new List<Instruction>();         // Creates a list of Instruction class types -JND
        Stack<Instruction> Fetch = new Stack<Instruction>();                    // Creates a stack for Fetch section of pipeline -JND
        Stack<Instruction> Decode = new Stack<Instruction>();                   // Creates a stack for Decode section of pipeline -JND
        Stack<Instruction> Execute = new Stack<Instruction>();                  // Creates a stack for Execute section of pipeline -JND
        Stack<Instruction> Memory = new Stack<Instruction>();                   // Creates a stack for Memory section of pipeline -JND
        Stack<Instruction> Register = new Stack<Instruction>();                 // Creates a stack for Register section of pipeline -JND

        public Tangents()
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

                    //try to parse one line of input, converting hexadecimal to int and sending to disassembler -H, J
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

        private void StartButton_Click(object sender, EventArgs e)
        {
            Simulation();
        }

        public void Simulation()
        {
            Instruction temp;

            for (int i = 0; i < this.Input_Instructions.Count; i++)
            {

                if (this.Register.Count > 0)
                {
                    this.Register.Pop();
                }


                if (this.Memory.Count > 0)
                {
                    temp = this.Memory.Pop();
                    this.Register.Push(temp);
                    RegisterText(temp);
                }

                if (this.Execute.Count > 0)
                {
                    temp = this.Execute.Pop();
                    this.Memory.Push(temp);
                    MemoryText(temp);
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
                    this.Decode.Push(temp);
                    DecodeText(temp);
                }

                this.Fetch.Push(this.Input_Instructions[i]);
                FetchText(this.Input_Instructions[i]);
            }
        }

        public void RegisterText(Instruction i)
        {
            RegisterBox.Text = i.Mnemonic;
        }

        public void MemoryText(Instruction i)
        {
            MemoryBox.Text = i.Mnemonic;
        }

        public void ExecuteText(Instruction i)
        {
            ExecuteBox.Text = i.Mnemonic;
        }
        public void DecodeText(Instruction i)
        {
            DecodeBox.Text = i.Mnemonic;
        }
        public void FetchText(Instruction i)
        {
            FetchBox.Text = i.Mnemonic;
            RegisterBox.Update();
            MemoryBox.Update();
            ExecuteBox.Update();
            DecodeBox.Update();
            FetchBox.Update();
            System.Threading.Thread.Sleep(1000);
        }
    }
}
