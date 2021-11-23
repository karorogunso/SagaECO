using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_03
    {
        //盜賊
        選擇轉職為盜賊 = 0x1,
        盜賊轉職任務完成 = 0x2,
        盜賊轉職成功 = 0x4,
        已經轉職為盜賊 = 0x8,
    }
}
