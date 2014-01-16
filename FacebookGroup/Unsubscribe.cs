using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookGroup
{
    class Unsubscribe<WallPost>: IDisposable
    {
        private List<IObserver<WallPost>> _observers;
        private IObserver<WallPost> _observer;

        public Unsubscribe(List<IObserver<WallPost>> observers, IObserver<WallPost> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }
            
        public void Dispose()
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }
}
