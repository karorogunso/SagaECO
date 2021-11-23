using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_10
    {
        //農夫
        選擇轉職為農夫 = 0x1,
        農夫轉職任務完成 = 0x2,
        農夫轉職成功 = 0x4,
        已經轉職為農夫 = 0x8,
    }
}
