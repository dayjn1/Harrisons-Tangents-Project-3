﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3_HT
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Test the Cache -jfm
            Cache cache = new Cache();
            Instruction i = new Instruction(0x03F13579);
            Console.WriteLine();

            cache.Add(new Instruction(0x03F13579));
            cache.Add(new Instruction(0x03F14579));
            cache.Add(new Instruction(0x03F15579));
            cache.Add(new Instruction(0x03F16579));
            Console.WriteLine("Last instruction should be 16579, tag should be 595");
            Console.WriteLine(cache.ToString());
            cache.Add(new Instruction(0x03F17579));
            cache.Add(new Instruction(0x03F18579));
            cache.Add(new Instruction(0x03F19579));
            cache.Add(new Instruction(0x03F1A579));
            cache.Add(new Instruction(0x03F1B579));
            Console.WriteLine("Last instruction should be 1B579, tag should be 6D5");
            Console.WriteLine(cache.ToString());

            //01 is our opcode
            //F is our destination register
            //0 is our offset
            //6789 is our index and tag
            //Index should be 1
            //Tag should be 19E2

            /*//Please god uncomment this -jfm (Debugging)
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Controller());*/
        }
    }
}
