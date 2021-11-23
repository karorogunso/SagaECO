using System;
using System.Collections.Generic;
using System.Text;

namespace SagaMap.Partner
{
    public interface AICommand //Interface for all AI commands
    {
        string GetName();
        void Update(object para);
        CommandStatus Status { get; set; }
        void Dispose();
    }

    public enum CommandStatus
    {
        INIT,
        RUNNING,
        RUNNING_NOTUPDATE,
        PAUSED,
        FINISHED,
        DELETING,
    }

}
