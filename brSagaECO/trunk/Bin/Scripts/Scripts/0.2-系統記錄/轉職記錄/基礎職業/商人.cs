using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_12
    {
        //商人
        選擇轉職為商人 = 0x1,
        轉交商人總管的信 = 0x2,
        給予老爺爺的錢包 = 0x4,
        給予老爺爺的手帕 = 0x8,
        給予老爺爺的魔杖 = 0x10,
        給予老爺爺的眼鏡 = 0x20,
        給予老爺爺的褲子 = 0x40,
        給予老爺爺的假牙 = 0x80,
        商人轉職任務完成 = 0x100,
        商人轉職成功 = 0x200,
        已經轉職為商人 = 0x400,
    }
}
