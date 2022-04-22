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
            Cache cache = new Cache();
            Instruction i = new Instruction(0x03F67890);

           

            cache.DeconstructInstruction(i);
            //01 is our opcode
            //F is our destination register
            //6789 is our index and tag
            //Index should be 1
            //0 is our offset
            //Tag should be 19E2

            /*//Please god uncomment this -jfm (Debugging)
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Controller());*/
        }
    }
}
