// ---------------------------------------------------------------------------
// File name:                   Instruction.cs
// Project name:                Project 2 - Harrison's Tangents
// ---------------------------------------------------------------------------
// Creator’s name:              Janine Day
// Edited By:                   Janine Day, 
// Course-Section:              CSCI-4717
// Creation Date:               02/17/2022
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_HT
{
    /**
    * Class Name:       Instruction
    * Class Purpose:    Holds all information required for pipeline process (decoding and parsing through information)
    *
    * <hr>
    * Date created: 02/17/2022
    * @Janine Day
    */
    public class Instruction
    {
        public string Mnemonic;
        uint OpCode;
        string DestReg;
        string Reg1;
        string Reg2;
        int FetchCC, DecodeCC, ExecuteCC, MemoryCC, RegisterCC;

        

        public static Dictionary<uint, string> InstructionSet = new Dictionary<uint, string>
        {
            { 0, "HALT"},       
            { 1, "LOAD"},       
            { 2, "STOR"},
            { 3, "ADD"},
            { 4, "ADDI"},
            { 5, "SUB"},
            { 6, "SUBI" },
            { 7, "BR" },
            { 8, "BRLT" },
            { 9, "BRLE" },
            {10, "BREQ" },
            {11, "BRNE"},
            {12, "BRGT" },
            {13, "BRGE" },
            {14, "AND" },
            {15, "OR" },
            {16, "NOT" },
            {17, "NEG" },
            {18, "ASL" },
            {19, "ASR" },
            {20, "MOV" },
            {404, "INVALID" }           // Invalid code, 404 isn't possible to reach, op code 8 bits (255 decimal) -JND

        };

        List<Instruction> InstructionSetNew = new List<Instruction>
        {
            new Instruction( 0, "Halt", 1, 1, 1, 0, 0),
            new Instruction( 1, "LOAD", 1, 2, 1, 3, 1),
            new Instruction( 2, "STOR", 1, 2, 1, 3, 0),
            new Instruction( 3,  "ADD", 1, 1, 1, 0, 1),
            new Instruction( 4, "ADDI", 1, 1, 1, 0, 1),
            new Instruction( 5,  "SUB", 1, 1, 1, 0, 1),
            new Instruction( 6, "SUBI", 1, 1, 1, 0, 1),
            new Instruction( 7,   "BR", 1, 1, 1, 0, 0),
            new Instruction( 8, "BRLT", 1, 1, 1, 0, 0),
            new Instruction( 9, "BRLE", 1, 1, 1, 0, 0),
            new Instruction(10, "BREQ", 1, 1, 1, 0, 0),
            new Instruction(11, "BRNE", 1, 1, 1, 0, 0),
            new Instruction(12, "BRGT", 1, 1, 1, 0, 0),
            new Instruction(13, "BRGE", 1, 1, 1, 0, 0),
            new Instruction(14,  "AND", 1, 1, 1, 0, 1),
            new Instruction(15,   "OR", 1, 1, 1, 0, 1),
            new Instruction(16,  "NOT", 1, 1, 1, 0, 1),
            new Instruction(17,  "NEG", 1, 1, 1, 0, 1),
            new Instruction(18,  "ASL", 1, 1, 1, 0, 1),
            new Instruction(19,  "ASR", 1, 1, 1, 0, 1),
            new Instruction(20,  "MOV", 1, 1, 1, 0, 1),
            new Instruction(404, "INVALID", 1, 1, 0, 0, 0)

        };

        public void FindIS()
        {
            for(int i = 0; i < InstructionSetNew.Count; i++)
            {
                if(this.OpCode == InstructionSetNew[i].OpCode)
                {
                    this.Mnemonic = 
                }    
            }
        }


        /**
        * Method Name: Instruction(int)
        * Method Purpose: Takes an instruction in hex and creates an instruction
        *
        * <hr>
        * Date created: 02/17/2022
        * @Janine Day
        * <hr>
        * @param int - hex input to covert into instruction
        */
        public Instruction(int instr)
        {
            Disassemble(instr);
        }

        public Instruction(uint opcode, string mnemonic, int fetch, int decode, int execute, int memory, int register)
        {
            this.OpCode = opcode;
            this.Mnemonic = mnemonic;
            this.FetchCC = fetch;
            this.DecodeCC = decode;
            this.ExecuteCC = execute;
            this.MemoryCC = memory;
            this.RegisterCC = register;
        }


        /**
        * Method Name: disassemble(int)
        * Method Purpose: Takes the hex and separate each component into an instruction type global variable
        *
        * <hr>
        * Date created: 02/17/2022
        * @Janine Day
        * <hr>
        * @param int - hex input to covert into instruction
        */
        public void Disassemble(int input)
        {
            //separate the opcode
            this.OpCode = (uint)input & 0xFF000000;   //must be unsigned so that r shift (>>) is logical, not arithmetic -JM
            this.OpCode >>= 24;

            this.Mnemonic = "";

            FindIS();

            /*if (InstructionSet.ContainsKey(this.OpCode))
            {
                this.Mnemonic = InstructionSet[this.OpCode];
            }
            else
            {
                this.OpCode = 404;                            // Creates illegal instruction
                this.Mnemonic = InstructionSet[this.OpCode];
            }*/

            uint rd = (uint)input & 0x00F00000;
            rd >>= 20;
            this.DestReg = "R" + rd.ToString("X");            // Uses shifts to isolate certain bits in instruction hex - JND
                                                              // Sets Destination reg, reg 1, and reg 2
            uint reg1 = (uint)input & 0x000F0000;
            reg1 >>= 16;
            this.Reg1 = "R" + reg1.ToString("X");

            uint reg2 = (uint)input & 0x0000F000;
            reg2 >>= 12;
            this.Reg2 = "R" + reg2.ToString("X");

        }//end disassemble

    }
}
