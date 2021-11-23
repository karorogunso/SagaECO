using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_04
    {
        //弓手
        選擇轉職為弓手 = 0x1,
        弓手轉職任務完成 = 0x2,
        弓手轉職成功 = 0x4,
        已經轉職為弓手 = 0x8,
    }
}
