using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_HT
{
    class Instruction
    {
        string Mnemonic;
        uint OpCode;
        string DestReg;
        string Operand;

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
        public Instruction(int instr)
        {
            disassemble(instr);
        }


        public void disassemble(int input)
        {
            //separate the opcode
            this.OpCode = (uint)input & 0xFF000000;   //must be unsigned so that r shift (>>) is logical, not arithmetic -J
            this.OpCode >>= 24;

            this.Mnemonic = "";
            //added condition logic to check for valid parsed instruction (** not finished yet) -H
            if (InstructionSet.ContainsKey(this.OpCode))
            {
                this.Mnemonic = InstructionSet[this.OpCode];
            }
            else
            {
                this.OpCode = 404;
                this.Mnemonic = InstructionSet[this.OpCode];
            }

            uint operand = (uint)input & 0x00FFFFFF;
            this.Operand = operand.ToString("X");

            uint rd = (uint)input & 0x00F00000;
            rd >>= 20;
            this.DestReg = rd.ToString("X");

        }//end disassemble




    }
}
