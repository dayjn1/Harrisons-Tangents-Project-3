﻿// File name:                   Form1.cs
// Project name:                Project 2 - Harrison's Tangents
// ---------------------------------------------------------------------------
// Creator’s name:              Janine Day
// Edited By:                   Janine Day, Hannah Taylor, Nataliya Chibizova,
//                              Jason Middlebrook, Avery Marlow 
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
        List<Instruction> Save_Stats = new List<Instruction>();
        List<String> usedRegisters = new List<string>();                        //store stale registers  


        Stack<Instruction> Fetch = new Stack<Instruction>();                    // Creates the stacks for pipeline process -JND
        Stack<Instruction> Decode = new Stack<Instruction>();
        Stack<Instruction> Execute = new Stack<Instruction>();
        Stack<Instruction> Memory = new Stack<Instruction>();
        Stack<Instruction> Register = new Stack<Instruction>();
        
        int cycleCount = 0;                                                     // Counts the number of cycles
        int dataHazardCount = 0;                                                    //count hazards
        int structuralHazardCount = 0;
        int SimulationCount;
        int time = 1000;

        //use mostly in SaveFile to determine output calculations when a halt or invalid instruction is found - HT
        //only works when save after run? -- may need to scratch this idea
        //can use booleans in Simulation checks
        bool invalid = false; 
        bool halt = false;

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
                cycleCount = 0;
                dataHazardCount = 0;
                cycleLabel.Text = cycleCount.ToString();
                DHLabel.Text = dataHazardCount.ToString();
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
            usedRegisters.Clear();
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
            while (this.SimulationCount < this.Input_Instructions.Count)
            {
                CountUpdate();
                UpdateAndDelay();

                RegisterCycle();                // cycles/clears the register stack

                ProcessRegister();              // if instruction in memory goes to reg stack

                ProcessMemory();                // if instruction in execute goes to memory or register
                
                ProcessExecute();               // add check here as well for halt
                if (halt)
                {
                    return;
                }

                ProcessDecode();

                FetchCycle();

                //check if Invalid was called and return from Simulation
                //popup notice? --update label8 text to say an invlid was found -HT
                if (invalid)
                {
                    return; //return to what called Simulation
                }

            }

            // clean up pipeline

            while (this.Fetch.Count != 0 || this.Decode.Count != 0 || this.Execute.Count != 0 || this.Memory.Count != 0 || this.Register.Count != 0)
            {

                RegisterCycle();

                CountUpdate();
                UpdateAndDelay();

                ProcessRegister();

                ProcessMemory();

                ProcessExecute(); //call halt method within ProcessExecute and add check if halt was found to return/break from simulation loop
                if (halt)
                {
                    return;
                }

                ProcessDecode();                
            }

        }//end simulation

        /**
        * Method Name: InvalidFound()
        * Method Purpose: Method to pop all stacks and close/crash the program when illegal instruction is found
        *
        * <hr>
        * Date created: 03/07/2022
        * @Hannah Taylor
        * <hr>
        */
        public void InvalidFound()
        {
            invalid = true;
            label8.Text = "Invalid Instruction Found! Dumping Pipeline";

            UpdateAndDelay(); //give system time to update the text in fetch to show invalid (double delay to give time to read message
            UpdateAndDelay();
            //check each stack 
            if (this.Fetch.Count > 0)
            {
                this.Fetch.Pop();
                //update text
                ClearFetchText();

                Update();
            }
            if (this.Decode.Count > 0)
            {
                this.Decode.Pop();
                //update text
                ClearDecodeText();
                Update();
            }
            if (this.Execute.Count > 0)
            {
                this.Execute.Pop();
                //update text
                ClearExecuteText();

                Update();
            }
            if (this.Memory.Count > 0)//memory
            {
                this.Memory.Pop();
                //update text
                ClearMemoryText();

                Update();
            }
            if (this.Register.Count > 0)                //register
            {
                this.Register.Pop();
                //update text
                ClearRegisterText();

                Update();
            }

            //method call to close Gui?? --completes the crash
            //System.Windows.Forms.Application.Exit();
        }

        /**
        * Method Name: HaltFound()
        * Method Purpose: Method to finish simulation when halt reaches Execute before it is popped off
        *
        * <hr>
        * Date created: 03/07/2022
        * @Hannah Taylor
        * <hr>
        */
        public void HaltFound()
        {
            halt = true;
            //clear any instructions loaded in after halt
            if(this.Fetch.Count > 0)
            {
                this.Fetch.Pop();
                //update text
                ClearFetchText();
                Update();
            }
            if (this.Decode.Count > 0)
            {
                this.Decode.Pop();
                //update text
                ClearDecodeText();

                Update();
            }

            //keep going if anything in memory or writeback (halt is in Execute)
            while (this.Memory.Count > 0 || this.Register.Count > 0)
            {
                if(this.Register.Count > 0)
                {
                    this.Register.Pop();
                    ClearRegisterText();
                    UpdateAndDelay();
                }
                else if (this.Memory.Count > 0)
                {
                    Instruction temp = this.Memory.Peek();
                    if(temp.MemoryCC == 0)
                    {
                        temp = this.Memory.Pop();
                        ClearMemoryText();
                        if(temp.RegisterCC > 0)
                        {
                            PushRegister(temp);
                        }
                    }
                    else if (temp.MemoryCC > 0)
                    {
                        temp.MemoryCC--; //pop and push so actual instruction values in stack are updated
                        this.Memory.Pop();
                        this.Memory.Push(temp);
                    }
                    UpdateAndDelay();
                }
                //CountUpdate();  <-- I dont think this is needed -HT
            }

            //pop halt from execute
            this.Execute.Pop();
            ClearExecuteText();

        }
         

        /**
        * Method Name: KeepGoing(int)
        * Method Purpose: Process a single cycle while a stack is stalling
        *
        * <hr>
        * Date created: 03/06/2022
        * @Janine Day
        * <hr>
        * @param i - used to determine which stack is stalling
        */
        public void KeepGoing(int i)
        {
            if (i == 1) //decode stall
            {
                if (this.Register.Count > 0)    // register for one cycle
                {
                    RegisterCycle();
                    UpdateAndDelay();
                }

                if (this.Memory.Count > 0)      // memory for one cycle
                {
                    MemoryCycle();
                    UpdateAndDelay();
                }

                if (this.Execute.Count > 0)     // Execute for one cycle
                {
                    ExecuteCycle();
                    UpdateAndDelay();
                }

                // fetch for one cycle
                FetchCycle();
                UpdateAndDelay();

            }
            else if (i == 2) //execute stall
            {
                if (this.Register.Count > 0)        // register for one cycle
                {
                    RegisterCycle();
                    UpdateAndDelay();
                }

                if (this.Memory.Count > 0)          // memory for one cycle
                {
                    MemoryCycle();
                    UpdateAndDelay();
                }

                FetchCycle();
                UpdateAndDelay();

            }
            else if (i == 3) //memory stall
            {
                if (this.Register.Count > 0)
                {
                    RegisterCycle();
                    UpdateAndDelay();
                }

                if (this.Execute.Count == 1)
                {
                    ExecuteCycle();
                    UpdateAndDelay();

                }
                else if (this.Execute.Count == 0 && this.Decode.Count == 1)
                {
                    DecodeCycle();
                    UpdateAndDelay();
                }

                FetchCycle();
                UpdateAndDelay();

            }
        }

        /**
        * Method Name: RegisterCycle()
        * Method Purpose: Process a single register cycle
        *
        * <hr>
        * Date created: 03/06/2022
        * @Janine Day
        * <hr>
        */
        public void RegisterCycle()
        {
            if (this.Register.Count > 0)
            {
                Instruction wb = this.Register.Pop();
                usedRegisters.Remove(wb.DestReg);
                ClearRegisterText();
            }
        }

        /**
        * Method Name: MemoryCycle()
        * Method Purpose: Process a single memory cycle
        *
        * <hr>
        * Date created: 03/06/2022
        * @Janine Day
        * <hr>
        */
        public void MemoryCycle()
        {
            Instruction temp = this.Memory.Peek();

            if (temp.MemoryCC == 0)
            {
                temp = this.Memory.Pop();
                ClearMemoryText();

                if (temp.RegisterCC > 0)
                {
                    PushRegister(temp);
                }
            }
            else if (temp.MemoryCC > 0)
            {
                temp.MemoryCC--;
                this.Memory.Pop();
                this.Memory.Push(temp);
            }
        }

        /**
        * Method Name: ExecuteCycle()
        * Method Purpose: Process a single execute cycle
        *
        * <hr>
        * Date created: 03/06/2022
        * @Janine Day
        * <hr>
        */
        public void ExecuteCycle()
        {
            Instruction temp = this.Execute.Peek();

            if (temp.ExecuteCC == 0)
            {
                if (temp.ExecuteCC == 0 && temp.MemoryCC == 0 && temp.RegisterCC == 0)
                {
                    this.Execute.Pop();
                }
                else if (temp.MemoryCC > 0 && this.Memory.Count == 0)
                {
                    this.Execute.Pop();
                    ClearExecuteText();

                    PushMemory(temp);
                    MemoryText(temp);
                }
                else if (temp.RegisterCC > 0 && this.Register.Count == 0)
                {
                    this.Execute.Pop();
                    ClearExecuteText();

                    PushRegister(temp);
                    RegisterText(temp);
                }
                else if (temp.ExecuteCC > 0)
                {
                    temp.ExecuteCC--;
                    this.Execute.Pop();
                    this.Execute.Push(temp);
                }
            }
        }

        /**
        * Method Name: DecodeCycle()
        * Method Purpose: Process a single decode cycle
        *
        * <hr>
        * Date created: 03/06/2022
        * @Janine Day
        * <hr>
        */
        public void DecodeCycle()
        {
            Instruction temp = this.Decode.Peek();
            if (temp.DecodeCC == 0)
            {
                this.Decode.Pop();
                ClearDecodeText();

                PushExecute(temp);
            }
            else
            {
                temp.DecodeCC--;
                this.Decode.Pop();
                this.Decode.Push(temp);
            }

        }

        /**
        * Method Name: FetchCycle()
        * Method Purpose: Process a single fetch cycle
        *
        * <hr>
        * Date created: 03/06/2022
        * @Janine Day
        * <hr>
        */
        public void FetchCycle()
        {
            if(this.Fetch.Count == 0 && (this.SimulationCount < this.Input_Instructions.Count))
            {
                PushFetch(this.Input_Instructions[this.SimulationCount]);
                this.SimulationCount++;
                //Update();
                //add InvalidFound call after check-HT
                Instruction temp = this.Fetch.Peek(); //since just called PushFetch, there is no need to check Fetch.Count -HT
                uint op = temp.OpCode;
                //these checks are spread out for ease of understanding rather than one large convoluted if condition
                /*if (op < 0)
                    InvalidFound();
                if (op > 20 && op < 128)
                    InvalidFound();
                if (op > 131)
                    InvalidFound();*/   // if invalid, op code == 404
                if (temp.OpCode == 404)
                    InvalidFound();
            }
            else if (this.Decode.Count == 0 && this.Fetch.Count > 0)     // decode for one cycle
            {
                Instruction temp = this.Fetch.Pop();
                PushDecode(temp);
            }
        }



        
        // See if an operand register in an instruction is being used already, and stall until it is free.
        //Avery 
        public void CompareOpRegisters(Instruction i)
        {
            //check if its in the stale registers
            //if (usedRegisters.Contains(i.Reg1) && i.Reg1 != null || usedRegisters.Contains(i.Reg2) && i.Reg2 != null)
            //{
            //    hazardCount++;
            //    label7.Text = hazardCount.ToString();
            //    CountUpdate(); //go stall
            //}
            //else
            //{
            //    return; //if not, leave
            //}
            //CompareOpRegisters(i); //try again otherwise
            while((usedRegisters.Contains(i.Reg1) && i.Reg1 != null || usedRegisters.Contains(i.Reg2) && i.Reg2 != null))
            {
                dataHazardCount++;
                DHLabel.Text = dataHazardCount.ToString();
                KeepGoing(1);
                CountUpdate(); //go stall
            }
        }


        /**
        * Method Name: ProcessDecode()
        * Method Purpose: Pops from fetch and pushes onto decode if instruction needs to be decoded (i.DecodeCC)
        *                 Uses that value to check if a stall will occur, which will be resolved within a while loop
        *                 Uses the KeepGoing method to process the other stacks while stalling
        *
        * <hr>
        * Date created: 03/01/2022
        * @Janine Day
        * <hr>
        */
        public void ProcessDecode()
        {
            if (this.Fetch.Count > 0)
            {
                Instruction i = this.Fetch.Pop();
                ClearFetchText();

                CompareOpRegisters(i);
                if (i.writeBack == true)
                {
                    CheckRegisters(i);
                }
                if (i.DecodeCC != 0)
                {
                    PushDecode(i);

                    UpdateAndDelay();

                    while (i.DecodeCC > 0)
                    {
                        i.DecodeCC--;

                        structuralHazardCount++;
                        SHLabel.Text = structuralHazardCount.ToString();

                        CountUpdate();
                        UpdateAndDelay();
                    }
                }
            }
        }


        /**
        * Method Name: ProcessExecute()
        * Method Purpose: Pops from Decode and pushes onto Execute if instruction needs to be executed (i.ExecuteCC)
        *                 Uses that value to check if a stall will occur, which will be resolved within a while loop
        *                 Uses the KeepGoing method to process the other stacks while stalling
        *
        * <hr>
        * Date created: 03/01/2022
        * @Janine Day
        * <hr>
        */
        public void ProcessExecute()
        {
            if (this.Decode.Count > 0)
            {
                Instruction i = this.Decode.Pop();
                ClearDecodeText();

                if (i.ExecuteCC != 0)
                {
                    PushExecute(i);
                    UpdateAndDelay();
                    //check for halt after i is loaded into Execute
                    uint op = i.OpCode;
                    if (op == 0)
                    {
                        HaltFound();
                        return; //halt should clean up what the below while statement does...? I think
                    }

                    while (i.ExecuteCC > 0)
                    {
                        i.ExecuteCC--;

                        structuralHazardCount++;
                        SHLabel.Text = structuralHazardCount.ToString();

                        CountUpdate();
                        UpdateAndDelay();
                    }
                }
            }
        }


        /**
        * Method Name: ProcessMemory()
        * Method Purpose: Pops from Execute and pushes onto Memory (or Register) if instruction needs to access memory or writeback (i.MemoryCC OR i.RegisterCC)
        *                 Uses that value to check if a stall will occur, which will be resolved within a while loop
        *                 Uses the KeepGoing method to process the other stacks while stalling
        *
        * <hr>
        * Date created: 03/01/2022
        * @Janine Day
        * <hr>
        */
        public void ProcessMemory()
        {
            if (this.Execute.Count > 0)
            {
                Instruction i = this.Execute.Pop();
                ClearExecuteText();

                if (i.MemoryCC != 0)
                {
                    PushMemory(i);
                    UpdateAndDelay();

                    while (i.MemoryCC > 0)
                    {
                        i.MemoryCC--;

                        structuralHazardCount++;
                        SHLabel.Text = structuralHazardCount.ToString();

                        CountUpdate();
                        UpdateAndDelay();
                    }
                }
                else if (i.RegisterCC != 0)
                {
                    PushRegister(i);
                    UpdateAndDelay();


                    while (i.RegisterCC > 0)
                    {
                        i.RegisterCC--;

                        structuralHazardCount++;
                        SHLabel.Text = structuralHazardCount.ToString();

                        CountUpdate();
                        UpdateAndDelay();
                    }
                }
            }
        }


        /**
        * Method Name: ProcessRegister()
        * Method Purpose: Pops from Memory and pushes onto Register if instruction needs to writeback to a register (i.RegisterCC)
        *                 Uses that value to check if a stall will occur, which will be resolved within a while loop
        *                 Uses the KeepGoing method to process the other stacks while stalling
        *
        * <hr>
        * Date created: 03/01/2022
        * @Janine Day
        * <hr>
        */
        public void ProcessRegister()
        {
            if (this.Memory.Count > 0)
            {
                Instruction i = this.Memory.Pop();
                ClearMemoryText();

                if (i.RegisterCC != 0)
                {
                    PushRegister(i);

                    UpdateAndDelay();


                    while (i.RegisterCC > 0)
                    {
                        i.RegisterCC--;

                        structuralHazardCount++;
                        SHLabel.Text = structuralHazardCount.ToString();

                        CountUpdate();
                        UpdateAndDelay();
                    }
                }
            }
        }


        /// <summary>Accepts an instruction and checks if its registers are available.
        /// On fail it waits until the registers are available. Used to get the registers ready to push.</summary>
        /// AM
        /// <param name="i">Instruction passed in from Process Registers</param>
        public void CheckRegisters(Instruction i)
        {

            //see if register is available by checking the usedRegisters list
            if (usedRegisters.Contains(i.DestReg))
            {
                while (usedRegisters.Contains(i.DestReg))
                {
                    dataHazardCount++;
                    DHLabel.Text = dataHazardCount.ToString();
                    KeepGoing(1);
                }
            }
            else
            {
                usedRegisters.Add(i.DestReg);
            }
            //else//if the register is in use already, wait until it is not.
            //{
            //    hazardCount++;

            //    Task.Delay(time).Wait();
            //    usedRegisters.Remove(i.DestReg);
            //}

        }

        /**
        * Method Name: CountUpdate()
        * Method Purpose: Increments cycle count and changes text to reflect new amount
        *                 For simplicity purposes
        *
        * <hr>
        * Date created: 03/01/2022
        * @Janine Day
        * <hr>
        */
        public void CountUpdate()
        {
            this.cycleCount++;
            cycleLabel.Text = cycleCount.ToString();
        }

        /**
        * Method Name: UpdateAndDelay()
        * Method Purpose: Updates the entire form so changes can be seen, delays so that changes can be seen
        *                 For simplicity purposes
        *
        * <hr>
        * Date created: 03/01/2022
        * @Janine Day
        * <hr>
        */
        public void UpdateAndDelay()
        {
            Update();
            Task.Delay(time).Wait();
        }

        /**
        * Method Name: PushFetch(Instruction)
        * Method Purpose: Pushes param onto Fetch stack, used for simplicity purposes 
        *
        * <hr>
        * Date created: 03/01/2022
        * @Janine Day
        * <hr>
        * @param Instruction i - Pushed onto Fetch Stack, decrements value for i, and then text is changed
        */
        public void PushFetch(Instruction i)
        {
            i.FetchCC--;
            this.Fetch.Push(i);
            FetchText(i);
        }

        /**
        * Method Name: PushDecode(Instruction)
        * Method Purpose: Pushes param onto Decode stack, used for simplicity purposes 
        *
        * <hr>
        * Date created: 03/01/2022
        * @Janine Day
        * <hr>
        * @param Instruction i - Pushed onto Decode Stack, decrements value for i, and then text is changed
        */
        public void PushDecode(Instruction i)
        {
            i.DecodeCC--;
            this.Decode.Push(i);
            DecodeText(i);
        }

        /**
        * Method Name: PushExecute(Instruction)
        * Method Purpose: Pushes param onto Execute stack, used for simplicity purposes 
        *
        * <hr>
        * Date created: 03/01/2022
        * @Janine Day
        * <hr>
        * @param Instruction i - Pushed onto Execute Stack, decrements value for i, and then text is changed
        */
        public void PushExecute(Instruction i)
        {
            i.ExecuteCC--;
            this.Execute.Push(i);
            ExecuteText(i);
        }

        /**
        * Method Name: PushMemory(Instruction)
        * Method Purpose: Pushes param onto Memory stack, used for simplicity purposes 
        *
        * <hr>
        * Date created: 03/01/2022
        * @Janine Day
        * <hr>
        * @param Instruction i - Pushed onto Memory Stack, decrements value for i, and then text is changed
        */
        public void PushMemory(Instruction i)
        {
            i.MemoryCC--;
            this.Memory.Push(i);
            MemoryText(i);
        }

        /**
        * Method Name: PushRegister(Instruction)
        * Method Purpose: Pushes param onto Register stack, used for simplicity purposes 
        *
        * <hr>
        * Date created: 03/01/2022
        * @Janine Day
        * <hr>
        * @param Instruction i - Pushed onto Register Stack, decrements value for i, and then text is changed
        */
        public void PushRegister(Instruction i)
        {
            i.RegisterCC--;
            this.Register.Push(i);
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
            RegisterDR.Text = i.DestReg;
            RegisterOp1.Text = i.Reg1;
            RegisterOp2.Text = i.Reg2;
        }


        /**
        * Method Name: ClearRegisterText()
        * Method Purpose: Updates RegisterText content
        *
        * <hr>
        * Date created: 03/09/2022
        * @Janine Day
        * <hr>
        */
        public void ClearRegisterText()
        {
            RegisterBox.Text = "";
            RegisterDR.Text = "";
            RegisterOp1.Text = "";
            RegisterOp2.Text = "";
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
            MemoryDR.Text = i.DestReg;
            MemoryOp1.Text = i.Reg1;
            MemoryOp2.Text = i.Reg2;
        }


        /**
        * Method Name: ClearMemoryText()
        * Method Purpose: Updates MemoryText content
        *
        * <hr>
        * Date created: 03/09/2022
        * @Janine Day
        * <hr>
        */
        public void ClearMemoryText()
        {
            MemoryBox.Text = "";
            MemoryDR.Text = "";
            MemoryOp1.Text = "";
            MemoryOp2.Text = "";
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
            ExecuteDR.Text = i.DestReg;
            ExecuteOp1.Text = i.Reg1;
            ExecuteOp2.Text = i.Reg2;
        }


        /**
        * Method Name: ClearExecuteText()
        * Method Purpose: Updates ExecuteText content
        *
        * <hr>
        * Date created: 03/09/2022
        * @Janine Day
        * <hr>
        */
        public void ClearExecuteText()
        {
            ExecuteBox.Text = "";
            ExecuteDR.Text = "";
            ExecuteOp1.Text = "";
            ExecuteOp2.Text = "";
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
            DecodeDR.Text = i.DestReg;
            DecodeOp1.Text = i.Reg1;
            DecodeOp2.Text = i.Reg2;
        }

        

        /**
        * Method Name: ClearDecodeText()
        * Method Purpose: Updates DecodeText content
        *
        * <hr>
        * Date created: 03/09/2022
        * @Janine Day
        * <hr>
        */
        public void ClearDecodeText()
        {
            DecodeBox.Text = "";
            DecodeDR.Text = "";
            DecodeOp1.Text = "";
            DecodeOp2.Text = "";
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
            FetchDR.Text = i.DestReg;
            FetchOp1.Text = i.Reg1;
            FetchOp2.Text = i.Reg2;
        }

        /**
        * Method Name: ClearFetchText()
        * Method Purpose: Updates FetchText content
        *
        * <hr>
        * Date created: 03/09/2022
        * @Janine Day
        * <hr>
        */
        public void ClearFetchText()
        {
            FetchBox.Text = "";
            FetchDR.Text = "";
            FetchOp1.Text = "";
            FetchOp2.Text = "";
        }

        public static string dirParameter = AppDomain.CurrentDomain.BaseDirectory + @"\saveOut.txt";
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OpenFileDialog SaveDlg = new OpenFileDialog();
            //string fileN = SaveDlg.FileName;
            FileStream fParameter = new FileStream(dirParameter, FileMode.Create, FileAccess.Write);
            StreamWriter filewrite = new StreamWriter(fParameter);

            filewrite.WriteLine("   Instruction         |     Registers         |    Fetch    |    Decode    |    Execute    |    Memory    |    WriteBack ");
            filewrite.WriteLine("___________________________________________________________________________________________________________________________");

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

                Instruction inst = Save_Stats[i];
                string destR = inst.DestReg;
                string opnd1 = inst.Reg1;
                string opnd2 = inst.Reg2;

                string fullOperand = " ";
                if (Save_Stats[i].Mnemonic != "HALT" && Save_Stats[i].Mnemonic != "INVALID")
                {
                    if (destR != null)
                        fullOperand += destR + ", ";
                    if (opnd1 != null)
                        fullOperand += opnd1 + ", ";
                    if (opnd2 != null)
                        fullOperand += opnd2; 
                }

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
                    else if(Save_Stats[i].Mnemonic == "HALT")
                    {
                        if(memE > executeE)
                        {
                            executeE = memE + 1;
                            exec = executeE.ToString();
                        }
                        else
                        {
                            exec = executeE.ToString();
                        }
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
                    else if (Save_Stats[i].Mnemonic == "HALT")
                    {
                        if (memE > executeE)
                        {
                            executeE = memE + 1;
                            exec = executeE.ToString();
                        }
                        else
                        {
                            exec = executeE.ToString();
                        }
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


                filewrite.WriteLine("      " + Save_Stats[i].Mnemonic.PadRight(6, ' ') + "               " + 
                                    fullOperand.PadRight(12, ' ') + "              " +
                                   (fetch) + "             " +
                                   (decode.PadRight(7, ' ')) + "         " +
                                   (exec) + "              " +
                                   (memo.PadRight(9, ' ')) + "       " +
                                   (stringWB) + "          ");


                if (Save_Stats[i].Mnemonic == "HALT" || Save_Stats[i].Mnemonic == "INVALID")
                {
                    break;
                }
                


            }//end for loop for printing Instruction log


            filewrite.WriteLine();
            filewrite.WriteLine("___________________________________________________________________________________________________________________________");
            filewrite.WriteLine();
            filewrite.WriteLine("Cycle count: " + cycleCount);
            filewrite.WriteLine("Data Hazard count: " + dataHazardCount);
            filewrite.WriteLine("Structural Hazard count: " + structuralHazardCount);



            filewrite.Flush();
            filewrite.Close();
            label8.Text = "Saved";


        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input_Instructions.Clear();        
            Save_Stats.Clear();
            usedRegisters.Clear();


            Fetch.Clear();
            Decode.Clear();
            Execute.Clear();
            Memory.Clear();
            Register.Clear();

            cycleCount = 0;
            cycleLabel.Text = cycleCount.ToString();

            dataHazardCount = 0;
            DHLabel.Text = dataHazardCount.ToString();

            structuralHazardCount = 0;
            SHLabel.Text = structuralHazardCount.ToString();
            
            SimulationCount = 0;

            invalid = false;
            halt = false;
        }
    }
}
