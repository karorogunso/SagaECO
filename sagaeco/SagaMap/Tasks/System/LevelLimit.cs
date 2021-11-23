using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaDB.LevelLimit;

using SagaMap.Network.Client;
namespace SagaMap.Tasks.System
{
    public class LevelLimit : MultiRunTask
    {
        public LevelLimit()
        {
            this.period = 60000;
            this.dueTime = 0;
        }
        static LevelLimit instance;

        public static LevelLimit Instance
        {
            get
            {
                if (instance == null)
                    instance = new LevelLimit();
                return instance;
            }
        }
        public override void CallBack()
        {
            SagaDB.LevelLimit.LevelLimit LL = SagaDB.LevelLimit.LevelLimit.Instance;
            if (DateTime.Now > LL.NextTime)
            {
                SagaMap.LevelLimit.LevelLimitManager.Instance.UpdataLevelLimit();
            }
        }
    }
}
