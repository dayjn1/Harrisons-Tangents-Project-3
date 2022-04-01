﻿using Project3_HT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Project3_HT
{
    public class AddressUnit
    {
        public static void ProcessAU(Instruction i)
        {
            if (i.OpCode == 1)
            {
                LoadBuffer.AddToBuffer(i);
            }
            else
            {
                // check if there is a space on the reoder buffer.
                // If it is, place it on the RO
                // else, stull
                //send the whole instruction to reorder buffer whenever that gets done.
                ReorderBuffer.AddToReorderBuf(i);
            }
        }
    }
}