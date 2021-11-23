using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum DailyItem
    {
        聽取說明 = 0x1,//未使用
        第一天 = 0x2,
        第二天 = 0x4,
        第三天 = 0x8,
        第四天 = 0x10,
        第五天 = 0x20,
        第六天 = 0x40,
        第七天 = 0x80,
        第八天 = 0x100,
        第九天 = 0x200,
        第十天 = 0x400,//未使用
    }
}