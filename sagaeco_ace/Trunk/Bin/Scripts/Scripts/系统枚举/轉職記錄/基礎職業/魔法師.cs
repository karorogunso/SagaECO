using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_05
    {
        //魔法師
        選擇轉職為魔法師 = 0x1,
        已經從魔術那裡聽取有關新生魔法的知識 = 0x2,
        魔法師轉職任務完成 = 0x4,
        魔法師轉職成功 = 0x8,
        已經轉職為魔法師 = 0x10,
    }
}
