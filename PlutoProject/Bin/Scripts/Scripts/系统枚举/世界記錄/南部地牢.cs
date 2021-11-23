using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum NBDLFlags
    {
        接受特效藥任务 = 0x1,//2a52
        寻找特效藥 = 0x2,//2a53
        未获得特效藥 = 0x4,//2a54
    }
}