using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_HT
{
    class Instruction
    {
        String Mnemonic;
        int OpCode;
        int DestReg;
        int Operand;

        public Instruction(int instr)
        {
            //this.Mnemonic; // alter disassemble to give only mnemonic, possibly list of global values -JD

        }

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
            {20, "MOV" }

        };

        public static string[] disassemble(int input, int pc)
        {
            string am = "";     //addressing mode
            //separate the opcode
            uint instruction = (uint)input & 0xFF000000;   //must be unsigned so that r shift (>>) is logical, not arithmetic -J
            instruction >>= 24;

            string val = "";
            //added condition logic to check for valid parsed instruction (** not finished yet) -H
            if (InstructionSet.ContainsKey(instruction))
            {
                val = InstructionSet[instruction];
            }
            else
            {
                Console.WriteLine("Invalid instruction found. Closing program.");
                return new string[] { instruction.ToString("X"), "INV" };//may need to use stop/exit here; needed to return something -J
            }


            //determine the address type
            //update -- we got rid of this field
            if (val == "LOAD" || val == "STOR")
            {
                am = "i";
            }
            else if (val == "HALT")
            {
                am = "c";
            }
            else
                am = "r";

            return new string[] { instruction.ToString("X"), val, am };
        }//end disassemble


    }
}
