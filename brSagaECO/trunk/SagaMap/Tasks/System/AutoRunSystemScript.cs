using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaDB.LevelLimit;

using SagaMap.Network.Client;
using SagaMap.Manager;
namespace SagaMap.Tasks.System
{
    public class AutoRunSystemScript : MultiRunTask
    {
        uint ID = 0;
        public AutoRunSystemScript(uint EventID)
        {
            this.period = 5000;
            this.dueTime = 10000;
            ID = EventID;
        }

        public override void CallBack()
        {
            if (ScriptManager.Instance.Events.ContainsKey(ID))
            {
                Scripting.Event evnt = ScriptManager.Instance.Events[ID];
                evnt.OnEvent(ScriptManager.Instance.VariableHolder);
                Logger.ShowInfo("done Read Script：" + evnt.ToString());
            }
            this.Deactivate();
        }
    }
}
