using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SagaLib
{
    public class RegisteredManager : Singleton<RegisteredManager>
    {
        public List<MultiRunTask> registered = new List<MultiRunTask>();
    }
}
