using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project3_HT
{
    internal class Unsubscriber : IDisposable
    {
        private List<IObserver<Instruction>> _observers;
        private IObserver<Instruction> _observer;

        public Unsubscriber(List<IObserver<Instruction>> observers, IObserver<Instruction> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (!(_observer == null)) _observers.Remove(_observer);
        }
    }
}