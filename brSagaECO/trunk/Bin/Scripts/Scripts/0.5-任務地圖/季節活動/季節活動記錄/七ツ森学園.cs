using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum nanatumori
    {  
        幽靈第一次對話 = 0x1,
        第一次雕像 = 0x2,
        廊下第一次 = 0x4,
        水槽第一次 = 0x8,
        地下室 = 0x10,
        對話 = 0x20,
        對話2 = 0x40,
        幽靈第一次對話2 = 0x80,
        第二次雕像 = 0x100,
        廊下第二次 = 0x200,
        對話3 = 0x400,
        廊下第三次 = 0x800,
        出現了 = 0x1000,
        對話4 = 0x2000,
        對話5 = 0x4000,
        幽靈第三次對話 = 0x8000,
        幽靈第四次對話 = 0x10000,
        任務 = 0x20000,
        任務完成 = 0x40000,
        結束 = 0x80000,
        入手 = 0x100000,
        入手2 = 0x200000,
        水槽第二次 = 0x400000,
    }
}
