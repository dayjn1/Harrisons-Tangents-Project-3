using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    internal interface IResStation
    {
        /// <summary>
        /// Called by the CDB when it wants to transmit results from a functional unit
        /// Res station can check if it needs the value, ignore it otherwise
        /// </summary>
        /// <param name="result">The instruction that was just executed and is being sent along the CDB</param>
        void ReceiveResults(Instruction result);
    }
}
