using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_01
    {
        //劍士
        選擇轉職為劍士 = 0x1,
        劍士轉職任務完成 = 0x2,
        劍士轉職成功 = 0x4,
        已經轉職為劍士 = 0x8,
    }
}
