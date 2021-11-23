using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_11
    {
        //冒險家
        選擇轉職為冒險家 = 0x1,
        冒險家轉職任務完成 = 0x2,
        冒險家轉職成功 = 0x4,
        已經轉職為冒險家 = 0x8,
    }
}
