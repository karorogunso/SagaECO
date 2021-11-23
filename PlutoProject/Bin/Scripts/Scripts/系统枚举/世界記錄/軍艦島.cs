using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JJDFlags
    {
        神風精靈第一次對話 = 0x1,
        杰利科收集任务开始 = 0x2,
        杰利科收集任务结束 = 0x4,
        给予杰利科 = 0x8,
    }
}