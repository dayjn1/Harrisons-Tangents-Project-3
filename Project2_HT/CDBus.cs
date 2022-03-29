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

        public CDBus(List<IObserver<Instruction>> ResStations, ReorderBuffer ROB)
        {
            observers = new List<IObserver<Instruction>>();
            foreach (var station in ResStations)
            {
                observers.Add(station);
            }
            observers.Add( (IObserver<Instruction>)ROB );
        }

        public CDBus()
        {
            observers = new List<IObserver<Instruction>>();
        }

        public void AddROB(ReorderBuffer rob)
        {
            observers.Add((IObserver<Instruction>)rob);
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

        }//end OnNext()

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
