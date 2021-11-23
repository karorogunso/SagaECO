using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_07
    {
        //祭司
        選擇轉職為祭司 = 0x1,
        已經從菲爾那裡聽取有關治療魔法的知識 = 0x2,
        祭司轉職任務完成 = 0x4,
        祭司轉職成功 = 0x8,
        已經轉職為祭司 = 0x10,
    }
}