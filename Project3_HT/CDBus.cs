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
    /// <summary>
    /// The common data bus passes info from functional units back to reservation stations and to the ROB
    /// </summary>
    internal class CDBus : IObserver<Instruction>, IObservable<Instruction>
    {
        private List<IObserver<Instruction>> observers;

        public CDBus(List<IObserver<Instruction>> ResStations)
        {
            observers = new List<IObserver<Instruction>>();
            foreach (var station in ResStations)
            {
                observers.Add(station);
            }
        }

        public CDBus()
        {
            observers = new List<IObserver<Instruction>>();
        }

        public void AddResStations(List<IObserver<Instruction>> resStations)
        {
            foreach (var station in resStations)
                observers.Add(station);
        }

        /// <summary>
        /// This method will be called when the CDB is given instruction results from functional units.
        /// The goal is to send these results, including the rd info, to res stations and ROB.
        /// </summary>
        /// <param name="instr">The instruction that is being passed to the CDB</param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnNext(Instruction instr)
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].OnNext(instr);
            }
            ReorderBuffer.OnNext(instr);

        }//end OnNext()

        /// <summary>
        /// Observers individually subscribe, adds observer to observers List<>
        /// </summary>
        /// <param name="observer">An individual observer to be added to observers</param>
        /// <returns>Observer calls unsubscriber.Dispose() to unsubscribe</returns>
        public IDisposable Subscribe(IObserver<Instruction> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        public void OnCompleted()
        {
            //do nothing
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

    }//end CDBus class
}