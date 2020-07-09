using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainRapidFusion.Multiplication
{
    public class StateService
    {

        public State Get()
        {
            // get state from browser cache
            return new State();
        }

        public void Set(State state)
        {
            // save state to browser cache
        }

        public void Sync()
        {
            // sync browser cache with server
        }
    }
}
