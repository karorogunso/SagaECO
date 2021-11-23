using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum YGLFlags
    {
        石像收集任务开始 = 0x1,
        给予石像 = 0x2,
        石像收集任务结束 = 0x4,
    }
}