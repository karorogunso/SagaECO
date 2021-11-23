using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum 测试剧情
    {
        接受委托 = 0x1,
        完成第二关 = 0x2,
        完成第三关 = 0x4,
        完成第四关 = 0x8,
    }
}