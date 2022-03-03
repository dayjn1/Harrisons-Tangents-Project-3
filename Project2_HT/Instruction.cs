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
            disassemble(instr);
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
        public void disassemble(int input)
        {
            //separate the opcode
            this.OpCode = (uint)input & 0xFF000000;   //must be unsigned so that r shift (>>) is logical, not arithmetic -JM
            this.OpCode >>= 24;

            this.Mnemonic = "";
            //added condition logic to check for valid parsed instruction (** not finished yet) -H
            if (InstructionSet.ContainsKey(this.OpCode))
            {
                this.Mnemonic = InstructionSet[this.OpCode];
            }
            else
            {
                this.OpCode = 404;                            // Creates illegal instruction
                this.Mnemonic = InstructionSet[this.OpCode];
            }

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
