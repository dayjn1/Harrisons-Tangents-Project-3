// ---------------------------------------------------------------------------
// File name:                   CDBus.cs
// Project name:                Project 2 - Harrison's Tangents
// Creator’s name:              Jason Middlebrook
// Course-Section:              CSCI 4717-201
// Creation Date:               03/28/2022
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project3_HT
{
    ///Check each functional unit to see if it's done executing, receive results
    ///from left to right on the pipeline and make sure that you're iterating
    ///through which functional units we receive from every cycle
    /// 
    /// <summary>
    /// The common data bus passes info from functional units back to reservation stations and to the ROB
    /// </summary>
    internal class CDBus
    {
        private List<IResStation> resStations;
        public Instruction currentInstruction;

        public CDBus(List<IResStation> ResStations)
        {
            resStations = new List<IResStation>();
            foreach (var station in ResStations)
            {
                resStations.Add(station);
            }
        }

        public CDBus()
        {
            resStations = new List<IResStation>();
        }

        public void AddResStations(List<IResStation> resStations)
        {
            foreach (var station in resStations)
                resStations.Add(station);
        }

        /// <summary>
        /// Called every cycle:
        ///     Check if value on CDB
        ///     if yes, check res.stations one by one if they need the data before pushing to reorder buf
        ///     if no, do nothing
        /// </summary>
        public void Cycle()
        {
            SendResults();
        }
        
        public void SendResults()
        {
            if (currentInstruction != null)
            {
                for (int i = 0; i < resStations.Count; i++)
                {
                    resStations[i].ReceiveResults(currentInstruction);
                }
            }
            ReorderBuffer.PassedtoRB(currentInstruction);
            currentInstruction = null;
        }

        /// <summary>
        /// This method will be called when the CDB is given instruction results from functional units.
        /// The goal is to send these results, including the rd info, to res stations and ROB.
        /// </summary>
        /// <param name="instr">The instruction that is being passed to the CDB</param>
        public void ReceiveResults(Instruction instr)
        {
            this.currentInstruction = instr;

        }//end ReceiveResults()

    }//end CDBus class
}