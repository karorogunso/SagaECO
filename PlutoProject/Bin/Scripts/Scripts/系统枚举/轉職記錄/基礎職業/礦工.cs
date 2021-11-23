using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_09
    {
        //礦工
        選擇轉職為礦工 = 0x1,
        礦工轉職任務完成 = 0x2,
        礦工轉職成功 = 0x4,
        已經轉職為礦工 = 0x8,
    }
}
