using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum ExpandedArea
    {
        已跟南平原運送員對話 = 0x1,
        已跟開拓師團長對話 = 0x2,
        已說明需要油=0x4,
        已得到油 = 0x8,
    }
}