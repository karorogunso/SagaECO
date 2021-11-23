using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum FallenTitantia
    {
        任务开始 = 0x1,
        给花 = 0x2,
        任务完成 = 0x4,
        告知需要花 = 0x8,
        大导师告知可以回来了 = 0x10,
    }
}