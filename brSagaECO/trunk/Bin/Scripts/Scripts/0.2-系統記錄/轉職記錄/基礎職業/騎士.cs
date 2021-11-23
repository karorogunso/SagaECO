using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_02
    {
        //騎士
        選擇轉職為騎士 = 0x1,
        騎士轉職任務完成 = 0x2,
        騎士轉職成功 = 0x4,
        已經轉職為騎士 = 0x8,
    }
}
